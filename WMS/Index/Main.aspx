<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Index_Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"> 
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="~/Css/default.css" />
    <link rel="stylesheet" type="text/css" href="~/Css/icon.css" />
    <link href="../Css/Main.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../EasyUI/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../EasyUI/themes/icon.css" />
    <script type="text/javascript" src="../EasyUI/jquery.min.js"></script>
    <script type="text/javascript" src="../EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../JScript/JsAjax.js" ></script>
   
    <script type="text/javascript" language="javascript">
        var BaseUrl = "../Handler/BaseHandler.ashx";
        var OtherUrl = "../Handler/OtherHandler.ashx";

        function SetNewColor(source) {
            _oldColor = source.style.backgroundColor;
            source.style.backgroundColor = '#C0E4EE';
            source.style.cursor = "pointer";
            source.style.border = "1px solid #5384bb";
        }
        function SetOldColor(source) {
            source.style.backgroundColor = _oldColor;
            source.style.cursor = "default";
            source.style.border = "1px solid #EAF2F4";

        }
        function ShowFormMsg(obj) {
            var msg = document.getElementById(obj).value;
            alert(msg);
        }
        $(function () {
            if (HasExists('VCMD_ProductInstock', "1=1", '')) {
                $('#dgSub').datagrid({
                    url: '../Handler/BaseHandler.ashx?Action=PageDate&Comd=WMS.SelectProductInStock',
                    queryParams: { Where: encodeURIComponent("1=1") }
                });
                $('#AddWin').dialog('open').dialog('setTitle', '出库产品');
            }

        });
        function CreateOutStock() {
            var rows = $('#dgSub').datagrid('getChecked'); 
              if (rows == null || rows.length == 0) {
                $.messager.alert("提示", "请选择要产生出库单的行！", "info");
                return false;
            }
            if (rows) {
                var BillID = '';
                var data = { Action: 'AutoCodeByTableName', PreName: 'OS', Filter: '1=1', TableName: 'WMS_Bill', dtTime: new Date().Format("yyyy/MM/dd") };
                $.ajax({
                    type: "post",
                    url: BaseUrl,
                    data: data,
                    //contentType: "application/json; charset=utf-8",
                    dataType: "text",
                    async: false,
                    success: function (data) {
                        BillID = data;
                    },
                    error: function (msg) {
                        alert(msg);
                    }
                });
                var json = "";
                var rows = $('#dgSub').datagrid('getChecked'); // dgPI.datagrid('getChanges');
                for (var i = 0; i < rows.length; i++) {

                    rows[i].BillID = 'OS' + (parseInt(BillID.substr(2, BillID.length - 2)) + i).toString();
                    rows[i].BillDate = new Date().Format("yyyy/MM/dd");
                    rows[i].Creator = '<%=Session["G_user"] %>';
                    rows[i].Updater = '<%=Session["G_user"] %>';
                    if (json == "")
                        json = jsonToStr(rows[i]);
                    else
                        json = json + "," + jsonToStr(rows[i]);
                }
                json = "[" + json + "]";

                var SubQuery = encodeURIComponent(json);
                data = { Action: 'CreateOutStock',SubJson: SubQuery };
                $.post(OtherUrl, data, function (result) {
                    if (result.status == 1) {
                        $('#AddWin').window('close');

                    } else {
                        $.messager.alert('错误', result.msg, 'error');
                    }
                }, 'json');
            }

        }
        </script>
</head>
<body bgcolor="#F8FCFF" style="margin-top:30px;">
    <form id="form1" runat="server">
         <input type="hidden" runat="server" id="hdnMsg" /> 
         <input type="hidden" runat="server" id="hdnProduct" /> 
         <input type="hidden" runat="server" id="hdnTask" />
    </form>
     
     <div id="AddWin" class="easyui-dialog"  style="width:800px;height:500px; padding: 5px 5px"
        data-options="closed:true,buttons:'#AddWinBtn',modal:true"> 
       <table id="dgSub"  class="easyui-datagrid" data-options="loadMsg: '正在加载数据，请稍等...',fit:true, rownumbers:true,pagination:true,pageSize:PageSize, pageList:[15, 20, 30, 50],method:'post',striped:true,fitcolumns:true">
            <thead>
		        <tr>
                    <th data-options="field:'',checkbox:true"></th> 
                    <th data-options="field:'ProductCode',width:100">产品编号</th>
                    <th data-options="field:'ProductName',width:120">产品名称</th>
                    <th data-options="field:'BatchNo',width:100">批次</th>
                    <th data-options="field:'SectionName',width:110">阶段</th>
                    <th data-options="field:'Qty',width:80">托盘数量</th>
                </tr> 
            </thead>               
        </table>
        
         
    </div>
    <div id="AddWinBtn">
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="CreateOutStock()">生成出库单</a>
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#AddWin').dialog('close')">取消</a>
    </div>
</body>
</html>
 