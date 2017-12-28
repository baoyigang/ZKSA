<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskQuery.aspx.cs" Inherits="WebUI_Query_TaskQuery" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="~/Css/default.css" />
    <link rel="stylesheet" type="text/css" href="~/Css/icon.css" />
    <link rel="stylesheet" type="text/css" href="../../EasyUI/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../EasyUI/themes/icon.css" />
    <script type="text/javascript" src="../../EasyUI/jquery.min.js"></script>
    <script type="text/javascript" src="../../EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../JScript/JsAjax.js" ></script>
    <script type="text/javascript" language="javascript">
        var SessionUrl = '<% =ResolveUrl("~/Login.aspx")%>';
        $(function () {
            var data = { Action: 'FillDataTable', Comd: 'cmd.SelectProductCategory', Json: "[{\"{0}\": \"1=1\",\"{1}\": \"1\"}]" };
            BindComboList(data, 'txtQueryCategory', 'CategoryCode', 'CategoryName');
        });
        function Query() {
            var where = "1=1 ";
            var CategoryCode = $("#txtQueryCategory").combobox("getValue");
            var Product = $("#txtQueryProduct").textbox("getValue");
            var BatchNo = $("#txtQueryBatchNo").textbox("getValue");
            if (CategoryCode != "") {
                where += " and product.CategoryCode = '" + CategoryCode + "'";
            }
            if (Product != "") {
                where += " and (product.ProductName like '%" + Product + "%' or product.ProductCode like '%" + Product + "%')";
            }
            if (BatchNo != "") {
                where += " and BatchNo like '%" + BatchNo + "%'";
            }
            where = encodeURIComponent(where);
            var value = $('input:radio[name="QueryFlag"]:checked').val();
            var Comd = "SelectProductDetailQuery";
            if (value == "1") { //统计表
                Comd = "SelectProductTotalQuery";

            }
            $('#dg').datagrid({
                url: '../../Handler/BaseHandler.ashx?Action=PageDate&Comd=WMS.' + Comd,
                queryParams: { Where: where }
            });
            if (value == "1") {
                $('#dg').datagrid('hideColumn', 'CellCode');
                $('#dg').datagrid('hideColumn', 'InDate');

            }
            else {
                $('#dg').datagrid('showColumn', 'CellCode');
                $('#dg').datagrid('showColumn', 'InDate');
            }
        }
        function Refresh() {
            $("#txtQueryCategory").combobox("setValue", "");
            $("#txtQueryProduct").textbox("setValue", "");
            $("#txtQueryBatchNo").textbox("setValue", "");
            document.getElementById("QueryFlag0").checked = true;
        }
       
        
 </script>  
</head>
<body  class="easyui-layout">
    <table id="dg"  class="easyui-datagrid" data-options="loadMsg: '正在加载数据，请稍等...',fit:true, rownumbers:true ,
            pagination:true,pageSize:PageSize, pageList:[15, 20, 30, 50],method:'post',striped:true,fitcolumns:true,toolbar:'#tb'">  
        <thead data-options="frozen:true">
			<tr>
                <th data-options="field:'CategoryName',width:90">产品类别</th>
                <th data-options="field:'ProductCode',width:90">产品编号</th>
                <th data-options="field:'ProductName',width:150">产品名称</th>
                <th data-options="field:'BatchNo',width:120">批次</th>
                <th data-options="field:'SectionName',width:100">阶段</th>
                <th data-options="field:'PalletQty',width:80">托盘数</th>
                <th data-options="field:'Qty',width:80">产品数量</th>
                <th data-options="field:'CellCode',width:120">货位</th>
                <th data-options="field:'InDate',width:150">入库时间</th>
            </tr>
        </thead>
        
    </table>
      
    <div id="tb" style="padding: 5px; height: auto">  
        <table style="width:100%" >
            <tr>
                 <td>
                     任务时间
                <input id="txtQueryStartDate" class="easyui-datebox" style="width:110px"/>
                ~
                <input id="txtQueryEndDate"class="easyui-datebox" style="width:110px" />

                产品
                <input id="txtQueryProduct" class="easyui-textbox" style="width:100px"/>
                批次
                <input id="txtQueryBatchNo" class="easyui-textbox" style="width:100px"/>
                阶段
                <input id="txtQuerySection" class="easyui-textbox" style="width:100px"/>
                <input type="radio" id="QueryFlag0" name="QueryFlag" value="0" >入库</input>
                <input type="radio" id="QueryFlag1" name="QueryFlag" value="1">出库</input>
                <input type="radio" id="QueryFlag2" name="QueryFlag" value="2" checked="checked">全部</input>
                <a  href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="Query()" >查询</a>
                <a  href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload'" onclick="Refresh()" >重新过滤</a>
                </td>
            </tr>
        </table>
   </div>
</body>
</html>
