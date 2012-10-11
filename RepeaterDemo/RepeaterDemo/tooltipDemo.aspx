<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tooltipDemo.aspx.cs" Inherits="RepeaterDemo.tooltipDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>

    <link href="CSS/jquery.qtip.min.css" rel="stylesheet" media="screen" />
    <script src="JavaScript/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="JavaScript/jquery.qtip.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            // Match all <A/> links with a title tag and use it as the content (default).
        $('a[title]').qtip();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="tooltips">
        <a href="#" title="Wikipedia: Quantum mechanics">quantum mechanics</a>
    </div>
    <a href="/wiki/Quantum_mechanics" title="Wikipedia: Quantum mechanics">quantum mechanics</a>
    </form>
</body>
</html>
