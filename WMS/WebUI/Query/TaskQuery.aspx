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
            var StartDate = $("#txtQueryStartDate").datebox("getValue");
            var EndDate = $("#txtQueryEndDate").datebox("getValue");

            var Product = $("#txtQueryProduct").textbox("getValue");
            var BatchNo = $("#txtQueryBatchNo").textbox("getValue");
            var Section = $("#txtQuerySection").textbox("getValue");

            if (StartDate != "" && EndDate=="") {
                where += " and CONVERT(nvarchar(10), WCS_TASK.TaskDate,120)>= '" + StartDate + "'";
            }
            if (StartDate == "" && EndDate != "") {
                where += " and CONVERT(nvarchar(10), WCS_TASK.TaskDate,120)<= '" + EndDate + "'";
            }
            if (StartDate != "" && EndDate != "") {
                where += " and CONVERT(nvarchar(10), WCS_TASK.TaskDate,120) between '" + StartDate + "' and '" + EndDate + "'";
            }
            if (Product != "") {
                where += " and (product.ProductName like '%" + Product + "%' or product.ProductCode like '%" + Product + "%')";
            }
            if (BatchNo != "") {
                where += " and BatchNo like '%" + BatchNo + "%'";
            }
            if (BatchNo != "") {
                where += " and SenctionName like '%" + Section + "%'";
            }
            var value = $('input:radio[name="QueryFlag"]:checked').val();
            if (value == "0") { //入库
                where += " and WCS_TASK.TaskType='11'";
            } else if (value == "1") { //出库
                where += " and WCS_TASK.TaskType='12'";
            }
            where = encodeURIComponent(where);
            
            $('#dg').datagrid({
                url: '../../Handler/BaseHandler.ashx?Action=PageDate&Comd=WMS.SelectTask',
                queryParams: { Where: where }
            });
        }
        function Refresh() {
            $("#txtQueryStartDate").datebox("setValue", "");
            $("#txtQueryEndDate").datebox("setValue", "");
            $("#txtQueryProduct").textbox("setValue", "");
            $("#txtQueryBatchNo").textbox("setValue", "");
            $("#txtQuerySection").textbox("setValue", "");
            document.getElementById("QueryFlag2").checked = true;
        }
       
        
 </script>  
</head>
<body  class="easyui-layout">
    <table id="dg"  class="easyui-datagrid" data-options="loadMsg: '正在加载数据，请稍等...',fit:true, rownumbers:true ,
            pagination:true,pageSize:PageSize, pageList:[15, 20, 30, 50],method:'post',striped:true,fitcolumns:true,toolbar:'#tb'">  
        <thead data-options="frozen:true">
			<tr>
                <th data-options="field:'TaskNo',width:90">任务号</th>
                <th data-options="field:'BillTypeName',width:90">任务类型</th>
                <th data-options="field:'StateDesc',width:80">状态</th>
                <th data-options="field:'CellCode',width:100">货位编号</th>
                <th data-options="field:'AisleNo',width:70">巷道</th>
                <th data-options="field:'ProductCode',width:100">产品编号</th>
                <th data-options="field:'ProductName',width:120">产品名称</th>
                <th data-options="field:'BatchNo',width:80">批次</th>
                <th data-options="field:'SectionName',width:60">阶段</th>
                <th data-options="field:'ShelfValue',width:50">排</th>
                <th data-options="field:'CellColumn',width:50">列</th>
                <th data-options="field:'CellRow',width:50">层</th>
                <th data-options="field:'TaskDate',width:120">作业时间</th>
                <th data-options="field:'Tasker',width:80">作业人员</th>
                <th data-options="field:'StartDate',width:120">开始时间</th>
                <th data-options="field:'FinishDate',width:150">结束时间</th>
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
