<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JqueryFileUpload.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="stylesheet" type="text/css" href="uploadify/uploadify.css">

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <script src="uploadify/jquery.uploadify.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('#file_upload').uploadify({
                'requeueErrors': true,
                'fileTypeExts': '*.gif; *.jpg; *.png; *.rar; *.zip',
                'fileTypeDesc': 'Image Files',
                'fileSizeLimit': '1000000KB',
                'progressData': 'speed',
                'buttonText': '浏览...',
                'swf': '/uploadify/uploadify.swf',
                'checkExisting': '/Ajax/CheckExists.ashx',
                'uploader': '/Ajax/UploadHandler.ashx',
                'height': 30,
                'width': 120,
                'auto': false,
                'onUploadStart': function (file) {
                    //alert(file.name);
                },
                'formData': {
					'fileItemType' : 'fileType'
				}
            });
        });
	</script>
</head>
<body>
    <form id="form1" runat="server">
	    <input id="file_upload" name="file_upload" type="file" multiple="true">
        <a href="javascript:$('#file_upload').uploadify('upload')">Upload Files</a>
    </form>
</body>
</html>
