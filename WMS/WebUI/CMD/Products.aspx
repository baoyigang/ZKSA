<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="WebUI_CMD_Products" %>

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
        var OtherUrl = "../../Handler/OtherHandler.ashx";
        var SessionUrl = '<% =ResolveUrl("~/Login.aspx")%>';
        var FormID = "Product";
        var OldStandardNoValue;
//        var product;
//        var baodata = { Action: 'FillDataTable', Comd: 'cmd.SelectAreaEdit', Where: "1=1" };
//        $.ajax({
//            type: "post",
//            url: url,
//            data: baodata,
//            //contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            async: false,
//            success: function (json) {
//                product = json.rows;
//            }
//        })
        function getQueryParams(objname, queryParams) {
            if (objname == "dg") {
                var Where = "1=1 ";
                var productcode = $("#txtQueryProductCodeOne").textbox("getValue");
                var modelNo = $("#txtQueryModelNo").textbox("getValue");
                var StandardNo = $("#txtQueryStandardNoOne").textbox("getValue");
                var ProductName = $("#txtQueryProductName").textbox("getValue");
                var ProductNo = $("#txtQueryProductNoOne").textbox("getValue");
                if (productcode != "") {
                    Where += " and Product.productcode like '%" + productcode + "%'";
                }
                if (modelNo != "") {
                    Where += " and modelNo like '%" + modelNo + "%'";
                }
                if (StandardNo!= "") {
                    Where += " and StandardNo like '%" + StandardNo + "%'";
                }
                if (ProductName!= "") {
                    Where += " and ProductName like '%" + ProductName + "%'";
                }
                if (ProductNo != "") {
                    Where += " and ProductNo like '%" + ProductNo + "%'";
                }
                queryParams.Where = encodeURIComponent(Where);
            }
            else {
                var ProductCodeWhere = $("#txtProductCode").textbox("getValue");
                var Where = "1=1 and ModelNo='' and Product.ProductCode !='" + ProductCodeWhere + "'";
                var ProductCode = $("#txtQueryProductCodetTwo").textbox("getValue");
                var ProductNo = $("#txtQueryProductNoTwo").textbox("getValue");
                var StandardNo = $("#txtQueryStandardNoTwo").textbox("getValue");

                if (ProductCode != "") {
                    Where += " and Product.ProductCode like '%" + ProductCode + "%'";
                }
                if (ProductNo != "") {
                    Where += " and ProductNo like '%" + ProductNo + "%'";
                }
                if (StandardNo != "") {
                    Where += " and StandardNo like '%" + StandardNo + "%'";
                }
                queryParams.Where = encodeURIComponent(Where);
            }
            //queryParams.t = new Date().getTime(); //使系统每次从后台执行动作，而不是使用缓存。
            return queryParams;

        }
        //添加管理员
        function Add() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("Product", 0)) {
                alert("您没有新增权限！");
                return false;
            }
            $('#fm').form('clear');
            BindDropDownList();
//            $('#txtModelNo').textbox('enable', true);
//            $('#txtStandardNo').textbox('enable', true);
//            $('#btnProductNo').removeAttr('disabled');
            $('#AddWin').dialog('open').dialog('setTitle', '产品资料--新增');
//            SetAutoCodeNewID('txtProductCode', 'cmd_Product', 'ProductCode', '1=1');
            $('#txtPageState').val("Add");
            $("#txtProductCode").textbox('readonly', false);        
            SetInitValue('<%=Session["G_user"] %>');
            SetInitColor();
            $("input", $("#txtFactory").next("span")).addClass("TextRead");
        }

        //修改管理员
        function Edit() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("Product", 1)) {
                alert("您没有修改权限！");
                return false;
            }
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row.length == 0) {
                $.messager.alert("提示", "请选择要修改的行！", "info");
                return false;
            }
//            var ModelNo = GetFieldValue("CMD_Product", "ModelNo", "ProductCode='" + row.ProductCode + "'");
//            if (ModelNo == "") {
//                $('#txtModelNo').textbox('disable', true);
//                $('#txtStandardNo').textbox('disable', true);
//                $('#btnProductNo').attr('disabled', "true");
//            } else {
//                $('#txtModelNo').textbox('enable', true);
//                $('#txtStandardNo').textbox('enable', true);
//                $('#btnProductNo').removeAttr('disabled');
            //            }

            BindDropDownList();
            if (row) {
                var data = { Action: 'FillDataTable', Comd: 'cmd.SelectProduct', Where: "product.ProductCode='" + row.ProductCode + "'" };
                $.post(url, data, function (result) {
                    var Product = result.rows[0];
                    OldStandardNoValue=Product.StandardNo;
                    $('#AddWin').dialog('open').dialog('setTitle', '产品资料--编辑');
                    $('#fm').form('load', Product);
                }, 'json');
                $('#dgSubAdd').datagrid({
                    url: '../../Handler/BaseHandler.ashx?Action=FillDataTable&Comd=CMD.SelectProductDetail',
                    queryParams: { Where: encodeURIComponent("ProductCode='" + row.ProductCode + "'") }
                });
            }
            $('#txtPageState').val("Edit");
            $("#txtProductCode").textbox("readonly", true);
            SetInitColor();
        }
        function BatchEdit() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            var row = $('#dg').datagrid('getSelected');
            if (row==null || row.length == 0) {
                $.messager.alert("提示", "请选择要修改的行！", "info");
                return false;
            }

            if (row) {
                $('#BatchWin').dialog('open').dialog('setTitle', '批次变更');
                $("#txtBatchProductCode").textbox("setValue", row.ProductCode);
                $("#txtBatchNewProductCode").textbox("setValue", "");
            }
            $("input", $("#txtBatchProductCode").next("span")).addClass("TextRead");
        }
        //绑定下拉控件
        function BindDropDownList() {
            var data = { Action: 'FillDataTable', Comd: 'cmd.SelectProductCategory', Where: '1=1' };
            BindComboList(data, 'ddlCategoryCode', 'CategoryCode', 'CategoryName');

         var cdata = { Action: 'FillDataTable', Comd: 'cmd.SelectAreaEdit', Where: "1=1" };
//            BindComboList(cdata, 'bao', 'AreaCode', 'AreaName');

//            $("#ddlDetailAreaCode").combobox({
//                onSelect: function (record) {
//                    var edata = { Action: 'FillDataTable', Comd: 'cmd.SelectRegionEdit', Where: "a.AreaCode='" + $('#ddlDetailAreaCode').combobox('getValue') + "'" };
//                    BindComboList(edata, 'ddlDetailRegionCode', 'RegionCode', 'RegionName')

//                }
            //            });
        }
       
        //保存信息
        function Save() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!$("#fm").form('validate')) {
                return false;
            }
            var MainQuery = createParam();
            var SubQuery = createSubParam();
            var test = $('#txtPageState').val();
            var data;

            if (test == "Add") {
                //判断单号是否存在
                if (HasExists('cmd_product', "ProductCode='" + $('#txtProductCode').textbox('getValue') + "'", '产品编号已经存在，请重新修改！'))
                    return false;
                data = { Action: 'AddMainDetail', MainComd: 'Cmd.InsertProduct', SubComd: 'Cmd.InsertProductDetail', MainJson: MainQuery, SubJson: SubQuery };
                $.post(url, data, function (result) {
                    if (result.status == 1) {
                        ReloadGrid("dg");
                        $('#AddWin').window('close');

                    } else {
                        $.messager.alert('错误', result.msg, 'error');
                    }
                }, 'json');
            }
            else {//修改
//                if (OldStandardNoValue != '' && OldStandardNoValue != StandardNoValue) {
//                    if (HasExists('CMD_Cell', "PalletCode='" + OldStandardNoValue + "'", '该旧托盘已在货位上!')) {
//                        $('#txtStandardNo').textbox('setValue', OldStandardNoValue);
//                        return false;
//                    }
//                }
//                if (StandardNoValue != '' && OldStandardNoValue != StandardNoValue) {
//                    if (HasExists('CMD_Cell', "PalletCode='" + StandardNoValue + "'", '该新托盘已在货位上!')) {
//                        $('#txtStandardNo').textbox('setValue', StandardNoValue);
//                        return false;
//                    }
//                }
                data = { Action: 'EditMainDetail', MainComd: 'Cmd.UpdateProduct', SubDelComd: 'Cmd.DeleteProductDetail', SubComd: 'Cmd.InsertProductDetail', MainJson: MainQuery, SubJson: SubQuery };
                $.post(url, data, function (result) {
                    if (result.status == 1) {
                        ReloadGrid('dg');
                        $('#AddWin').window('close');
                        
                    } else {
                        $.messager.alert('错误', result.msg, 'error');
                    }
                }, 'json');
            }
        }
        function BatchSave() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!$("#Batchfrm").form('validate')) {
                return false;
            }

            //判断单号是否存在
            if (HasExists('cmd_product', "ProductCode='" + $('#txtBatchNewProductCode').textbox('getValue') + "'", '模具编号已经存在，请重新修改！'))
                return false;
            var productcode = $('#txtBatchProductCode').textbox('getValue');
            var Newproductcode = $('#txtBatchNewProductCode').textbox('getValue');
            data = { Action: 'BatchChangeCode', ProductCode:productcode, NewProductCode: Newproductcode };
            $.post('../../Handler/OtherHandler.ashx', data, function (result) {
                if (result.status == 1) {
                    ReloadGrid('dg');
                    $('#BatchWin').window('close');
                } else {
                    $.messager.alert('错误', result.msg, 'error');
                }
            }, 'json');
        }
        function SaveDetail() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            var ReturnValue = [];
            ReturnValue.ProductCode=$("#txtProductCode").textbox('getValue');
            ReturnValue.RegionCode =$("#ddlDetailRegionCode").combobox('getValue');
            ReturnValue.GrowDay = $("#txtGrowDay").textbox('getValue');
            ReturnValue.PreQty=$("#txtPreQty").textbox('getValue');
            AddRows('SelectWin', ReturnValue);
            $('#SelectWin').dialog('close');
        }
        //删除管理员
        function Delete() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("Product", 2)) {
                alert("您没有删除权限！");
                return false;
            }
            var checkedItems = $('#dg').datagrid('getChecked');
            if (checkedItems.length==0) {
                $.messager.alert("提示", "请选择要删除的行！", "info");
                return false;
            }
            if (checkedItems) {
                $.messager.confirm('提示', '你确定要删除吗？', function (r) {
                    if (r) {
                        var deleteCode = [];
                        var blnUsed = false;
                        $.each(checkedItems, function (index, item) {
                            if (HasExists('VUsed_CMD_Product', "ProductCode='" + item.ProductCode + "'", "产品编号 "+item.ProductCode+" 已经被其它单据使用，无法删除！"))
                                blnUsed = true;
                            deleteCode.push(item.ProductCode);
                        });
                        if (blnUsed)
                            return false;
                        var data = { Action: 'DelMainDetail', MainComd: 'cmd.DeleteProduct', SubComd: "cmd.DeleteProductDetail", json: "'" + deleteCode.join("','") + "'" };
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
        function createSubParamRow(RowData) {
            RowData.ProductCode = $('#txtProductCode').textbox('getValue');
            RowData.RegionCode = $('#dgSubAdd').datagrid('getEditor', { index: RowData.RowID - 1, field: 'RegionName' }).target.combobox('getValue');
            RowData.SectionName = $('#dgSubAdd').datagrid('getEditor', { index: RowData.RowID - 1, field: 'SectionName' }).target.textbox('getValue');
            RowData.GrowDay = $('#dgSubAdd').datagrid('getEditor', { index: RowData.RowID - 1, field: 'GrowDay' }).target.textbox('getValue');
            RowData.PreQty = $('#dgSubAdd').datagrid('getEditor', { index: RowData.RowID - 1, field: 'PreQty' }).target.textbox('getValue');
        }
        function CheckRow(rowIndex, rowData) {
            CheckSelectRow('dg', rowIndex, rowData);
        }

        function AddRow(ObjName, RowData) {
            $('#txtModelNo').textbox('setValue', RowData.ProductCode);
            $('#txtProductNo').textbox('setValue', RowData.ProductNo);
            $('#txtStandardNo').textbox('setValue', RowData.StandardNo);
        
        }
        function AddRows(ObjName, RowData) {
            var j = { "RowID": $('#dgSubAdd').datagrid('getRows').length + 1, "ProductCode": RowData.ProductCode, "RegionCode": RowData.RegionCode, "GrowDay": RowData.GrowDay, "PreQty": RowData.PreQty};
            $('#dgSubAdd').datagrid('appendRow', j);
        }

        function BindSelectUrl(objName) {
            var WhereProductCode = $("#txtProductCode").textbox("getValue");
            $('#dgSelect').datagrid({
                url: '../../Handler/BaseHandler.ashx?Action=PageDate&FormID=Product',
                pageNumber: 1,
                queryParams: { Where: encodeURIComponent("1=1 and ModelNo='' and Product.ProductCode !='" + WhereProductCode + "'") }
            });
        }
        //绑定详细表
        function getDetail(index, data) {
            var selectdata = data;
            if (selectdata) {
                $('#dgSub').datagrid({
                    url: '../../Handler/BaseHandler.ashx?Action=PageDate&Comd=WMS.SelectProductDetailView',
                    queryParams: { Where: encodeURIComponent("ProductCode='" + selectdata.ProductCode + "'") }
                });
            }
        }
        function EditDetail() {
            var editIndex = undefined;
            if (endEditing()) {
                editIndex = $('#dgSubAdd').datagrid('getRows').length - 1;
                var j = { "RowID": $('#dgSubAdd').datagrid('getRows').length + 1 };
                $('#dgSubAdd').datagrid('appendRow', j);
                $('#dg').datagrid('selectRow', editIndex).datagrid('beginEdit', editIndex);
            }
        }
        function getRowIndex(target) {
            var tr = $(target).closest("tr.datagrid-row");
            return parseInt(tr.attr("datagrid-row-index"));
        }
        $(function () {
            var areaPro;
            var regionPro;
            var baodata = { Action: 'FillDataTable', Comd: 'cmd.SelectAreaEdit', Where: "1=1" };
            $.ajax({
                type: "post",
                url: url,
                data: baodata,
                //contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (json) {
                    areaPro = json.rows;
                }
            })
            $('#dgSubAdd').datagrid({
                onClickCell: function (index, field, value) {
                    $(this).datagrid('beginEdit', index);
                    var ed = $(this).datagrid('getEditor', { index: index, field: field });
                    $(ed.target).focus();
                },
                onLoadSuccess: function (json) {
                    if (json) {
                        for (var i = 0; i < json.rows.length; i++) {
                            $('#dgSubAdd').datagrid('beginEdit', i);
                        };
                    };
                }
                ,
                columns: [[{
                    field: '', checkbox: true
                },
                {
                    field: 'RowID', width: 60, title: '序号'
                },
                 {
                     field: 'SectionName', title: '阶段名称', width: 150, editor: {
                         type: 'textbox',
                         required: true
                     }
                 },
                {
                    field: 'GrowDay', title: '成长天数', width: 150, editor: {
                        type: 'numberbox',
                        options: {
                            precision: 0
                        },   
                        required: true
                    }
                },
                {
                    field: 'PreQty', title: '每盆数量', width: 120, editor: {
                        type: 'numberbox',
                        options: {
                            precision: 0
                        }, 
                        required: true
                    }
                },
                {
                    field: 'AreaName', title: '存放区域', width: 130, editor: {
                        type: 'combobox',
                        options: {
                            data: areaPro,
                            valueField: 'AreaCode',
                            textField: 'AreaName',
                            editable: false,
                            onSelect: function (record) {
                                var indexSelect = getRowIndex(this);
                                var edArea = $('#dgSubAdd').datagrid('getEditor', { index: indexSelect, field: 'AreaName' });
                                var edReg = $('#dgSubAdd').datagrid('getEditor', { index: indexSelect, field: 'RegionName' });
                                var val = edArea.target.combobox('getValue');
                                var eadata = { Action: 'FillDataTable', Comd: 'cmd.SelectRegionEdit', Where: "a.AreaCode='" + val + "'" };
                                $.ajax({
                                    type: "post",
                                    url: url,
                                    data: eadata,
                                    //contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    async: false,
                                    success: function (json) {
                                        edReg.target.combobox({
                                            data: json.rows,
                                            valueField: 'RegionCode',
                                            textField: 'RegionName'
                                        });
                                    }
                                });
                            }
                        }
                    }
                },
                {
                    field: 'RegionName', title: '存放库区', width: 130, editor: {
                        type: 'combobox',
                        options: {
                            data: regionPro,
                            valueField: 'RegionCode',
                            textField: 'RegionName',
                            editable: false
                        }
                    }
                }
                ]]
            })

        })           
    </script> 
</head>
<body class="easyui-layout">
    <div class="easyui-layout" data-options="fit:true">
    <div data-options="region:'north',split:true" style="height:300px;">
    <table id="dg"  class="easyui-datagrid" 
        data-options="loadMsg: '正在加载数据，请稍等...',fit:true, rownumbers:true,url:'../../Handler/BaseHandler.ashx?Action=PageDate&FormID='+FormID,
                     pagination:true,pageSize:PageSize, pageList:[15, 20, 30, 50],method:'post',striped:true,fitcolumns:true,toolbar:'#tb',onLoadSuccess: function(data){ 
                             $('#dg').datagrid('selectRow',0);},singleSelect:true,selectOnCheck:false,checkOnSelect:false,onSelect:getDetail,onCheck:CheckRow,onUncheck:CheckRow"> 
        <thead data-options="frozen:true">
			<tr>
				<th data-options="field:'',checkbox:true"></th> 
                <th data-options="field:'ProductCode',width:140">产品编号</th>
                <th data-options="field:'ProductName',width:140">产品名称</th>
                <th data-options="field:'CategoryName',width:90">产品类别</th>      
               
			</tr>
		</thead>
        <thead>
		    <tr>               
               <th data-options="field:'Memo',width:100">备注</th>
                        <th data-options="field:'Creator',width:80">建单人员</th>
                        <th data-options="field:'CreateDate',width:180">建单日期</th>
                        <th data-options="field:'Updater',width:80">修改人员</th>
                        <th data-options="field:'UpdateDate',width:180">修改日期</th>
		    </tr>
        </thead>
    </table>
    </div>
            <div data-options="region:'center', split:true,title:'产品',split:true" >
            <table id="dgSub"  class="easyui-datagrid" 
                data-options="loadMsg: '正在加载数据，请稍等...',fit:true, rownumbers:true,pagination:true,pageSize:PageSize, pageList:[15, 20, 30, 50],method:'post',striped:true,fitcolumns:true"> 
                <thead>
		            <tr>
		                <th data-options="field:'RowID',width:100">序号</th>
                        <th data-options="field:'SectionName',width:100">产品阶段</th>
                        <th data-options="field:'RegionCode',width:100">库区编号</th>
                        <th data-options="field:'GrowDay',width:100">成长天数</th>
                        <th data-options="field:'PreQty',width:100">数量</th>
		            </tr>
                </thead>
            </table>
        </div>   

    </div>
    <div id="tb" style="padding: 5px; height: auto">  
    
        <table style="width:100%">
            <tr>
                <td>
                    产品编号
                    <input id="txtQueryProductCodeOne" class ="easyui-textbox" style="width: 100px" />
                    产品名称
                    <input id="txtQueryModelNo" class ="easyui-textbox" style="width: 100px" />
                    产品类别
                    <input id="txtQueryStandardNoOne" class="easyui-textbox" style="width: 100px"/>  
                    品名
                    <input id="txtQueryProductName" class="easyui-textbox" style="width: 100px" /> 
                    产品编号  
                    <input id="txtQueryProductNoOne" class="easyui-textbox" style="width: 100px"/>   
                    <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="ReloadGrid('dg')">查询</a> 
                </td>
                <td  style="width:*"  align="right">
                     <a href="javascript:void(0)" onclick="Add()" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">新增</a>  
                     <a href="javascript:void(0)" onclick="Edit() " class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true">修改</a>  
                     <a href="javascript:void(0)" onclick="Delete()" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">删除</a>
                     <a href="javascript:void(0)" onclick="BatchEdit() " class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true">批次变更</a>
                     <a href="javascript:void(0)" onclick="Exit()" class="easyui-linkbutton" data-options="iconCls:'icon-no',plain:true">离开</a>
                </td>
            </tr>
        </table>
   </div>
      <%-- 弹出操作框--%>
    <div id="AddWin" class="easyui-dialog" style="width: 1000px; height: auto; padding: 5px 5px"
        data-options="closed:true,buttons:'#AddWinBtn',modal:true"> 
        <form id="fm" method="post">
               <div>
                    <table id="Table1" class="grid maintable" style="table-layout:fixed;"  width="100%" align="center">			
				    <tr> 
                        <td align="center" class="musttitle"style="width:9%">
                            产品编号 </td>
                        <td style="width:21%" >
                        
                            &nbsp;<input id="txtProductCode" name="ProductCode" class="easyui-textbox" data-options="required:true" style="width:160px"/> 
                            <input name="PageState" id="txtPageState" type="hidden" />
                        
                        </td>
                        <td align="center" class="musttitle"style="width:9%">
                                产品名称
                        </td>
                        <td  style="width:21%">
                                &nbsp;<input id="txtProductName" name="ProductName" 
                                    class="easyui-textbox" data-options="required:true" maxlength="20" style="width:160px"/>
                        </td>
                        <td align="center" class="musttitle"style="width:9%"  >
                                产品类别
                        </td>
                        <td > 
                            &nbsp;<input 
                                id="ddlCategoryCode" name="CategoryCode" class="easyui-combobox" 
                                data-options="required:true" maxlength="50" style="width:270px"/>
                        </td>
                        
                    </tr>
                    <tr style="height:40px;">
                        <td align="center"  class="smalltitle" style="width:9%;">
                            备注
                        </td>
                        <td colspan="5">
                            &nbsp;<input 
                                id="txtMemo" name="Memo" class="easyui-textbox" 
                                data-options="multiline:true" style="width:856px; height:32px"/>

                        </td>
                        </tr>
                        <tr>
                        <td colspan="6">
                                <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'" onclick="EditDetail()">新增明细</a>
                                <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-remove'" onclick="DeleteSubDetail('dgSubAdd')">删除明细</a>
                        </td>
                        </tr>
		            </table>
            </div>
           
            <table id="dgSubAdd" class="easyui-datagrid" style="width:100%;height:272px;"
                data-options="loadMsg: '正在加载数据，请稍等...',rownumbers:true,pagination:false,method:'post',striped:true,fitcolumns:true,singleSelect:true,
                              selectOnCheck:false,checkOnSelect:false"> 
            </table>
            <table class="grid maintable" style="table-layout:fixed;width:100%">
                <tr>
                     <td align="center"  class="smalltitle" style="width:8%;">
                            建单人员
                     </td> 
                    <td style="width:12%">
                    &nbsp;<input id="txtCreator" name="Creator" class="easyui-textbox" data-options="editable:false" style="width:90%"/>
                    </td>
                    <td align="center" class="smalltitle" style="width:8%;">
                        建单日期
                    </td> 
                    <td style="width:12%">
                    &nbsp;<input id="txtCreateDate" name="CreateDate" class="easyui-textbox" data-options="editable:false"  style="width:90%"/>
                    </td>
                    <td align="center"  class="smalltitle" style="width:8%;">
                        修改人员
                    </td> 
                    <td style="width:12%">
                        &nbsp;<input id="txtUpdater" name="Updater" class="easyui-textbox" data-options="editable:false"  style="width:90%"/>
                    </td>
                    <td align="center"  class="smalltitle" style="width:8%;">
                        修改日期
                    </td> 
                    <td style="width:12%">
                    &nbsp;<input id="txtUpdateDate" name="UpdateDate" class="easyui-textbox" data-options="editable:false"  style="width:90%"/>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="AddWinBtn">
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="Save()">保存</a>
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#AddWin').dialog('close')">关闭</a>
    </div>

    <%-- 单号批次变更 --%>
     <div id="BatchWin" class="easyui-dialog" style="width: 350px; height: auto; padding: 5px 5px"
        data-options="closed:true,buttons:'#BatchWinBtn',modal:true"> 
        <form id="Batchfrm" method="post">
              <table id="Table2" class="maintable"  width="100%" align="center">			
				<tr>
                    <td align="center" class="musttitle"style="width:90px">
                        模具编号</td>
                    <td width="176px">
                        
                        &nbsp;<input id="txtBatchProductCode" name="ProductCode" class="easyui-textbox" 
                            data-options="required:true,editable:false" style="width:172px"/>&nbsp;
                       
                    </td>
                    
                </tr>
                <tr>
                    <td align="center" class="musttitle"style="width:90px">
                            新模具编号
                    </td>
                    <td  width="176px">
                            &nbsp;<input id="txtBatchNewProductCode" name="NewProductCode" 
                                class="easyui-textbox" data-options="required:true" maxlength="20" 
                                style="width:172px"/>
                    </td>                    
                </tr>            
             </table>
        </form>
    </div>
    <div id="BatchWinBtn">
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="BatchSave()">保存</a>
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#BatchWin').dialog('close')">关闭</a>
    </div>

</body>
</html>