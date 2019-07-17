using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using Microsoft.Win32;

//http://www.briangrinstead.com/blog/multipart-form-post-in-c

namespace ConvertApi
{

    public static class Helpers
    {

        public static void CopyStream(Stream clientResponse, Stream outStream)
        {
            try
            {
                int bytesRead;
                byte[] buffer = new byte[32768];
                while (clientResponse != null && (bytesRead = clientResponse.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outStream.Write(buffer, 0, bytesRead);
                }
            }
            catch (IOException)
            {
                //suppress empty stream response
            }
        }

        public static string GetMimeType(string filePath)
        {

            const string defaultMimeType = "application/octet-stream";

            FileInfo fileInfo = new FileInfo(filePath);
            string fileExtension = fileInfo.Extension.ToLower();

            // direct mapping which is fast and ensures these extensions are found
            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                    return "text/html";
                case ".js":
                    return "text/javascript"; // registry may return "application/x-javascript"
                case ".pdf":
                    return "application/pdf";
            }



            // looks for extension with a content type
            RegistryKey rkContentTypes = Registry.ClassesRoot.OpenSubKey(fileExtension);
            if (rkContentTypes != null)
            {
                object key = rkContentTypes.GetValue("Content Type");
                if (key != null)
                    return key.ToString().ToLower();
            }


            // looks for a content type with extension
            // Note : This would be problem if  multiple extensions associate with one content type.
            var typeKey = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type");

            foreach (string keyname in typeKey.GetSubKeyNames())
            {
                RegistryKey curKey = typeKey.OpenSubKey(keyname);
                if (curKey != null)
                {
                    object extension = curKey.GetValue("Extension");
                    if (extension != null)
                    {
                        if (extension.ToString().ToLower() == fileExtension)
                        {
                            return keyname;
                        }
                    }
                }
            }

            return defaultMimeType;
        }
    }

    public abstract class FormUpload
    {

        private static readonly Encoding Encoding = Encoding.UTF8;
        public static HttpWebResponse MultipartFormDataPost(string postUrl, string userAgent, int timeOut, Dictionary<string, object> postParameters, bool compressRequest)
        {
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;

            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

            return PostForm(postUrl, userAgent, timeOut, contentType, formData, compressRequest);
        }

        private static HttpWebResponse PostForm(string postUrl, string userAgent, int timeOut, string contentType, byte[] formData, bool compress)
        {
            //http://msdn.microsoft.com/en-us/library/system.net.servicepointmanager.expect100continue.aspx
            ServicePointManager.Expect100Continue = false;

            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;

            if (request == null)
            {
                throw new NullReferenceException("request is not a http request");
            }

            if (compress)
            {
                request.Headers.Add(HttpRequestHeader.ContentEncoding, "gzip");
                formData = Compress(formData);
            }

            // Set up the request properties
            request.Timeout = (int)TimeSpan.FromSeconds(timeOut).TotalMilliseconds;
            request.Method = "POST";
            request.ContentType = contentType;
            request.UserAgent = userAgent;
            request.CookieContainer = new CookieContainer();
            request.ContentLength = formData.Length;  // We need to count how many bytes we're sending. 

            using (Stream requestStream = request.GetRequestStream())
            {
                // Push it out there
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
            }

            return request.GetResponse() as HttpWebResponse;
        }

        private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            Stream formDataStream = new System.IO.MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {
                // Thanks to feedback from commenters, add a CRLF to allow multiple parameters to be added.
                // Skip it on the first parameter, add it to subsequent parameters.
                if (needsCLRF)
                    formDataStream.Write(Encoding.GetBytes("\r\n"), 0, Encoding.GetByteCount("\r\n"));

                needsCLRF = true;

                if (param.Value is FileParameter)
                {
                    FileParameter fileToUpload = (FileParameter)param.Value;

                    // Add just the first part of this param, since we will write the file data directly to the Stream
                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        param.Key,
                        fileToUpload.FileName ?? param.Key,
                        fileToUpload.ContentType ?? "application/octet-stream");

                    formDataStream.Write(Encoding.GetBytes(header), 0, Encoding.GetByteCount(header));

                    // Write the file data directly to the Stream, rather than serializing it to a string.
                    formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                }
                else
                {
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        param.Key,
                        param.Value);
                    formDataStream.Write(Encoding.GetBytes(postData), 0, Encoding.GetByteCount(postData));
                }
            }

            // Add the end of the request.  Start with a newline
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(Encoding.GetBytes(footer), 0, Encoding.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }

        public class FileParameter
        {
            public byte[] File { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public FileParameter(byte[] file) : this(file, null) { }
            public FileParameter(byte[] file, string filename) : this(file, filename, null) { }
            public FileParameter(byte[] file, string filename, string contenttype)
            {
                File = file;
                FileName = filename;
                ContentType = contenttype;
            }
        }

        public static byte[] Compress(byte[] raw)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
                    gzip.Write(raw, 0, raw.Length);
                return memory.ToArray();
            }
        }

    }
}