<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JQueryUI.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="Scripts/themes/default/easyui.css">
	<link rel="stylesheet" type="text/css" href="Scripts/themes/icon.css">
	<link rel="stylesheet" type="text/css" href="Scripts/demo/demo.css">
	
    <script type="text/javascript" src="Scripts/jquery-1.8.0.min.js"></script>
	<script type="text/javascript" src="Scripts/jquery.easyui.min.js"></script>
    <script>
        $(function () {
            $('#test').treegrid({
                title: 'TreeGrid',
                iconCls: 'icon-save',
                width: 700,
                height: 350,
                nowrap: false,
                rownumbers: true,
                animate: true,
                collapsible: true,
                url: 'json.ashx?Method=Init',
                idField: 'CCode',
                treeField: 'CCode',
                frozenColumns: [[
	                { title: '行政编号', field: 'CCode', width: 200,
	                    formatter: function (value) {
	                        return '<span style="color:red">' + value + '</span>';
	                    }
	                }
				]],
                columns: [[
					{ field: 'CName', title: '行政区', width: 150 },
					{ field: 'GmZz', title: '土地整治', width: 220 },
                    { field: 'GmCJ', title: '拆旧复垦', width: 220 },
					{ field: 'GmNT', title: '标准农田', width: 150 }
				]],
                onBeforeLoad: function (row, param) {
//                    if (row) {
//                        $(this).treegrid('options').url = 'json.ashx?Method=GetTreeData&Condition=' + row.id;
//                    }
                },
                onContextMenu: function (e, row) {
                    e.preventDefault();
                    $(this).treegrid('unselectAll');
                    $(this).treegrid('select', row.code);
                    $('#mm').menu('show', {
                        left: e.pageX,
                        top: e.pageY
                    });
                }
            });
        });

        function reload() {
            var node = $('#test').treegrid('getSelected');
            if (node) {
                $('#test').treegrid('reload', node.code);
            } else {
                $('#test').treegrid('reload');
            }
        }
        function getChildren() {
            var node = $('#test').treegrid('getSelected');
            if (node) {
                var nodes = $('#test').treegrid('getChildren', node.code);
            } else {
                var nodes = $('#test').treegrid('getChildren');
            }
            var s = '';
            for (var i = 0; i < nodes.length; i++) {
                s += nodes[i].code + ',';
            }
            alert(s);
        }
        function getSelected() {
            var node = $('#test').treegrid('getSelected');
            if (node) {
                alert(node.code + ":" + node.name);
            }
        }
        function collapse() {
            var node = $('#test').treegrid('getSelected');
            if (node) {
                $('#test').treegrid('collapse', node.code);
            }
        }
        function expand() {
            var node = $('#test').treegrid('getSelected');
            if (node) {
                $('#test').treegrid('expand', node.code);
            }
        }
        function collapseAll() {
            var node = $('#test').treegrid('getSelected');
            if (node) {
                $('#test').treegrid('collapseAll', node.code);
            } else {
                $('#test').treegrid('collapseAll');
            }
        }
        function expandAll() {
            var node = $('#test').treegrid('getSelected');
            if (node) {
                $('#test').treegrid('expandAll', node.code);
            } else {
                $('#test').treegrid('expandAll');
            }
        }
        function expandTo() {
            $('#test').treegrid('expandTo', '02013');
            $('#test').treegrid('select', '02013');
        }
        var codeIndex = 1000;
        function append() {
            codeIndex++;
            var data = [{
                code: 'code' + codeIndex,
                name: 'name' + codeIndex,
                addr: 'address' + codeIndex,
                col4: 'col4 data'
            }];
            var node = $('#test').treegrid('getSelected');
            $('#test').treegrid('append', {
                parent: (node ? node.code : null),
                data: data
            });
        }
        function remove() {
            var node = $('#test').treegrid('getSelected');
            if (node) {
                $('#test').treegrid('remove', node.code);
            }
        }
	</script>
</head>
<body>
    <form id="form1" runat="server">
        <h2>TreeGrid</h2>
	    <div class="demo-info">
		    <div class="demo-tip icon-tip"></div>
		    <div>Combines treeview and datagrid to represent hierarchical data.</div>
	    </div>
	
	    <div style="margin:10px 0;">
		    <a href="#" onclick="reload()">Reload</a>
		    <a href="#" onclick="getChildren()">GetChildren</a>
		    <a href="#" onclick="getSelected()">GetSelected</a>
		    <a href="#" onclick="collapse()">Collapse</a>
		    <a href="#" onclick="expand()">Expand</a>
		    <a href="#" onclick="collapseAll()">CollapseAll</a>
		    <a href="#" onclick="expandAll()">ExpandAll</a>
		    <a href="#" onclick="expandTo()">ExpandTo</a>
		    <a href="#" onclick="append()">Append</a>
	    </div>
	
	    <table id="test"></table>
	
	    <div id="mm" class="easyui-menu" style="width:120px;">
		    <div onclick="append()">Append</div>
		    <div onclick="remove()">Remove</div>
	    </div>
    </form>
</body>
</html>
