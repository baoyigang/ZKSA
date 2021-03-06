﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutStocks.aspx.cs" Inherits="WebUI_OutStock_OutStocks" %>

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

        //       $("input",$("#loginName").next("span")).blur(function(){  
        //            alert("登录名已存在");
        //        })
        $(function () {
            $("input", $("#txtProductCode").next("span")).dblclick(function () {
                SelectWinShow('SelectWin', '产品资料--选择');
            });
        });

        

        var url = "../../Handler/BaseHandler.ashx";
        var OtherUrl = "../../Handler/OtherHandler.ashx";
        var SessionUrl = '<% =ResolveUrl("~/Login.aspx")%>';
        var FormID = "InStock";
        var BaseWhere = encodeURIComponent("BillID like 'OS%'");
        var blnChange = true;

        function getQueryParams(objname, queryParams) {
            var Where = "1=1 ";
            if (objname == "dg") {
                var BillID = $("#txtQueryBillID").textbox("getValue");
                var BillDate = $("#txtQueryBillDate").datebox("getValue");
                var Product = $("#txtQueryProduct").textbox("getValue");
                var BatchNo = $("#txtQueryBatchNo").textbox("getValue");

                if (BillID != "") {
                    Where += " and BillID like '%" + BillID + "%'";
                }
                if (BillDate != "") {
                    Where += " and CONVERT(nvarchar(10), BillDate,120) = '" + BillDate + "'";
                }
                if (Product != "") {
                    Where += " and (ProductCode like '%" + Product + "%' or ProductName like '%" + Product + "%')";
                }
                if (BatchNo != "") {
                    Where += " and BatchNo like '%" + BatchNo + "%'";
                }

                Where = BaseWhere + encodeURIComponent(" and " + Where);
            }
            else {
                var ProductCode = $("#txtSelectProductCode").textbox("getValue");
                var ProductName = $("#txtSelectProductName").textbox("getValue");
                var BatchNo = $("#txtSelectBatchNo").textbox("getValue");
                if (ProductCode != "") {
                    Where += " and ProductCode like '%" + ProductCode + "%'";
                }
                if (ProductName != "") {
                    Where += " and ProductName like '%" + ProductName + "%'";
                }
                if (BatchNo != "") {
                    Where += " and BatchNo like '%" + BatchNo + "%'";
                }
                Where = encodeURIComponent(Where);

            }
            queryParams.Where = Where;
            queryParams.t = new Date().getTime(); //使系统每次从后台执行动作，而不是使用缓存。
            return queryParams;

        }
        //添加管理员
        function Add() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("Instock", 1)) {
                alert("您没有新增权限！");
                return false;
            }

            $('#fm').form('clear');
            BindDropDownList();
            $('#txtBillDate').datebox('setValue', new Date().Format("yyyy/MM/dd"));
            $('#AddWin').dialog('open').dialog('setTitle', '出库单--新增');
            SetAutoCodeByTableName('txtID', 'OS', '1=1', 'WMS_Bill', $('#txtBillDate').datebox('getValue'));

            SetTextRead('txtProductName');
            SetTextRead('txtBatchNo');
            SetTextRead('txtSectionName');
            $('#txtPageState').val("Add");
            $('#txtBillTypeCode').val("020");
            $("#txtID").textbox('readonly', false);
            SetInitValue('<%=Session["G_user"] %>');
            SetInitColor();
        }
        //修改管理员
        function Edit() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("OutStock", 2)) {
                alert("您没有修改权限！");
                return false;
            }
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row.length == 0) {
                $.messager.alert("提示", "请选择要修改的行！", "info");
                return false;
            }
            var BillID = row.BillID;
            var state = GetFieldValue("WMS_BillMaster", "State", "BillID='" + BillID + "'");
            if (state >= 1) {
                var StateDes = GetFieldValue("View_WMS_BillMaster", "StateDesc", "BillID='" + BillID + "'");
                $.messager.alert("提示", BillID + "单号已" + StateDes + "，无法修改!", "info");
                return false;
            }

            if (row) {
                var data = { Action: 'FillDataTable', Comd: 'WMS.SelectBillMaster', Json: "[{\"{0}\": \"BillID='" + row.BillID + "'\"}]"  };
                $.post(url, data, function (result) {
                    blnChange = false;
                    var Product = result.rows[0];
                    $('#AddWin').dialog('open').dialog('setTitle', '出库单--编辑');
                    $('#fm').form('load', Product);
                    blnChange = true;
                    BindDropDownList();
                }, 'json');
            }
            SetTextRead('txtProductName');
            SetTextRead('txtBatchNo');
            SetTextRead('txtSectionName');
            $('#txtPageState').val("Edit");
            $('#txtBillTypeCode').val("020");
            $("#txtID").textbox("readonly", true);
            SetInitColor();
        }
        //绑定下拉控件
        function BindDropDownList() {
//            var Product = $("#txtProductCode").textbox('getValue');
//            var data = { Action: 'FillDataTable', Comd: 'cmd.SelectProductDetail', Where: encodeURIComponent("ProductCode='" + Product + "'") };
//            BindComboList(data, 'ddlSectionID', 'RowID', 'SectionName');
        }
        
        //保存信息
        function Save() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!$("#fm").form('validate')) {
                return false;
            }
            var query = createParam();
            var test = $('#txtPageState').val();
            var data;
            if (test == "Add") {
                //判断单号是否存在
                if (HasExists('WMS_Bill', "BillID='" + $('#txtID').textbox('getValue') + "'", '出库单号已经存在，请重新修改！'))
                    return false;
                data = { Action: 'CreateOutStock', SubJson: query };
                $.post(OtherUrl, data, function (result) {
                    if (result.status == 1) {
                        ReloadGrid('dg');
                        $('#AddWin').window('close');

                    } else {
                        $.messager.alert('错误', result.msg, 'error');
                    }
                }, 'json');

            }
            else {
                var data = { Action: 'Delete', FormID: FormID, Comd: 'WMS.DeleteOutStock', json: "'"+ $('#txtID').textbox('getValue') +"'" };
                $.post(url, data, function (result) {
                    if (result.status == 1) {
                        debugger;
                        var Adddata = { Action: 'CreateOutStock', SubJson: query };
                        $.post(OtherUrl, Adddata, function (result) {
                            if (result.status == 1) {
                                ReloadGrid('dg');
                                $('#AddWin').window('close');

                            } else {
                                $.messager.alert('错误', result.msg, 'error');
                            }
                        }, 'json');

                    } else {
                        $.messager.alert('错误', result.msg, 'error');
                    }
                }, 'json');
            }
        }
        //删除管理员
        function Delete() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("OutStock", 3)) {
                alert("您没有删除权限！");
                return false;
            }
            var checkedItems = $('#dg').datagrid('getChecked');

            if (checkedItems == null || checkedItems.length == 0) {
                $.messager.alert("提示", "请选择要删除的行！", "info");
                return false;
            }
            if (checkedItems) {
                $.messager.confirm('提示', '你确定要删除吗？', function (r) {
                    if (r) {
                        var deleteCode = [];
                        var blnUsed = false;
                        $.each(checkedItems, function (index, item) {
                            var StateDes = GetFieldValue("View_WMS_BillMaster", "StateDesc", "BillID='" + item.BillID + "'");
                            if (HasExists('WMS_Bill', "BillID='" + item.BillID + "'and State!=0", "出库单号 " + item.BillID + "已" + StateDes + ",无法删除！")) {
                                blnUsed = true;
                            }
                            deleteCode.push(item.BillID);
                        });
                        if (blnUsed)
                            return false;
                        var data = { Action: 'Delete', FormID: FormID, Comd: 'WMS.DeleteOutStock', json: "'" + deleteCode.join("','") + "'" };
                        $.post(url, data, function (result) {
                            if (result.status == 1) {
                                ReloadGrid('dg');


                            } else {
                                $.messager.alert('错误', result.msg, 'error');
                            }
                        }, 'json');

                    }
                });
            }
        }
        function CheckBill() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("InStock", 5)) {
                alert("您没有审核权限！");
                return false;
            }

            var checkedItems = $('#dg').datagrid('getChecked');

            if (checkedItems == null || checkedItems.length == 0) {
                $.messager.alert("提示", "请选择要[审核]的行！", "info");
                return false;
            }
            if (checkedItems) {
                var checkCode = [];
                var blnUsed = false;
                $.each(checkedItems, function (index, item) {
                    var BillID = item.BillID;
                    var state = GetFieldValue("View_WMS_BillMaster", "State", encodeURIComponent("BillID='" + BillID + "'"));
                    if (state == 1) {
                        $.messager.alert("提示", BillID + "单号已审核!", "info");
                        blnUsed = true;
                    }
                    if (state > 1) {
                        var StateDes = GetFieldValue("View_WMS_BillMaster", "StateDesc", "BillID='" + BillID + "'");
                        $.messager.alert("提示", BillID + "单号已" + StateDes + "，无法再审核!", "info");
                        blnUsed = true;
                    }
                    checkCode.push(item.BillID);
                });
                if (blnUsed)
                    return false;
                var data = { Action: 'CheckBill', FormID: FormID, Comd: 'WMS.UpdateCheckBillMaster', json: "'" + checkCode.join("','") + "'" };
                $.post(url, data, function (result) {
                    if (result.status == 1) {
                        ReloadGrid('dg');
                    } else {
                        $.messager.alert('错误', result.msg, 'error');
                    }
                }, 'json');
            }
        }
        function UnCheckBill() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("InStock", 6)) {
                alert("您没有取消审核权限！");
                return false;
            }

            var checkedItems = $('#dg').datagrid('getChecked');

            if (checkedItems == null || checkedItems.length == 0) {
                $.messager.alert("提示", "请选择要[取消审核]的行！", "info");
                return false;
            }
            if (checkedItems) {
                var checkCode = [];
                var blnUsed = false;
                $.each(checkedItems, function (index, item) {
                    var BillID = item.BillID;
                    var state = GetFieldValue("View_WMS_BillMaster", "State", encodeURIComponent("BillID='" + BillID + "'"));
                    if (state == 0) {
                        $.messager.alert("提示", BillID + "单号未审核!", "info");
                        blnUsed = true;
                    }
                    if (state > 1) {
                        var StateDes = GetFieldValue("View_WMS_BillMaster", "StateDesc", "BillID='" + BillID + "'");
                        $.messager.alert("提示", BillID + "单号已" + StateDes + "，无法再取消审核!", "info");
                        blnUsed = true;
                    }
                    checkCode.push(item.BillID);
                });
                if (blnUsed)
                    return false;
                var data = { Action: 'CancelCheck', FormID: FormID, Comd: 'WMS.UpdateCheckBillMaster', json: "'" + checkCode.join("','") + "'" };
                $.post(url, data, function (result) {
                    if (result.status == 1) {
                        ReloadGrid('dg');
                    } else {
                        $.messager.alert('错误', result.msg, 'error');
                    }
                }, 'json');
            }
        }
        function ExecTask() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("OutStock", 7)) {
                alert("您没有出库作业权限！");
                return false;
            }
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row.length == 0) {
                $.messager.alert("提示", "请选择要入库作业的行！", "info");
                return false;
            }
            var BillID = row.BillID;
            var state = GetFieldValue("View_WMS_BillMaster", "State", encodeURIComponent("BillID='" + BillID + "'"));
            if (state == 0) {
                $.messager.alert("提示", BillID + "单号还未审核，不能进行出库作业!", "info");
                return false;
            }
            if (state > 1) {
                var StateDes = GetFieldValue("View_WMS_BillMaster", "StateDesc", "BillID='" + BillID + "'");
                $.messager.alert("提示", BillID + "单号已" + StateDes + "，无法再进行出库作业!", "info");
                return false;
            }
            var data = { Action: 'ExecTask', FormID: FormID, Comd: 'WMS.SpOutStockTask', json: BillID };
            $.post(url, data, function (result) {
                if (result.status == 1) {
                    ReloadGrid('dg');
                } else {
                    $.messager.alert('错误', result.msg, 'error');
                }
            }, 'json');

        }
        function CancelTask() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("InStock", 8)) {
                alert("您没有取消作业权限！");
                return false;
            }
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row.length == 0) {
                $.messager.alert("提示", "请选择要入库作业的行！", "info");
                return false;
            }
            var BillID = row.BillID;
            if (state > 2) {
                var StateDes = GetFieldValue("View_WMS_BillMaster", "StateDesc", "BillID='" + BillID + "'");
                $.messager.alert("提示", BillID + "单号已" + StateDes + "，不能再进行取消作业。", "info");
                return false;
            }
            if (state < 2) {
                $.messager.alert("提示", BillID + "单号还未作业，不能进行取消作业。", "info");
                return false;
            }
            var data = { Action: 'CancelTask', FormID: FormID, Comd: 'WMS.SpCancelInstockTask', json: BillID };
            $.post(url, data, function (result) {
                if (result.status == 1) {
                    ReloadGrid('dg');
                } else {
                    $.messager.alert('错误', result.msg, 'error');
                }
            }, 'json');

        }



        function BindSelectUrl(objName) {

            var Comd = "WMS.SelectProductInStock";
            var AreaCode = '';
            $('#dgSelect').datagrid({
                url: '../../Handler/BaseHandler.ashx?Action=PageDate&Comd=' + Comd,
                pageNumber: 1,
                queryParams: { Where: encodeURIComponent("1=1 ") }
            });
        }

        function CheckRow(rowIndex, rowData) {
            CheckSelectRow('dg', rowIndex, rowData);
        }

        //绑定详细表
        function getDetail(index, data) {
            var selectdata = data;
            if (selectdata) {
                $('#dgSub').datagrid({
                    url: '../../Handler/BaseHandler.ashx?Action=PageDate&Comd=WMS.SelectBillDetail',
                    queryParams: { Where: encodeURIComponent("BillID='" + selectdata.BillID + "'") }
                });
            }
        }
        
        function GetProduct(newValue, oldValue) {
            if (!blnChange)
                return;
            var row = GetFieldValue("VCMD_ProductInStock", "*", encodeURIComponent("ProductCode='" + newValue + "'"));
            if (row.length>0) {
                $("#txtProductName").textbox('setValue', row[0].ProductName);
                $("#txtBatchNo").textbox('setValue', row[0].BatchNo);
                $("#txtSectionID").val(row[0].SectionID);
                $("#txtSectionName").textbox('setValue', row[0].SectionName);
            }
            else {
               
                $("#txtProductCode").textbox('setValue', '');
                $("#txtProductName").textbox('setValue', '');
                $("#txtBatchNo").textbox('setValue', '');
                $("#txtSectionID").val('');
                $("#txtSectionName").textbox('setValue', '');
               
                $('#txtProductCode').next('span').find('input').focus();
               

            }

        }
        function AddRow(ObjName, RowData) {
            $("#txtProductCode").textbox('setValue', RowData.ProductCode);
            $("#txtProductName").textbox('setValue', RowData.ProductName);
            $("#txtBatchNo").textbox('setValue', RowData.BatchNo);
            $("#txtSectionID").val(RowData.SectionID);
            $("#txtSectionName").textbox('setValue', RowData.SectionName);
        }
 </script>  
</head>
<body>
     <div class="easyui-layout" data-options="fit:true">
        <div data-options="region:'north',split:true" style="height:300px;">
              <table id="dg"  class="easyui-datagrid" 
                             data-options="loadMsg: '正在加载数据，请稍等...',fit:true, rownumbers:true,url:'../../Handler/BaseHandler.ashx?Action=PageDate&FormID=InStock',queryParams:{Where:BaseWhere},
                             pagination:true,pageSize:PageSize, pageList:[15, 20, 30, 50],method:'post',striped:true,fitcolumns:true,toolbar:'#tb',onLoadSuccess: function(data){ 
                             $('#dg').datagrid('selectRow',0);},singleSelect:true,selectOnCheck:false,checkOnSelect:false,onSelect:getDetail,onCheck:CheckRow,onUncheck:CheckRow"> 
                 <thead data-options="frozen:true">
			         <tr>
                        <th data-options="field:'',checkbox:true"></th> 
		                <th data-options="field:'BillID',width:110">出库单号</th>
                        <th data-options="field:'BillDate',width:90">日期</th>
                        <th data-options="field:'StateDesc',width:70">状态</th>
                        <th data-options="field:'ProductCode',width:90">产品编号</th>
                        <th data-options="field:'ProductName',width:150">产品名称</th>
                        <th data-options="field:'BatchNo',width:80">批次</th>
                        <th data-options="field:'SectionName',width:80">阶段</th>
                        <th data-options="field:'PalletQty',width:70">托盘数</th>
                        <th data-options="field:'Qty',width:70">产品数量</th>
                     </tr>
                 </thead>
                 <thead>
			         <tr>
                        <th data-options="field:'Memo',width:100">备注</th>
                        <th data-options="field:'Creator',width:80">建单人员</th>
                        <th data-options="field:'CreateDate',width:160">建单日期</th>
                        <th data-options="field:'Updater',width:80">修改人员</th>
                        <th data-options="field:'UpdateDate',width:160">修改日期</th>
                        <th data-options="field:'Tasker',width:80">作业人员</th>
                        <th data-options="field:'TaskDate',width:160">作业日期</th>
		            </tr>
                </thead>
            </table>
        </div>
        <div data-options="region:'center',split:true,title:'入库任务明细'">
            <table id="dgSub"  class="easyui-datagrid" 
            data-options="loadMsg: '正在加载数据，请稍等...',fit:true, rownumbers:true,pagination:true,pageSize:PageSize, pageList:[15, 20, 30, 50],method:'post',striped:true,fitcolumns:true">
                 <thead>
			        <tr>
                        <th data-options="field:'TaskNo',width:100">任务号</th>
                        <th data-options="field:'StateDesc',width:80">状态</th>
                        <th data-options="field:'CellCode',width:100">货位</th>
                        <th data-options="field:'Quantity',width:100">数量</th>
                        <th data-options="field:'StartDate',width:120">开始时间</th>
                        <th data-options="field:'FinishDate',width:120">结束时间</th>
                         
                    </tr>
                </thead>                
            </table>
        </div>
    </div>
    <div id="tb" style="padding: 5px; height: auto">  
        <table style="width:100%" >
            <tr>
                 <td>
                 单号
                <input id="txtQueryBillID" class="easyui-textbox" style="width:100px" />
                日期
                <input id="txtQueryBillDate" class="easyui-datebox" style="width:120px"/>
                产品
                <input id="txtQueryProduct" class="easyui-textbox" style="width:100px"/>
                批次
                <input id="txtQueryBatchNo" class="easyui-textbox" style="width:100px"/>
                <a  href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="ReloadGrid('dg')" >查询</a>
                </td>
                <td style="width:*" align="right">
                    <a href="javascript:void(0)" onclick="Add()"   class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">新增</a> 
                   <%-- <a href="javascript:void(0)" onclick="Edit()" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true">修改</a>--%>
                    <a href="javascript:void(0)" onclick="Delete()" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">删除</a>
                    <a href="javascript:void(0)"  onclick="CheckBill()" class="easyui-linkbutton" data-options="iconCls:'icon-man',plain:true">审核</a> 
                    <a href="javascript:void(0)" onclick="UnCheckBill()" class="easyui-linkbutton" data-options="iconCls:'icon-undo',plain:true">取消审核</a> 
                    <a href="javascript:void(0)" onclick="ExecTask()"  class="easyui-linkbutton" data-options="iconCls:'icon-instock',plain:true">出库作业</a>
                    <a href="javascript:void(0)" onclick="CancelTask()" class="easyui-linkbutton" data-options="iconCls:'icon-clear',plain:true">取消作业</a>
                    <a href="javascript:void(0)" onclick="Exit()"   class="easyui-linkbutton" data-options="iconCls:'icon-no',plain:true">离开</a>
                </td>
            </tr>
        </table>
   </div>
      <%-- 弹出操作框--%>
    <div id="AddWin" class="easyui-dialog" style="width: 630px; height: auto; padding: 5px 5px"
        data-options="closed:true,buttons:'#AddWinBtn',modal:true"> 
        <form id="fm" method="post">
              <table id="Table1" class="maintable"  width="100%" align="center">			
				<tr>
                    <td align="center" class="musttitle"style="width:90px">
                        日期 </td>
                    <td style="width: 210px" >
                        
                        &nbsp;<input id="txtBillDate" name="BillDate" class="easyui-datebox" data-options="required:true,editable:false,onSelect:function(date){ SetAutoCodeByTableName('txtID', 'OS', '1=1', 'WMS_Bill', date.Format('yy/MM/dd'));}" style="width:180px"/> 
                        <input name="PageState" id="txtPageState" type="hidden" />
                        
                    </td>
                   <td align="center" class="musttitle"style="width:90px">
                        入库单号
                   </td>
                   <td  colspan="2">
                        &nbsp;<input id="txtID" name="BillID" class="easyui-textbox" data-options="editable:false,required:true" maxlength="20" style="width:186px"/>
                  </td>
                </tr>
                <tr>
                    
                    <td align="center" class="musttitle" style="width:90px">
                            产品编号
                    </td>
                    <td colspan="4" >
                            &nbsp;<input 
                                id="txtProductCode" name="ProductCode" class="easyui-textbox" data-options="required:true,onChange: GetProduct " style="width:180px"/>
                            <input id="txtProductName" name="ProductName" class="easyui-textbox" 
                                data-options="editable:false" maxlength="30" 
                                style="width:306px; margin-left: 2px;"/>
                                
                    </td>
                     
                     
                    
                    
                </tr>
                <tr>
                    <td align="center"  class="musttitle"  >
                            批次
                    </td>
                    <td>
                        &nbsp;<input id="txtBatchNo" name="BatchNo" class="easyui-textbox"  maxlength="30"  data-options="editable:false" style="width:180px"/>
                               
                    </td>
                               
                    <td align="center" class="musttitle"  >
                        阶段</td>
                    <td colspan="2">
                       &nbsp;<input id="txtSectionName" name="SectionName" class="easyui-textbox" data-options="editable:false" maxlength="50" style="width: 186px;" />
                        <input type="hidden" id="txtSectionID" name="SectionID" />
                        <input type="hidden" id="txtBillTypeCode" name="BillTypeCode" />
                    </td>
                </tr>
              
                <tr style=" height:80px">
                    <td align="center"  class="smalltitle" style="width:90px;height:80px;">
                        备注
                    </td>
                    <td colspan="4" style="height:80px;">
                       &nbsp;<input id="txtMemo" name="Memo" 
                            class="easyui-textbox" data-options="multiline:true" 
                            style="width:486px; height:72px"/>

                    </td>
                </tr>
                 
                 <tr>
                  <td align="center"  class="smalltitle"style="width:90px">
                        建单人员
                  </td> 
                  <td   width="210px"> 
                    &nbsp;<input id="txtCreator" name="Creator" class="easyui-textbox" data-options="editable:false"  style="width:180px"/>
                  </td>
                    <td align="center" class="smalltitle"style="width:90px">
                        建单日期
                  </td> 
                  <td   colspan="2">
                  &nbsp;<input id="txtCreateDate" name="CreateDate" class="easyui-textbox" data-options="editable:false"  style="width:188px"/>
                  </td>
                 </tr>
                 <tr>
                   
                  <td align="center"  class="smalltitle"style="width:90px">
                        修改人员
                  </td> 
                  <td   width="210px"> 
                    &nbsp;<input id="txtUpdater" name="Updater" class="easyui-textbox" data-options="editable:false"  style="width:180px"/>
                  </td>
                  <td align="center"  class="smalltitle"style="width:90px">
                        修改日期
                  </td> 
                  <td  colspan="2">
                    &nbsp;<input id="txtUpdateDate" name="UpdateDate" class="easyui-textbox" data-options="editable:false" style="width:188px"/>
                  </td>
                  
                </tr>	
               
                	
		</table>
        </form>
    </div>
    <div id="AddWinBtn">
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="Save()">保存</a>
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#AddWin').dialog('close')">关闭</a>
    </div>

     <div id="SelectWin" style="width:600px;height:500px">
       <table id="dgSelect" class="easyui-datagrid"
            data-options="loadMsg: '正在加载数据，请稍等...',fit:true, rownumbers:true,
                         pagination:true,pageSize:PageSize, pageList:[15, 20, 30, 50],method:'post',striped:true,fitcolumns:true,toolbar:'#tbSelect',singleSelect:true,selectOnCheck:true,checkOnSelect:true,onCheck:SelectSingleCheckRow,onUncheck:SelectSingleUnCheckRow,onLoadSuccess:SelectLoadSelectSuccess,onDblClickRow:DblClickRow"> 
            <thead>
                    <tr>
                        <th data-options="field:'',checkbox:true"></th> 
                        <th data-options="field:'ProductCode',width:80">产品编号</th>
                        <th data-options="field:'ProductName',width:160">品名</th>
                        <th data-options="field:'BatchNo',width:80">批次</th>
                        <th data-options="field:'SectionName',width:80">阶段</th>
                        <th data-options="field:'Qty',width:80">托盘数</th>
                    </tr>
            </thead>            
        </table>
        <div id="tbSelect" style="padding:5px;height:auto">
           <table>
                <tr>
                     <td>
                        产品编号
                        <input id="txtSelectProductCode" class ="easyui-textbox" style="width: 80px" /> 
                        品名
                        <input id="txtSelectProductName" class ="easyui-textbox" style="width: 80px" /> 
                        批次
                        <input id="txtSelectBatchNo" class="easyui-textbox" style="width: 80px" />  
                        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="ReloadGrid('dgSelect')" >查询</a> 
                    </td>
                    <td>
                         <a href="javascript:void(0)"onclick="closeSelectWin()" class="easyui-linkbutton" data-options="iconCls:'icon-return'">取回</a>  
                    </td>
                </tr>          
           </table>
        </div>
    </div>

</body>
</html>