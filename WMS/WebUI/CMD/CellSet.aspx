<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CellSet.aspx.cs" Inherits="WebUI_CMD_CellSet" %>

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
        var url = "../../Handler/BaseHandler.ashx";
        var SessionUrl = '<% =ResolveUrl("~/Login.aspx")%>';
        var FormID = "Cell";
        var ActiveValue = [{ ActiveCode: '1', ActiveText: '无异常' }, { ActiveCode: '0', ActiveText: '异常'}];
        var LockValue = [{ LockCode: '0', LockText: '未锁定' }, { LockCode: '1', LockText: '锁定'}];
        function AddRow(ObjName, RowData) {
            $("#txtEditProductCode").textbox('setValue', RowData.ProductCode);
            $("#txtEditProductName").textbox('setValue', RowData.ProductName);
            BindDropDownList();
        }
        function getQueryParams(objname, queryParams) {
            var Where = "1=1 ";
            var CellCode = $("#txtCellCode").textbox("getValue");
            var RegionName = $("#txtRegionName").textbox("getValue");
            var Pai = $("#txtPai").textbox("getValue");
            var Lie = $("#txtLie").textbox("getValue");
            var Ceng = $("#txtCeng").textbox("getValue");
            var cellValue = $("#comboCell").is(':checked');
            if (CellCode != "") {
                Where += " and c.CellCode like '%" + CellCode + "%'";
            }
            if (RegionName != "") {
                Where += " and RegionName like '%" + RegionName + "%'";
            }
            if (Pai != "") {
                Where += " and SUBSTRING(c.cellcode,1,3) like '%" + Pai + "%'";
            }
            if (Lie != "") {
                Where += " and c.CellColumn = " + Lie + "";
            }
            if (Ceng != "") {
                Where += " and  c.CellRow = " + Ceng + "";
            }
            if (cellValue) {
                Where += " and PalletBarCode =''";
            }
            queryParams.Where = encodeURIComponent(Where);
            //queryParams.t = new Date().getTime(); //使系统每次从后台执行动作，而不是使用缓存。
            return queryParams;

        }
        //添加管理员 
        function Add() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("Cell", 2)) {
                alert("您没有新增权限！");
                return false;
            }
            $('#ddlEditRowID').combobox({
                data: [],
                valueField: 'RowID',
                textField: 'SectionName'
            });


            $('#Form1').form('clear');
            var data = { Action: 'FillDataTable', Comd: 'cmd.SelectAreaEdit', Json: "[{\"{0}\": \"1=1\",\"{1}\":\"1\"}]" };
            BindComboList(data, 'ddlAreaName', 'AreaCode', 'AreaName');
        

            $('#AddCell').dialog('open').dialog('setTitle', '库位--新增');
            $('#txtPageState').val("AddCell");
            $("#txtEditCellCode").textbox('readonly', false);
            $('#txtEditCellCode').textbox().next('span').find('input').focus();
            SetInitValue('<%=Session["G_user"] %>');
            SetInitColor();
        }
        //删除管理员
        function Delete() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("Cell", 2)) {
                alert("您没有删除权限！");
                return false;
            }
            var checkedItems = $('#dg').datagrid('getChecked');
            if (checkedItems.length == 0) {
                $.messager.alert("提示", "请选择要删除的行！", "info");
                return false;
            }
            if (checkedItems) {
                $.messager.confirm('提示', '你确定要删除吗？', function (r) {
                    if (r) {
                        var deleteCode = [];
                        var blnUsed = false;
                        $.each(checkedItems, function (index, item) {
//                            if (HasExists('Wms_Pallet', "CellCode='" + item.CellCode + "'", "货位编号 " + item.CellCode + " 已经被其它单据使用，无法删除！"))
//                                blnUsed = true;
                            deleteCode.push(item.CellCode);
                        });
                        if (blnUsed)
                            return false;
                        var data = { Action: 'Delete', FormID: FormID, Comd: 'cmd.DeleteCellEdit', json: "'" + deleteCode.join("','") + "'" };
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
        //修改单个货位
        function EditCell() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("Cell", 2)) {
                alert("您没有修改权限！");
                return false;
            }
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row.length == 0) {
                $.messager.alert("提示", "请选择要修改的行！", "info");
                return false;
            }
            $("#txtEditCellCode").textbox('readonly', true);
            $("#ddlAreaName").combobox({ disabled: false });
            $("#ddlRegionName").combobox({ disabled: false });
            BindCellDrop();
            if (row) {
                $('#Form1').form('clear');
                var data = { Action: 'FillDataTable', Comd: 'CMD.SelectCellEdit', Json: "[{\"{0}\": \"c.CellCode='" + row.CellCode + "'\",\"{1}\":\"1\"}]" };

                    if (row.PalletBarCode != '') {
                        $("#ddlAreaName").combobox({ disabled: true });
                        $("#ddlRegionName").combobox({ disabled: true });
                    }
                    $.post(url, data, function (result) {
                        var Product = result.rows[0];
                        $('#AddCell').dialog('open').dialog('setTitle', '库位--编辑');
                        var eadata = { Action: 'FillDataTable', Comd: 'cmd.SelectRegionEdit', Json: "[{\"{0}\": \"a.AreaCode='" + Product.AreaCode + "'\",\"{1}\":\"1\"}]" };
                        BindComboList(eadata, 'ddlRegionName', 'RegionCode', 'RegionName');
                        $('#Form1').form('load', Product);
                    }, 'json');
            }
            
            $('#txtPageState').val("EditCell");
            $('#txtFlag').val("1");
            $("#txtID").textbox("readonly", true);
            SetInitColor();
        }
        //批量修改
        function Edit() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("Cell", 2)) {
                alert("您没有修改权限！");
                return false;
            }
            var checkedItems = $('#dg').datagrid('getChecked');
            if (checkedItems == null || checkedItems.length == 0) {
                $.messager.alert("提示", "请选择要修改的行！", "info");
                return false;
            }
            var checkedItems = $('#dg').datagrid('getChecked');
            var RegionCode = checkedItems[0].RegionCode;
            var blnUsed = false;
            var updateCode = [];
            var blnUsed = false;
            $.each(checkedItems, function (index, item) {
                if (item.PalletBarCode != "") {
                    alert("不是空货位，无法修改有货货位的库区");
                    blnUsed = true;
                    return false;
                }

                if (item.RegionCode != RegionCode) {
                    alert("请修改相同库区库位");
                    blnUsed = true;
                    return false;
                }
            });
            if (blnUsed) {
                return false;
            }
            BindDropDownList();
             if (checkedItems) {
                $('#AddWin').dialog('open').dialog('setTitle', '库位设置--编辑');

            }
            $("#SelectAreaName").combobox({
                    onSelect: function (record) {
                        var edata = { Action: 'FillDataTable', Comd: 'cmd.SelectRegionEdit', Json: "[{\"{0}\": \"a.AreaCode='" + $('#SelectAreaName').combobox('getValue') + "'\",\"{1}\":\"1\"}]" };
                    BindComboList(edata,'SelectRegionName','RegionCode','RegionName')
                    }
            });
//            $('#SelectRegionName').combobox('setValue', '');
            aeAreaCode = checkedItems[0].AreaCode;
            var aedata = { Action: 'FillDataTable', Comd: 'cmd.SelectRegionEdit', Json: "[{\"{0}\": \"a.AreaCode='" + aeAreaCode + "'\",\"{1}\":\"1\"}]" };
            BindComboList(aedata, 'SelectRegionName', 'RegionCode', 'RegionName')
            $('#fm').form('load', checkedItems[0]);
            $('#txtPageState').val("Edit");
            $("#txtID").textbox("readonly", true);
            SetInitColor();
        }
        //绑定下拉控件
        function BindDropDownList() {
            var data = { Action: 'FillDataTable', Comd: 'cmd.SelectAreaEdit', Json: "[{\"{0}\": \"1=1\",\"{1}\":\"1\"}]" };
            BindComboList(data, 'SelectAreaName', 'AreaCode', 'AreaName');
        }
        function BindCellDrop() {
            var data = { Action: 'FillDataTable', Comd: 'cmd.SelectAreaEdit', Json: "[{\"{0}\": \"1=1\",\"{1}\":\"1\"}]" };
            BindComboList(data, 'ddlAreaName', 'AreaCode', 'AreaName');

            var edata = { Action: 'FillDataTable', Comd: 'cmd.SelectRowID', Json: "[{\"{0}\": \"ProductCode='" + $('#dg').datagrid('getSelected').ProductCode + "'\"}]" };
//            BindComboList(edata, 'ddlEditRowID', 'RowID', 'SectionName','0');
            $.ajax({
                type: "post",
                url: BaseUrl,
                data: edata,
                //contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (json) {
                    $('#ddlEditRowID').combobox({
                        data: json.rows,
                        valueField: 'RowID',
                        textField: 'SectionName',
                        loadFilter: function (data) {
                            data.unshift({ RowID: '0', SectionName: '' });
                            return data;
                        }
                    });
                },
                error: function (msg) {
                    alert(msg);
                }
            });  
                   
        }
        //修改库区 保存信息
        function Save() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!$("#fm").form('validate')) {
                return false;
            }
            var checkedItems = $('#dg').datagrid('getChecked');
            var query = createParam();
            var js = "[{AreaCode:" + $("#SelectAreaName").combobox("getValue") + "," + "RegionCode:" + $("#SelectRegionName").combobox("getValue") + ",";
            var updateCode = [];
            var blnUsed = false;
            $.each(checkedItems, function (index, item) {
                updateCode.push(item.CellCode);
            });
            if (HasExists('CMD_Cell', "CellCode in ('" + updateCode.join("','") +"') and PalletBarCode!=''", '所选货位中存在非空货位，请重新修改！')) {
                return false;
            }
            if (blnUsed)
                return false;
            var data = { Action: 'Edit', Comd: 'Cmd.UpdateCellEdit', json: js + "\"{0}\":'" + updateCode.join("','") + "'}]" };
                $.post(url, data, function (result) {
                    if (result.status == 1) {
                        ReloadGrid('dg');
                        $('#AddWin').window('close');

                    } else {
                        $.messager.alert('错误', result.msg, 'error');
                    }
                }, 'json');
            }
            //货位 保存信息
            function SaveCell() {
                if (SessionTimeOut(SessionUrl)) {
                    return false;
                }
                if (!$("#Form1").form('validate')) {
                    return false;
                }
                var test = $('#txtPageState').val();
                if (test == 'AddCell') {

                    if ($("#txtEditProductCode").textbox('getValue') == "") {
                        $("#txtEditBatchNo").textbox('setValue', '');
                        $("#ddlEditRowID").combobox('setValue', '');
                        $("#txtEditIndate").textbox('setValue', '');
                    }
//                    var paramjs = "{\"CellCode\":\"" + $("#txtEditCellCode").textbox("getValue") + "\"," + "\"AreaCode\":\"" + $("#ddlAreaName").combobox("getValue") + "\"," + "\"RegionCode\":\"" + $("#ddlRegionName").combobox("getValue") + "\"," + "\"IsActive\":\"" + $("#ddlEditActive").combobox("getValue") + "\"," + "\"IsLock\":\"" + $("#ddlEditLock").combobox("getValue") + "\"," + "\"CellName\":\"" + $("#txtEditCellName").textbox("getValue") + "\"," + "\"PalletBarCode\":\"" + $("#txtEditProductCode").textbox("getValue") + "\"," + "\"BatchNo\":\"" + $("#txtEditBatchNo").textbox("getValue") + "\"," + "\"Qty\":\"" + $("#txtEditPreQty").textbox("getValue") + "\"," + "\"SectionID\":\"" + $("#ddlEditRowID").combobox("getValue") + "\"," + "\"Indate\":\"" + $("#txtEditIndate").textbox("getValue") + "\","  + "\"Memo\":\"" + $("#txtEditMemo").textbox("getValue") + "\"," + "\"Indate\":\"" + $("#txtEditIndate").textbox("getValue") + "\"}";
                    var query = $("#Form1").serializeArray();
                    query = convertArray(query);
                    var ParamQuery = "[" + encodeURIComponent(jsonToStr(query)) + "]";

                    data = { Action: 'Add', Comd: 'Cmd.InsertCmdCell', json: ParamQuery };
                    $.post(url, data, function (result) {
                        if (result.status == 1) {
                            ReloadGrid('dg');
                            $('#AddCell').window('close');

                        } else {
                            $.messager.alert('错误', result.msg, 'error');
                        }
                    }, 'json');
                }
                else (test == 'EditCell')
                {
//                    var paramjs = "{\"CellCode\":\"" + $("#txtEditCellCode").textbox("getValue") + "\"," + "\"AreaCode\":\"" + $("#ddlAreaName").combobox("getValue") + "\"," + "\"RegionCode\":\"" + $("#ddlRegionName").combobox("getValue") + "\"," + "\"IsActive\":\"" + $("#ddlEditActive").combobox("getValue") + "\"," + "\"IsLock\":\"" + $("#ddlEditLock").combobox("getValue") + "\"," + "\"CellName\":\"" + $("#txtEditCellName").textbox("getValue") + "\"," + "\"Memo\":\"" + $("#txtEditMemo").textbox("getValue") + "\"," + "\"PalletBarCode\":\"" + $("#txtEditProductCode").textbox("getValue") + "\"," + "\"BatchNo\":\"" + $("#txtEditBatchNo").textbox("getValue") + "\"," + "\"Qty\":\"" + $("#txtEditPreQty").textbox("getValue") + "\"," + "\"SectionID\":\"" + $("#ddlEditRowID").combobox("getValue") + "\"," + "\"Indate\":\"" + $("#txtEditIndate").textbox("getValue") + "\"}";

//                    var ParamQuery = "[" + encodeURIComponent(paramjs) + "]";
                    if ($("#txtEditProductCode").textbox('getValue') == "") {
                        $("#txtEditBatchNo").textbox('setValue', '');
                        $("#ddlEditRowID").combobox('setValue', '');
                        $("#txtEditIndate").textbox('setValue', '');
                    }

                    var query = $("#Form1").serializeArray();
                    query = convertArray(query);
                    var ParamQuery = "[" + encodeURIComponent(jsonToStr(query)) + "]";

               
                        data = { Action: 'Edit', Comd: 'Cmd.UpdateCmdCell', json: ParamQuery };
                        $.post(url, data, function (result) {
                            if (result.status == 1) {
                                ReloadGrid('dg');
                                $('#AddCell').window('close');

                            } else {
                                $.messager.alert('错误', result.msg, 'error');
                            }
                        }, 'json');
                  
                }
        }
        function CheckRow(rowIndex, rowData) {
          //  CheckSelectRow('dg', rowIndex, rowData);
        }
        function BindProduct() {
            var edata = { Action: 'FillDataTable', Comd: 'cmd.SelectRowID', Json: "[{\"{0}\": \"ProductCode='" + $('#txtEditProductCode').textbox('getValue') + "'\"}]" };
            $.ajax({
                type: "post",
                url: BaseUrl,
                data: edata,
                //contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (json) {
                    $('#ddlEditRowID').combobox({
                        data: json.rows,
                        valueField: 'RowID',
                        textField: 'SectionName',
                        loadFilter: function (data) {
                            data.unshift({ RowID: '0', SectionName: '' });
                            return data;
                            }
                    });
                },
                error: function (msg) {
                    alert(msg);
                }
            }); 
        }
        //绑定产品名称
        var blnProductChange = false;
        function GetProduct(newValue, oldValue) {
            if (blnProductChange)
                return;
            var ProductName = GetFieldValue("CMD_Product", "ProductName", encodeURIComponent("ProductCode='" + newValue + "'"));
            if (ProductName != "") {
                $("#txtEditProductName").textbox('setValue', ProductName);
                BindProduct();
            }
            else {
                blnProductChange = true;
                $("#txtEditProductCode").textbox('setValue', '');
                $("#txtEditProductName").textbox('setValue', ProductName);
                $('#txtEditProductCode').next('span').find('input').focus();
                BindProduct();
                blnProductChange = false;

            }

        }

        function GetSection(record) {
            var SectionName = GetFieldValue("CMD_ProductDetail", "SectionName", encodeURIComponent("ProductCode='" + $("#txtEditProductCode").textbox('getValue') + "'" + " and RowID='" + $("#ddlEditRowID").combobox('getValue') + "'"));
        }

        //值转换
        function formatActive(value) {
            if (value == 0) {
                return '异常';
            }
            else {
                return '无异常';
            }
       }
       function formatLock(value) {
           if (value == 0) {
               return '未锁定';
            }
            else {
                return '锁定';
            }
        }
        function formatSet(value) {
            if (value == '') {
                return '未设置';
            }
            else {
                return value;
            }
        }
        function BindSelectUrl(objName) {

            var Comd = "CMD.SelectProduct";
            var AreaCode = '';
            $('#dgSelect').datagrid({
                url: '../../Handler/BaseHandler.ashx?Action=PageDate&Comd=' + Comd,
                pageNumber: 1,
                queryParams: { Where: encodeURIComponent("1=1 ") }
            });
        }
        $(function () {
            $("input", $("#txtEditProductCode").next("span")).dblclick(function () {
                SelectWinShow('SelectWin', '产品资料--选择');
            });

            $("#ddlAreaName").combobox({
                onSelect: function (record) {
                    var val = $('#ddlAreaName').combobox('getValue');
                    var eadata = { Action: 'FillDataTable', Comd: 'cmd.SelectRegionEdit', Json: "[{\"{0}\": \"a.AreaCode='" + val + "'\",\"{1}\":\"1\"}]" };
                    BindComboList(eadata, 'ddlRegionName', 'RegionCode', 'RegionName');
                }
            });
        })
    </script> 
</head>
<body class="easyui-layout">
    <table id="dg"  class="easyui-datagrid" 
        data-options="loadMsg: '正在加载数据，请稍等...',fit:true, rownumbers:true,url:'../../Handler/BaseHandler.ashx?Action=PageDate&FormID='+FormID,
                     pagination:true,pageSize:PageSize, pageList:[15, 20, 30, 50],method:'post',striped:true,fitcolumns:true,toolbar:'#tb',singleSelect:true,selectOnCheck:false,checkOnSelect:false,onCheck:CheckRow,onUncheck:CheckRow,onBeforeSortColumn:BeforeSortColumn,idField:'CellCode'"> 
        <thead>
		    <tr>
                <th data-options="field:'',checkbox:true"></th> 
		        <th data-options="field:'CellCode',width:100,sortable:true">货位编码</th>
                <th data-options="field:'CellName',width:100,sortable:true">货位名称</th>
                <th data-options="field:'IsActive',width:60,formatter:formatActive">异常</th>
                <th data-options="field:'IsLock',width:60,sortable:true,formatter:formatLock">锁定</th>
                <th data-options="field:'CellRow',width:50">层</th>
                <th data-options="field:'CellColumn',width:50">列</th>
                <th data-options="field:'Depth',width:50">深度</th>
                <th data-options="field:'Memo',width:100">备注</th>
                <th data-options="field:'AreaCode',width:80,formatter:formatSet">区域编码</th>
                <th data-options="field:'AreaName',width:120">区域名称</th>
                <th data-options="field:'RegionCode',width:80,sortable:true,formatter:formatSet">库区编码</th>
                <th data-options="field:'RegionName',width:120,sortable:true">库区名称</th>
                <th data-options="field:'PalletBarCode',width:120,sortable:true">产品编码</th>
                <th data-options="field:'ProductName',width:120,sortable:true">产品名称</th>
                <th data-options="field:'BatchNo',width:120,sortable:true">批次</th>
                <th data-options="field:'Qty',width:80,sortable:true">每盘数量</th>
                <th data-options="field:'SectionName',width:80,sortable:true">阶段</th>
                <th data-options="field:'InDate',width:180,sortable:true">入库日期</th>
		    </tr>
        </thead>
    </table>    
    <div id="tb" style="padding: 5px; height: auto">  
    
        <table style="width:100%" >
            <tr>
                <td>
                   货位编码
                    <input id="txtCellCode" class ="easyui-textbox" style="width: 100px" />  
                    库区名称
                    <input id="txtRegionName" class="easyui-textbox" style="width: 100px" />
                    &nbsp;&nbsp;
                    <input id="txtPai" class="easyui-textbox" style="width: 50px" />
                    排
                    <input id="txtLie" class="easyui-textbox" style="width: 50px" />
                    列
                    <input id="txtCeng" class="easyui-textbox" style="width: 50px" />
                    层
                    &nbsp;&nbsp;
                    <input id="comboCell" type="checkbox" style="width:20px;position: relative;top:2px"/>
                    空货位
                    &nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="ReloadGrid('dg')">查询</a> 
                </td>
                <td style="width:*" align="right">
                     <a href="javascript:void(0)" onclick="Add() " class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">新增库位</a>  
                     <a href="javascript:void(0)" onclick="EditCell() " class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true">修改库位</a>  
                     <a href="javascript:void(0)" onclick="Edit() " class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true">修改库区</a>
                     <a href="javascript:void(0)" onclick="Delete() " class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">删除库位</a>    
                     <a href="javascript:void(0)" onclick="Exit()" class="easyui-linkbutton" data-options="iconCls:'icon-no',plain:true">离开</a>
                </td>
            </tr>
        </table>
   </div>
      <%-- 弹出操作框--%>
    <div id="AddWin" class="easyui-dialog" style="width: 400px; height: auto; padding: 5px 5px"
        data-options="closed:true,buttons:'#AddWinBtn',modal:true"> 
        <form id="fm" method="post">
              <table id="Table1" class="maintable"  width="100%" align="center">			
				<tr>
                    
                     <td align="center" class="musttitle"style="width:90px">
                            区域名称
                    </td>
                    <td width="300px"> 
                        &nbsp;<input id="SelectAreaName" name="AreaCode" class="easyui-combobox" data-options="required:false,editable:false" maxlength="50" style="width:180px"/>
                    </td>
                    
                </tr>
                <tr>
                    <td align="center" class="musttitle"style="width:90px"  >
                           库区名称
                    </td>
                    <td width="300px"> 
                        &nbsp;<input id="SelectRegionName" name="RegionCode" class="easyui-combobox" data-options="required:false,editable:false,valueField:'RegionCode',textField:'RegionName'" maxlength="50" style="width:180px"/>
                    </td>
                </tr>
               
                	
		</table>
        </form>
    </div>
    <div id="AddWinBtn">
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="Save()">保存</a>
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#AddWin').dialog('close')">关闭</a>
    </div>
    <%-- 弹出操作框--%>
    <div id="AddCell" class="easyui-dialog" style="width: 600px; height: auto; padding: 5px 5px"
        data-options="closed:true,buttons:'#AddCellBtn',modal:true"> 
        <form id="Form1" method="post">
            <div>
                    <table id="Table2" class="grid maintable" style="table-layout:fixed;"  width="100%" align="center">			
				    <tr> 
                        <td align="center" class="musttitle"style="width:12%">
                            货位编码 </td>
                        <td style="width:33%" >
                        
                            &nbsp;<input id="txtEditCellCode" name="CellCode" class="easyui-textbox" data-options="required:true" style="width:160px"/> 
                            <input name="PageState" id="txtPageState" type="hidden" />
                        
                        </td>
                        <td align="center" class="musttitle"style="width:15%">
                                货位状态
                        </td>
                        <td  style="width:33%">
                                &nbsp;<input id="ddlEditActive" name="IsActive" 
                                    class="easyui-combobox" data-options="required:true,editable:false,valueField:'ActiveCode',textField:'ActiveText',data:ActiveValue" maxlength="20" style="width:160px"/>
                        </td>
                                                
                    </tr>
                    <tr> 
                        <td align="center" class="musttitle"style="width:15%"  >
                                货位锁定
                        </td>
                        <td  style="width:33%"> 
                            &nbsp;<input 
                                id="ddlEditLock" name="IsLock" class="easyui-combobox" 
                                data-options="required:true,editable:false,valueField:'LockCode',textField:'LockText',data:LockValue" maxlength="50" style="width:160px"/>
                        </td>
                        <td align="center" class="musttitle"style="width:15%">
                            货位名称 </td>
                        <td style="width:33%" >
                        
                            &nbsp;<input id="txtEditCellName" name="CellName" class="easyui-textbox" data-options="required:true" style="width:160px"/> 
                            <input name="PageState" id="Hidden1" type="hidden" />
                        
                        </td>
                   </tr>
                   <tr>
                        <td align="center" class="smalltitle"style="width:15%">
                                区域名称
                        </td>
                        <td  style="width:33%">
                                &nbsp;<input id="ddlAreaName" name="AreaCode" 
                                    class="easyui-combobox" data-options="editable:false" maxlength="20" style="width:160px"/>
                        </td>
                        <td align="center" class="smalltitle"style="width:15%"  >
                                库区名称
                        </td>
                        <td style="width:33%"> 
                            &nbsp;<input 
                                id="ddlRegionName" name="RegionCode" class="easyui-combobox" 
                                data-options="editable:false,valueField: 'RegionCode',textField: 'RegionName'" maxlength="50" style="width:160px"/>
                        </td>
                    </tr>
                    <tr> 
                        <td align="center" class="smalltitle"style="width:15%">
                            产品编号 </td>
                        <td colspan="3" >
                        
                            &nbsp;<input id="txtEditProductCode" name="PalletBarCode" class="easyui-textbox" data-options="onChange:GetProduct" style="width:160px"/> 
                            <input id="txtEditProductName" name="ProductName" class="easyui-textbox" data-options="disabled:true,editable:false" style="width:294px"/> 
                        
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="smalltitle"style="width:15%">
                                批次
                        </td>
                        <td  style="width:33%">
                                &nbsp;<input id="txtEditBatchNo" name="BatchNo" class="easyui-textbox"  maxlength="20" style="width:160px"/>
                        </td>
                        <td align="center" class="smalltitle"style="width:15%"  >
                                每盘数量
                        </td>
                        <td style="width:33%"> 
                            &nbsp;<input id="txtEditPreQty" name="Qty" class="easyui-numberbox" data-options="min:0,precision:0" maxlength="50" style="width:160px"/>
                        </td>
                        
                    </tr>
                    <tr> 
                        <td align="center" class="smalltitle"style="width:15%">
                            阶段 </td>
                        <td style="width:33%" >
                        
                            &nbsp;<input id="ddlEditRowID" name="SectionID" class="easyui-combobox" data-options="editable:false,onSelect:GetSection" style="width:160px"/> 
                        
                        </td>
                        <td align="center" class="smalltitle"style="width:15%"  >
                                入库日期
                        </td>
                        <td > 
                            &nbsp;<input  id="txtEditIndate" name="Indate" class="easyui-datebox" data-options="editable:false" style="width:160px"/>
                                <input type="hidden" id="txtEditPalletCode" name="PalletCode" />  
                        </td>
                     </tr>   
                     
                    <tr style="height:40px;">
                        <td align="center"  class="smalltitle" style="width:15%;">
                            备注
                        </td>
                        <td colspan="3">
                            &nbsp;<input id="txtEditMemo" name="Memo" class="easyui-textbox" data-options="multiline:true" style="width:460px; height:32px"/>

                        </td>
                        </tr>
		            </table>
            </div>
         
        </form>
    </div>
    <div id="AddCellBtn">
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="SaveCell()">保存</a>
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#AddCell').dialog('close')">关闭</a>
    </div>
     <div id="SelectWin" style="width:600px;height:500px">
       <table id="dgSelect" class="easyui-datagrid"
            data-options="loadMsg: '正在加载数据，请稍等...',fit:true, rownumbers:true,
                         pagination:true,pageSize:PageSize, pageList:[15, 20, 30, 50],method:'post',striped:true,fitcolumns:true,toolbar:'#tbSelect',singleSelect:true,selectOnCheck:true,checkOnSelect:true,onCheck:SelectSingleCheckRow,onUncheck:SelectSingleUnCheckRow,onLoadSuccess:SelectLoadSelectSuccess,onDblClickRow:DblClickRow"> 
            <thead>
                    <tr>
                        <th data-options="field:'',checkbox:true"></th> 
                        <th data-options="field:'ProductCode',width:100">产品编号</th>
                        <th data-options="field:'ProductName',width:120">品名</th>
                        <th data-options="field:'CategoryName',width:120">产品类别</th>
                    </tr>
            </thead>            
        </table>
        <div id="tbSelect" style="padding:5px;height:auto">
           <table>
                <tr>
                     <td>
                        库区
                        <input id="txtQueryAreaCodee" class ="easyui-textbox" style="width: 100px" /> 
                        货架
                        <input id="textQueryShelf" class ="easyui-textbox" style="width: 100px" /> 
                        货位
                        <input id="txtQueryCellCode" class="easyui-textbox" style="width: 100px" />  
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