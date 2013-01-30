<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="jqModal.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Scripts/jqModal/jqModal.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jqModal/jqModal.js" type="text/javascript"></script>
    <style type="text/css">
        .pop_box {
            background-color:#79A5D1;
            display:none;
            height:342px !important;
            left:50%;
            margin-left:-250px;
            padding:5px;
            position:fixed;
            top:150px;
            width:500px;
            z-index:9999;
            }
        .jqmOverlay{background-color:#000;}
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#pop").jqm({
                modal: true,
                overlay: 40,
                onShow: function (h) {
                    h.w.fadeIn(500);
                },
                onHide: function (h) {
                    h.o.remove();
                    h.w.fadeOut(500)
                }
            }).jqmAddClose("#close");
            $("#show").click(function () {
                $("#pop").jqmShow();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <a id="show" href="javascript:void(0)">显示遮罩层</a>
    <div class="pop_box" id="pop">
    </div>
    </form>
</body>
</html>
