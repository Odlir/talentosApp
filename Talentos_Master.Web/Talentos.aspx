<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Test de Talentos</title>
    <style type="text/css">
    html, body {
	    height: 100%;
	    width: 100%;
	    overflow: auto;
	    vertical-align: middle;
    }
    body {
	    padding: 0;
	    margin: 0;
    }
    #silverlightControlHost {
	    height: 100%;
	    text-align:center;
	    background-color:GrayText;
    }
    </style>
    <script type="text/javascript" src="Silverlight.js"></script>
    <script type="text/javascript" src="ClientBin/SplashScreen/TalentosSplash.js"></script>
    <script src="script/json2.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function navegador() {

            if (navigator.appName == "Netscape") {
                document.getElementById("mySilverlightObject").height = window.innerHeight + 'px';
            }
            else
                document.getElementById("mySilverlightObject").height = '100%';
        }
	
    
        function CloseWindow() {
            window.close();
        }

        function ShowVideo() {
            window.open("frmVideo.aspx", "List", "scrollbars=no,resizable=no,width=850,height=630");
        }

        function ShowRadar(color1, color2, color3, color4, color5, color6) {
            var pag = "frmRadar.aspx?color1=" + color1 + "&color2=" + color2 + "&color3=" + color3 + "&color4=" + color4 + "&color5=" + color5 + "&color6=" + color6;
            window.open(pag, "List", "scrollbars=no,resizable=no,width=600,height=550");

            return false;
        }

        function ShowAdvertencia() {
            alert("El Test de Talentos es una herramienta para el conocimiento personal, no es una prueba psicológica.");
        }

        function ShowPage(htmlurl) {
            newwindow = window.open(htmlurl);
        }

        function sleep(milliseconds) {
            var start = new Date().getTime();
            for (var i = 0; i < 1e7; i++) {
                if ((new Date().getTime() - start) > milliseconds) {
                    break;
                }
            }
        }

        function OpenDashBoardReport(htmlurl, lpTicket) {
            //alert('height=' + window.screen.height + ',width=' + window.screen.width);
            newwindow = window.open(htmlurl, 'name', 'height=' + window.screen.height + ',width=' + window.screen.width);
            if (window.focus) {
                newwindow.focus();
                sleep(1000);
                var newform = newwindow.document.forms['form1'];
                newform.Ticket.value = lpTicket;
                newform.submit();
            }
            return false;
        }

    
        function onSilverlightError(sender, args) {
            var appSource = "";
            if (sender != null && sender != 0) {
              appSource = sender.getHost().Source;
            }
            
            var errorType = args.ErrorType;
            var iErrorCode = args.ErrorCode;

            if (errorType == "ImageError" || errorType == "MediaError") {
              return;
            }

            var errMsg = "Unhandled Error in Silverlight Application " +  appSource + "\n" ;

            errMsg += "Code: "+ iErrorCode + "    \n";
            errMsg += "Category: " + errorType + "       \n";
            errMsg += "Message: " + args.ErrorMessage + "     \n";

            if (errorType == "ParserError") {
                errMsg += "File: " + args.xamlFile + "     \n";
                errMsg += "Line: " + args.lineNumber + "     \n";
                errMsg += "Position: " + args.charPosition + "     \n";
            }
            else if (errorType == "RuntimeError") {           
                if (args.lineNumber != 0) {
                    errMsg += "Line: " + args.lineNumber + "     \n";
                    errMsg += "Position: " +  args.charPosition + "     \n";
                }
                errMsg += "MethodName: " + args.methodName + "     \n";
            }

            throw new Error(errMsg);
        }
    </script>
</head>
	<body onload="navegador()">
    <form id="form1" runat="server" style="height:99%;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="height: 100%;">
        <object data="data:application/x-silverlight-2," id="mySilverlightObject" type="application/x-silverlight-2"
            width="100%" height="100%">
	      <param name="source" value="ClientBin/Talentos_Master.xap"/>
		  <param name="onError" value="onSilverlightError" />
		  <param name="background" value="white" />
		  <param name="minRuntimeVersion" value="3.0.40624.0" />
		  <param name="autoUpgrade" value="true" />
		  <param name="splashscreensource" value="ClientBin/SplashScreen/TalentosSplash.xaml"/>
          <param name="onSourceDownloadProgressChanged" value="onSourceDownloadProgressChanged" />

		  <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40624.0" style="text-decoration:none">
 			  <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style:none"/>
 			  <p>Al acceder al Juego de Talentos por primera vez, es posible que se le pida descargar “Microsoft Silverlight”. Sírvase esperar mientras se descarga.</p>
		  </a>
	    </object><iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe></div>	    
    </form>
</body>
</html>
