<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Talentos_Master.Web._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript" language="javascript">
        var resolucion = screen.availWidth;
        
        if (resolucion >= 1024) 
        {
            location.href = "Talentos_MasterTestPage.aspx"; 
        } 
        else {
                location.href = "Talentos_Master__1024x768TestPage.aspx";                
            }
    </script>
    <form id="form1" runat="server">
    <div>
    </div>
    </form>
</body>
</html>
