<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JboxForm.aspx.cs" Inherits="JBoxDemo.JboxForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript" src="jbox-v2.3/jBox/jquery-1.4.2.min.js"></script>
    <script type="text/javascript">
        var valueText = "子页传给你的值";
        
        function test() {
            alert("子页调用方法成功");
            Save();
        }
        
        function Save() {
            var jsonData = '{"GUID":"20120724133216703","ItemCode":"2101222012030","ItemName":"","Stage":12,"Xh":1,"ZT":0,"Ccode":"210122","Scrq":"","Scdw":"","Qsjd":"122,40,4","Jzjd":"122,40,7","Qswd":"41,18,13","Jzwd":"41,18,21","ZJGGList":[],"DKList":[{"QSXZ":"集体土地所有权","DLMC":"旱地、水田","XZQS":[{"DKID":"1","Jsmj":1644816598.14265,"XZQDM":"210122","XZQMC":"辽中县"}],"DLTBList":[{"ShapeArea":0.0,"ShapeLin":0.0,"bgbh":"","bgjlh":"","bgrq":"0001-01-01T00:00:00","bsm":0.0,"bz":"","dlbm":"013","dlbz":"","dlmc":"旱地","fldm":"","gdlx":"","gkjsqk":"","gxrq":"","hd":"","jlrq":"","kcdlbm":"","kclx":"","lxdwmj":0.0,"pcmj":0.0,"pdjb":"","pzwh":"","qsdwdm":"2101221052000001000","qsdwmc":"田家房村","qsxz":"30","qszdh":"","shape":"","sjyt":"","ssqy":"","sztfh":"","tbbh":"178","tbdlmj":3280.60981284,"tbmj":0.0,"tbybh":"","tkmj":0.0,"tkxs":0.0,"xzdwmj":0.0,"xzqdm":"","xzqmc":"田家房村","ydlbm":"","ysdm":"","zldwdm":"","zldwmc":"田家房村","zxdl":""},{"ShapeArea":0.0,"ShapeLin":0.0,"bgbh":"","bgjlh":"","bgrq":"0001-01-01T00:00:00","bsm":0.0,"bz":"","dlbm":"011","dlbz":"","dlmc":"水田","fldm":"","gdlx":"","gkjsqk":"","gxrq":"","hd":"","jlrq":"","kcdlbm":"","kclx":"","lxdwmj":0.0,"pcmj":0.0,"pdjb":"","pzwh":"","qsdwdm":"2101221052000001000","qsdwmc":"田家房村","qsxz":"30","qszdh":"","shape":"","sjyt":"","ssqy":"","sztfh":"","tbbh":"177","tbdlmj":2066.79335712,"tbmj":0.0,"tbybh":"","tkmj":0.0,"tkxs":0.0,"xzdwmj":72.08121729,"xzqdm":"","xzqmc":"田家房村","ydlbm":"","ysdm":"","zldwdm":"","zldwmc":"田家房村","zxdl":""},{"ShapeArea":0.0,"ShapeLin":0.0,"bgbh":"","bgjlh":"","bgrq":"0001-01-01T00:00:00","bsm":0.0,"bz":"","dlbm":"011","dlbz":"","dlmc":"水田","fldm":"","gdlx":"","gkjsqk":"","gxrq":"","hd":"","jlrq":"","kcdlbm":"","kclx":"","lxdwmj":0.0,"pcmj":0.0,"pdjb":"","pzwh":"","qsdwdm":"2101221052000001000","qsdwmc":"田家房村","qsxz":"30","qszdh":"","shape":"","sjyt":"","ssqy":"","sztfh":"","tbbh":"169","tbdlmj":1787.45602809,"tbmj":0.0,"tbybh":"","tkmj":0.0,"tkxs":0.0,"xzdwmj":86.66602144,"xzqdm":"","xzqmc":"田家房村","ydlbm":"","ysdm":"","zldwdm":"","zldwmc":"田家房村","zxdl":""}],"LXDWList":[],"XZDWList":[{"bgbh":"","bgjlh":"","bgrq":"0001-01-01T00:00:00","bsm":0.0,"bz":"","cd":0.0,"dlbm":"117","dlmc":"沟渠","fldm":"","gxrq":"","jlrq":"","kcbl":0.0,"kctbbh1":"","kctbbh2":"","kctbdwdm1":"","kctbdwdm2":"","kd":0.0,"qsdwdm1":"2101221052000001000","qsdwdm2":"","qsdwmc1":"田家房村","qsdwmc2":"","qsxz":"30","shape":"","ShapeLin":0.0,"ssqy":"","xzdwbh":"","xzdwmc":"","xzdwmj":72.08121729,"xzdwybh":"","xzqdm":"","xzqdm1":"","xzqdm2":"","ydlbm":"","ysdm":"","zkxs":0.0,"zldwdm1":"","zldwdm2":""},{"bgbh":"","bgjlh":"","bgrq":"0001-01-01T00:00:00","bsm":0.0,"bz":"","cd":0.0,"dlbm":"117","dlmc":"沟渠","fldm":"","gxrq":"","jlrq":"","kcbl":0.0,"kctbbh1":"","kctbbh2":"","kctbdwdm1":"","kctbdwdm2":"","kd":0.0,"qsdwdm1":"2101221052000001000","qsdwdm2":"","qsdwmc1":"田家房村","qsdwmc2":"","qsxz":"30","shape":"","ShapeLin":0.0,"ssqy":"","xzdwbh":"","xzdwmc":"","xzdwmj":86.66602144,"xzdwybh":"","xzqdm":"","xzqdm1":"","xzqdm2":"","ydlbm":"","ysdm":"","zkxs":0.0,"zldwdm1":"","zldwdm2":""}],"DKID":"1","JsTfh":"","Jsmj":7293.60643677,"Points":[{"Jzdbh":"","Group":0,"X":41472179.055303112,"Y":4574607.2334698308,"Z":NaN,"GUID":0},{"Jzdbh":"","Group":0,"X":41472249.170026675,"Y":4574351.9100425169,"Z":NaN,"GUID":1},{"Jzdbh":"","Group":0,"X":41472167.1490293,"Y":4574441.8685557675,"Z":NaN,"GUID":2},{"Jzdbh":"","Group":0,"X":41472179.055303112,"Y":4574603.2647118932,"Z":NaN,"GUID":3},{"Jzdbh":"","Group":0,"X":41472179.055303112,"Y":4574607.2334698308,"Z":NaN,"GUID":4}],"Jzds":5,"Scmj":0.0,"Dkbh":"","Dkmc":"新建地块_1","Txlx":"","ScTfh":"","Dkyt":"","Thjb":""}],"Jldw":"米","Jd":0.0001,"Zbx":"","Dh":41,"Jdfd":3,"Tylx":"高斯克吕格","Zhzbcs":""}';
            $("#Button1").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Jbox页面
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" style="display:none" />
        </div>
    </form>
</body>
</html>
