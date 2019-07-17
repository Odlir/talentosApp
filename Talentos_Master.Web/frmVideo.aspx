<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmVideo.aspx.cs" Inherits="Talentos_Master.Web.frmVideo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="height:100%; width:100%" >
	    <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="830" height="630" > <%--width="800" height="600"--%>
		    <param name="source" value="ClientBin/VideoPlayer.xap"/>
		    <param name="background" value="white" />
		    <param name="initParams" value="m=video.wmv" />
            <param name="minruntimeversion" value="2.0.31005.0" />
		    <a href="http://go.microsoft.com/fwlink/?LinkId=124807" style="text-decoration: none;">
 			    <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style: none"/>
		    </a>
	    </object>
    </form>
</body>
</html>
