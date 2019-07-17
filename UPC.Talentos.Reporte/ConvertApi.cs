using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace ConvertApi
{

    public class ConvertApiException : Exception
    {
        public ConvertApiException() { }
        public ConvertApiException(string message) : base(message) { }
        public ConvertApiException(string message, Exception inner) : base(message, inner) { }

        readonly HttpStatusCode _statusCode;
        readonly string _statusDescription;
        private readonly bool _customException = false;

        public ConvertApiException(HttpStatusCode statusCode, string statusDescription)
        {
            _statusCode = statusCode;
            _statusDescription = statusDescription;
            _customException = true;
        }

        public override string ToString()
        {
            if (!_customException)
                return base.ToString();
            else
            {
                const string convertapiServerResponse = "ConvertApi server response:";
                return _statusCode != HttpStatusCode.Unused
                           ? String.Format("{0} {1}  {2}", convertapiServerResponse, (int)_statusCode, _statusDescription)
                           : String.Format("{0} {1}", convertapiServerResponse, _statusDescription);
            }

        }

    }

    public abstract class ConvertApiBase
    {



        //internal string ApiBaseUri = "http://do.convertapi.com/";
        public static string ApiBaseUri = "http://do.convertapi.com/";


        private const string UserAgent = "ConvertApi DotNet library";
        internal Dictionary<string, object> PostParameters = new Dictionary<string, object>();
        private static readonly Dictionary<string, WebHeaderCollection> ApiStaticDataDictionary = new Dictionary<string, WebHeaderCollection>();
        private int _httpRequestTimeOut = 320;
        private const int ApiConverterTimeOutBias = 60;


        protected ConvertApiBase(int apiKey)
        {
            PostParameters["ApiKey"] = apiKey.ToString();
        }

        protected ConvertApiBase()
        {

        }


        private string GetConversionUri { get { return ApiBaseUri + Api; } }
        private string GetInfoUri { get { return ApiBaseUri + Api + "/info"; } }

        internal string Api { get { return GetType().Name; } }

        public int? GetFileSize { get; private set; }
        public string GetOutputFileName { get; private set; }
        public string GetProcessId { get; private set; }

        public void SetTimeout(int value)
        {
            PostParameters["Timeout"] = value;
            _httpRequestTimeOut = value * ApiConverterTimeOutBias;
        }


        public int GetCreditsCost
        {
            get
            {
                return int.Parse(GetApiStaticData("CreditsCost"));
            }
        }

        public int? GetCreditsLeft
        {
            get
            {
                var creditsLeft = GetApiStaticData("CreditsLeft");
                return String.IsNullOrEmpty(creditsLeft)
                           ? (int?)null
                           : System.Convert.ToInt32(creditsLeft);
            }
        }

        public string GetInputFormat
        {
            get
            {
                return GetApiStaticData("InputFormat");
            }
        }

        public string GetOutputFormat
        {
            get
            {
                return GetApiStaticData("OutputFormat");
            }
        }

        private string GetApiStaticData(string property)
        {
            string dictionaryKey = Api;

            WebHeaderCollection webHeaderCollection;

            if (ApiStaticDataDictionary.ContainsKey(dictionaryKey))
                webHeaderCollection = ApiStaticDataDictionary[dictionaryKey];
            else
            {
                Post(GetInfoUri, PostParameters, null, out webHeaderCollection, false);
                ApiStaticDataDictionary[dictionaryKey] = webHeaderCollection;
            }

            /*            List<string> propertyValue = new List<string>();
                        foreach (var item in webHeaderCollection[property].Split(new[] { ',' }))
                        {
                            propertyValue.Add(item);
                        }*/

            return webHeaderCollection[property];
        }

        public bool IsExtensionSupported(string inputFileExt)
        {
            return GetInputFormat.Contains(inputFileExt);
        }

        public bool IsExtensionSupported(string inputFileExt, string outputFileExt)
        {
            return GetInputFormat.Contains(inputFileExt) && GetOutputFormat.Contains(outputFileExt);
        }

        internal void Post(string postUri, Dictionary<string, object> postParameters, Stream outStream, out WebHeaderCollection webHeaderCollection, bool compressRequest)
        {
            try
            {
                HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(postUri, UserAgent, _httpRequestTimeOut, postParameters, compressRequest);
                using (Stream clientResponse = webResponse.GetResponseStream())
                    if (webResponse.StatusCode == HttpStatusCode.OK)
                    {
                        Helpers.CopyStream(clientResponse, outStream);
                        webHeaderCollection = webResponse.Headers;
                    }
                    else
                    {
                        throw new WebException("Response error!");
                    }
            }
            catch (WebException e)
            {
                if (e.Response != null)
                {
                    HttpWebResponse httpWebResponse = ((HttpWebResponse)e.Response);
                    throw new ConvertApiException(httpWebResponse.StatusCode, httpWebResponse.StatusDescription);
                }
                else
                {
                    throw new ConvertApiException("Exception occurred while posting data to server.", e);
                }
            }
        }



        internal void Convert(Dictionary<string, object> postParameters, Stream outStream, bool compressRequest)
        {
            WebHeaderCollection webHeaderCollection;
            Post(GetConversionUri, postParameters, outStream, out webHeaderCollection, compressRequest);
            ApiStaticDataDictionary[Api] = webHeaderCollection;
            GetFileSize = System.Convert.ToInt32(webHeaderCollection["FileSize"]);
            GetOutputFileName = webHeaderCollection["OutputFileName"];
            GetProcessId = webHeaderCollection["ProcessId"];
        }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class FileMethods : ConvertApiBase
    {
        protected FileMethods(int apiKey)
            : base(apiKey)
        {
        }

        protected FileMethods()
        {
        }

        public void ConvertFile(string inFileName, string outFileName, bool compressRequest)
        {
            try
            {
                using (FileStream intStream = new FileStream(inFileName, FileMode.Open))
                {
                    using (FileStream outStream = new FileStream(outFileName, FileMode.Create))
                    {
                        ConvertFile(intStream, inFileName, outStream, false);
                    }
                }

            }
            finally
            {
                //Deleting empty output file on exception               
                if (File.Exists(outFileName) && new FileInfo(outFileName).Length == 0) File.Delete(outFileName);
            }
        }

        public void ConvertFile(string inFileName, Stream outStream, bool compressRequest)
        {
            using (FileStream intStream = new FileStream(inFileName, FileMode.Open))
            {
                ConvertFile(intStream, inFileName, outStream, false);

            }
        }

        public virtual void ConvertFile(Stream inStream, string fileName, Stream outStream, bool compressRequest)
        {
            byte[] data = new byte[inStream.Length];
            inStream.Read(data, 0, data.Length);
            inStream.Close();

            /*            if (!PostParameters.ContainsKey("OutputFileName"))
                            SetOutputFileName(Path.GetFileNameWithoutExtension(fileName));*/

            PostParameters.Add("file", new FormUpload.FileParameter(data, fileName, Helpers.GetMimeType(fileName)));

            Convert(PostParameters, outStream, compressRequest);
        }
    }

    public abstract class OfficeEntity : FileMethods
    {

        protected OfficeEntity(int apiKey)
            : base(apiKey)
        {
        }

        protected OfficeEntity()
        {
        }



        public void SetDocumentTitle(string value)
        {
            PostParameters["DocumentTitle"] = value;
        }

        public void SetDocumentSubject(string value)
        {
            PostParameters["DocumentSubject"] = value;
        }

        public void SetDocumentAuthor(string value)
        {
            PostParameters["DocumentAuthor"] = value;
        }

        public void SetDocumentKeywords(string value)
        {
            PostParameters["DocumentKeywords"] = value;
        }

         public void SetOutputFormat(string value)
        {
            PostParameters["OutputFormat"] = value;
        }

    }

    public abstract class Pdf2SourceEntity : FileMethods
    {
        protected Pdf2SourceEntity(int apiKey) : base(apiKey)
        {
        }

        protected Pdf2SourceEntity()
        {
        }

        public void SetOutputFormat(string value)
        {
            PostParameters["OutputFormat"] = value;
        }
    }

    public class Word2Pdf : OfficeEntity
    {
        public Word2Pdf(int apiKey)
            : base(apiKey)
        {
        }

        public Word2Pdf()
        {
        }



    }

    public class Excel2Pdf : OfficeEntity
    {
        public Excel2Pdf(int apiKey)
            : base(apiKey)
        {
        }

        public Excel2Pdf()
        {
        }


    }

    public class Text2Pdf : OfficeEntity
    {
        public Text2Pdf(int apiKey)
            : base(apiKey)
        {
        }

        public Text2Pdf()
        {
        }


    }

    public class RichText2Pdf : OfficeEntity
    {
        public RichText2Pdf(int apiKey)
            : base(apiKey)
        {
        }

        public RichText2Pdf()
        {
        }


    }


    public class PowerPoint2Pdf : OfficeEntity
    {
        public PowerPoint2Pdf(int apiKey)
            : base(apiKey)
        {
        }

        public PowerPoint2Pdf()
        {
        }



    }

    public class Lotus2Pdf : OfficeEntity
    {
        public Lotus2Pdf(int apiKey)
            : base(apiKey)
        {
        }

        public Lotus2Pdf()
        {
        }



    }

    public class Snp2Pdf : OfficeEntity
    {
        public Snp2Pdf(int apiKey)
            : base(apiKey)
        {
        }

        public Snp2Pdf()
        {
        }



    }

    public class Image2Pdf : OfficeEntity
    {
        public Image2Pdf(int apiKey)
            : base(apiKey)
        {
        }

        public Image2Pdf()
        {
        }



    }

    public class Xps2Pdf : OfficeEntity
    {
        public Xps2Pdf(int apiKey)
            : base(apiKey)
        {
        }

        public Xps2Pdf()
        {
        }



    }

    public class Publisher2Pdf : OfficeEntity
    {
        public Publisher2Pdf(int apiKey)
            : base(apiKey)
        {
        }

        public Publisher2Pdf()
        {
        }


    }

    public class OpenOffice2Pdf : OfficeEntity
    {
        public OpenOffice2Pdf(int apiKey)
            : base(apiKey)
        {
        }

        public OpenOffice2Pdf()
        {
        }


    }

    public abstract class SourceEntity : FileMethods
    {
        protected SourceEntity(int apiKey)
            : base(apiKey)
        {
        }

        protected SourceEntity()
        {
        }



    }



    public abstract class WebEntity : ConvertApiBase
    {
        protected WebEntity(int apiKey)
            : base(apiKey)
        {
        }

        protected WebEntity()
        {
        }


        public void ConvertFile(string inFileName, string outFileName, bool compressRequest)
        {
            try
            {
                string htmlContent = File.ReadAllText(inFileName);

                using (FileStream outStream = new FileStream(outFileName, FileMode.Create))
                {
                    ConvertHtml(htmlContent, outStream, compressRequest);
                }


            }
            finally
            {
                //Deleting empty output file on exception               
                if (File.Exists(outFileName) && new FileInfo(outFileName).Length == 0) File.Delete(outFileName);
            }
        }

        public void ConvertHtml(string html, Stream stream, bool compressRequest)
        {
            ConvertUri(html, stream, compressRequest);
        }


        public void ConvertUri(string uri, string outFileName, bool compressRequest)
        {
            using (FileStream outStream = new FileStream(outFileName, FileMode.Create))
            {
                ConvertUri(uri, outStream, compressRequest);
            }

        }

        public void ConvertUri(string uri, Stream stream, bool compressRequest)
        {

            PostParameters["CUrl"] = uri;
            Convert(PostParameters, stream, compressRequest);
        }

        public void SetConversionDelay(int value)
        {
            PostParameters["ConversionDelay"] = value.ToString();
        }

        public void SetScripts(bool value)
        {
            PostParameters["Scripts"] = value.ToString();
        }

        public void SetPlugins(bool value)
        {
            PostParameters["Plugins"] = value.ToString();
        }

        public void SetAuthUsername(string value)
        {
            PostParameters["AuthUsername"] = value;
        }

        public void SetAuthPassword(string value)
        {
            PostParameters["AuthPassword"] = value;
        }

    }

    public class Web2Pdf : WebEntity
    {
        public Web2Pdf(int apiKey)
            : base(apiKey)
        {
        }

        public Web2Pdf()
        {
        }



        public void SetPrintType(bool value)
        {
            PostParameters["PrintType"] = value.ToString();
        }

        public void SetMarginLeft(string value)
        {
            PostParameters["MarginLeft"] = value;
        }

        public void SetMarginRight(string value)
        {
            PostParameters["MarginRight"] = value;
        }

        public void SetMarginTop(string value)
        {
            PostParameters["MarginTop"] = value;
        }


        public void SetMarginBottom(string value)
        {
            PostParameters["MarginBottom"] = value;
        }

        public void SetDocumentTitle(string value)
        {
            PostParameters["DocumentTitle"] = value;
        }

        public void SetPageOrientation(string value)
        {
            PostParameters["PageOrientation"] = value;
        }

        public void SetPageSize(string value)
        {
            PostParameters["PageSize"] = value;
        }

        public void SetPageWidth(string value)
        {
            PostParameters["PageWidth"] = value;
        }

        public void SetPageHeight(string value)
        {
            PostParameters["PageHeight"] = value;
        }

        public void SetCoverUrl(string value)
        {
            PostParameters["CoverUrl"] = value;
        }

        public void SetOutline(bool value)
        {
            PostParameters["Outline"] = value;
        }

        public void SetBackground(bool value)
        {
            PostParameters["Background"] = value;
        }

        public void SetPageNo(bool value)
        {
            PostParameters["PageNo"] = value;
        }

        public void SetInfoStamp(bool value)
        {
            PostParameters["InfoStamp"] = value;
        }

        public void SetLowQuality(bool value)
        {
            PostParameters["LowQuality"] = value;
        }

        public void SetFooterUrl(string value)
        {
            PostParameters["FooterUrl"] = value;
        }

        public void SetHeaderUrl(string value)
        {
            PostParameters["HeaderUrl"] = value;
        }

    }

    public class Web2Image : WebEntity
    {
        public Web2Image(int apiKey)
            : base(apiKey)
        {
        }

        public Web2Image()
        {
        }



        public void SetPageWidth(int value)
        {
            PostParameters["PageWidth"] = value;
        }

        public void SetPageHeight(int value)
        {
            PostParameters["PageHeight"] = value;
        }


    }


}
