<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RepeaterDemo.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link href="CSS/defaultTheme.css" rel="stylesheet" media="screen" />
    
    <script type="text/javascript" src="JavaScript/jquery-1.4.2.min.js"></script>
    <script src="JavaScript/jquery.fixedheadertable.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $('#myTable01').fixedHeaderTable({ footer: true, cloneHeadToFoot: true, altClass: 'odd', autoShow: false });

            $('#myTable01').fixedHeaderTable('show', 1000);

            $('#myTable02').fixedHeaderTable({ footer: true, altClass: 'odd' });

            $('#myTable05').fixedHeaderTable({ altClass: 'odd', footer: true, fixedColumns: 1 });

            $('#myTable03').fixedHeaderTable({ altClass: 'odd', footer: true, fixedColumns: 1 });

            $('#myTable04').fixedHeaderTable({ altClass: 'odd', footer: true, cloneHeadToFoot: true, fixedColumns: 3 });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <a href="/wiki/Quantum_mechanics" title="Wikipedia: Quantum mechanics">quantum mechanics</a>
        <div class="container_12">
    		<div class="grid_4">
    			<pre>
    				<code>
                        $('#myTable01').fixedHeaderTable({ 
	                        footer: true,
	                        cloneHeadToFoot: true,
	                        altClass: 'odd',
	                        autoShow: false
                        });
    				</code>
    			</pre>
    		</div>
    		<div class="grid_8 height250">
    			<table class="fancyTable" id="myTable01" cellpadding="0" cellspacing="0">
    			    <thead>
    			        <tr>
    			            <th>Browser</th>
    			            <th>Visits</th>
    			            <th>Pages/Visit</th>
    			            <th>Avg. Time on Site</th>
    			            <th>% New Visits</th>
    			            <th>Bounce Rate</th>
    			        </tr>
    			    </thead>
    			    <tbody>
    			        <tr>
    			            <td>Firefox</td>
    			            <td>1,990</td>
    			            <td>3.11</td>
    			            <td>00:04:22</td>
    			            <td>70.00%</td>
    			            <td>32.61%</td>
    			        </tr>
    			        <tr>
    			            <td>Firefox</td>
    			            <td>1,990</td>
    			            <td>3.11</td>
    			            <td>00:04:22</td>
    			            <td>70.00%</td>
    			            <td>32.61%</td>
    			        </tr>
    			        <tr>
    			            <td>Firefox</td>
    			            <td>1,990</td>
    			            <td>3.11</td>
    			            <td>00:04:22</td>
    			            <td>70.00%</td>
    			            <td>32.61%</td>
    			        </tr>
    			        <tr>
    			            <td>Firefox</td>
    			            <td>1,990</td>
    			            <td>3.11</td>
    			            <td>00:04:22</td>
    			            <td>70.00%</td>
    			            <td>32.61%</td>
    			        </tr>
    			        <tr>
    			            <td>Firefox</td>
    			            <td>1,990</td>
    			            <td>3.11</td>
    			            <td>00:04:22</td>
    			            <td>70.00%</td>
    			            <td>32.61%</td>
    			        </tr>
    			        <tr>
    			            <td>Firefox</td>
    			            <td>1,990</td>
    			            <td>3.11</td>
    			            <td>00:04:22</td>
    			            <td>70.00%</td>
    			            <td>32.61%</td>
    			        </tr>
    			        <tr>
    			            <td>Firefox</td>
    			            <td>1,990</td>
    			            <td>3.11</td>
    			            <td>00:04:22</td>
    			            <td>70.00%</td>
    			            <td>32.61%</td>
    			        </tr>
    			        <tr>
    			            <td>Firefox</td>
    			            <td>1,990</td>
    			            <td>3.11</td>
    			            <td>00:04:22</td>
    			            <td>70.00%</td>
    			            <td>32.61%</td>
    			        </tr>
    			        <tr>
    			            <td>Firefox</td>
    			            <td>1,990</td>
    			            <td>3.11</td>
    			            <td>00:04:22</td>
    			            <td>70.00%</td>
    			            <td>32.61%</td>
    			        </tr>
    			        <tr>
    			            <td>Firefox</td>
    			            <td>1,990</td>
    			            <td>3.11</td>
    			            <td>00:04:22</td>
    			            <td>70.00%</td>
    			            <td>32.61%</td>
    			        </tr>
    			        <tr>
    			            <td>Firefox</td>
    			            <td>1,990</td>
    			            <td>3.11</td>
    			            <td>00:04:22</td>
    			            <td>70.00%</td>
    			            <td>32.61%</td>
    			        </tr>
    			        <tr>
    			            <td>Firefox</td>
    			            <td>1,990</td>
    			            <td>3.11</td>
    			            <td>00:04:22</td>
    			            <td>70.00%</td>
    			            <td>32.61%</td>
    			        </tr>
    			        <tr>
    			            <td>Firefox</td>
    			            <td>1,990</td>
    			            <td>3.11</td>
    			            <td>00:04:22</td>
    			            <td>70.00%</td>
    			            <td>32.61%</td>
    			        </tr>
    			        <tr>
    			            <td>Firefox</td>
    			            <td>1,990</td>
    			            <td>3.11</td>
    			            <td>00:04:22</td>
    			            <td>70.00%</td>
    			            <td>32.61%</td>
    			        </tr>
    			        <tr>
    			            <td>Firefox</td>
    			            <td>1,990</td>
    			            <td>3.11</td>
    			            <td>00:04:22</td>
    			            <td>70.00%</td>
    			            <td>32.61%</td>
    			        </tr>
    			        <tr>
    			            <td>Firefox</td>
    			            <td>1,990</td>
    			            <td>3.11</td>
    			            <td>00:04:22</td>
    			            <td>70.00%</td>
    			            <td>32.61%</td>
    			        </tr>
    			    </tbody>
    			</table>
    		</div>
    		<div class="clear"></div>
    	</div>
        
        <div class="container_12 divider">
        	<div class="grid_4">
        		<pre>
    				<code>
                        $('#myTable02').fixedHeaderTable({
                            footer: true,
                            altClass: 'odd',
                        });
    				</code>
    			</pre>
        	</div>
        	<div class="grid_8 height250">
        		<table class="fancyTable" id="myTable02" cellpadding="0" cellspacing="0">
        		    <thead>
        		        <tr>
        		            <th>Browser</th>
        		            <th>Visits</th>
        		            <th>Pages/Visit</th>
        		            <th>Avg. Time on Site</th>
        		            <th>% New Visits</th>
        		            <th>Bounce Rate</th>
        		        </tr>
        		    </thead>
        		    <tfoot>
        		        <tr>
        		            <td>This is a unique footer</td>
        		            <td>1</td>
        		            <td>2</td>
        		            <td>3</td>
        		            <td>4</td>
        		            <td>5</td>
        		        </tr>
        		    </tfoot>
        		    <tbody>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox Web Browser Version 4.0</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		    </tbody>
        		</table>
        	</div>
        	<div class="clear"></div>
        </div>
        
        <div class="container_12 divider">
        	<div class="grid_4">
        		<pre>
        			<code>
                        $('#myTable05').fixedHeaderTable({
	                        altClass: 'odd',
	                        footer: true,
	                        fixedColumns: 1,
                        });
        			</code>
        		</pre>
        	</div>
        	<div class="grid_4 height400">
        		<table class="fancyTable" id="myTable05" cellpadding="0" cellspacing="0">
        		    <thead>
        		        <tr>
        		            <th>Browser</th>
        		            <th>Visits</th>
        		            <th>Pages/Visit</th>
        		            <th>Avg. Time on Site</th>
        		            <th>% New Visits</th>
        		            <th>Bounce Rate</th>
        		            <th>Avg. Time on Site</th>
        		            <th>% New Visits</th>
        		            <th>Bounce Rate</th>
        		        </tr>
        		    </thead>
        		    <tfoot>
        		        <tr>
        		            <th>Browser</th>
        		            <th>Visits</th>
        		            <th>Pages/Visit</th>
        		            <th>Avg. Time on Site</th>
        		            <th>% New Visits</th>
        		            <th>Bounce Rate</th>
        		            <th>Avg. Time on Site</th>
        		            <th>% New Visits</th>
        		            <th>Bounce Rate</th>
        		        </tr>
        		    </tfoot>
        		    <tbody>
        		        <tr>
        		            <td>Firefox first</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22 test test test</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00% test test test</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		    </tbody>
        		</table>
        	</div>
        	<div class="clear"></div>
        </div>
        
        <div class="container_12 divider">
        	<div class="grid_4">
        		<pre>
        			<code>
                        $('#myTable03').fixedHeaderTable({
	                        altClass: 'odd',
	                        footer: true,
	                        fixedColumns: 1,
                        });
        			</code>
        		</pre>
        	</div>
        	<div class="grid_4 height400">
        		<table class="fancyTable" id="myTable03" cellpadding="0" cellspacing="0">
        		    <thead>
        		        <tr>
        		            <th>Browser</th>
        		            <th>Visits</th>
        		            <th>Pages/Visit</th>
        		            <th>Avg. Time on Site</th>
        		            <th>% New Visits</th>
        		            <th>Bounce Rate</th>
        		            <th>Avg. Time on Site</th>
        		            <th>% New Visits</th>
        		            <th>Bounce Rate</th>
        		        </tr>
        		    </thead>
        		    <tfoot>
        		        <tr>
        		            <td colspan="9"><a class="button pagination" href="#">Prev</a><a class="button pagination" href="#">Next</a></td>
        		        </tr>
        		    </tfoot>
        		    <tbody>
        		        <tr>
        		            <td>Firefox first</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22 test test test</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00% test test test</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		    </tbody>
        		</table>
        	</div>
        	<div class="clear"></div>
        </div>
        
		<div class="container_12 divider">
        	<div class="grid_4">
        		<pre>
        			<code>
                        $('#myTable04').fixedHeaderTable({
	                        altClass: 'odd',
	                        footer: true,
	                        cloneHeadToFoot: true,
	                        fixedColumn: 3,
                        });
        			</code>
        		</pre>
        	</div>
        	<div class="grid_4 height400">
        		<table class="fancyTable" id="myTable04" cellpadding="0" cellspacing="0">
        		    <thead>
        		        <tr>
        		            <th>Browser</th>
        		            <th>Visits</th>
        		            <th>Pages/Visit</th>
        		            <th>Avg. Time on Site</th>
        		            <th>% New Visits</th>
        		            <th>Bounce Rate</th>
        		            <th>Avg. Time on Site</th>
        		            <th>% New Visits</th>
        		            <th>Bounce Rate</th>
        		        </tr>
        		    </thead>
        		    <tbody>
        		        <tr>
        		            <td>Firefox first</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22 test test test</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00% test test test</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		        <tr>
        		            <td>Firefox</td>
        		            <td class="numeric">1,990</td>
        		            <td class="numeric">3.11</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		            <td class="numeric">00:04:22</td>
        		            <td class="numeric">70.00%</td>
        		            <td class="numeric">32.61%</td>
        		        </tr>
        		    </tbody>
        		</table>
        	</div>
        	<div class="clear"></div>
        </div>
    </form>
</body>
</html>