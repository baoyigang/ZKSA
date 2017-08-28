<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegionSet.aspx.cs" Inherits="WebUI_CMD_RegionSet" %>

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
        var FormID = "Region";


        function getQueryParams(objname, queryParams) {
            var Where = "1=1 ";
            var RegionCode = $("#txtRegionCode").textbox("getValue");
            var RegionName = $("#txtRegionName").textbox("getValue");
            var AreaName = $("#txtAreaName").textbox("getValue");

            if (AreaName != "") {
                Where += " and AreaName like '%" + AreaName + "%'";
            }
            if (RegionCode != "") {
                Where += " and RegionCode like '%" + RegionCode + "%'";
            }
            if (RegionName != "") {
                Where += " and RegionName like '%" + RegionName + "%'";
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
            if (!GetPermisionByFormID("Area", 1)) {
                alert("您没有新增权限！");
                return false;
            }
            $('#fm').form('clear');
            BindDropDownList();
            $('#AddWin').dialog('open').dialog('setTitle', '库区设置--新增');
            $("#txtWarehouseCode").textbox('setValue', '01');

            SetAutoCodeNewID('txtID', 'CMD_Region', 'RegionCode', '1=1');
            $('#txtPageState').val("Add");
            $("#txtID").textbox('readonly', false);
            $('#txtRegionName').textbox().next('span').find('input').focus();
            SetInitValue('<%=Session["G_user"] %>');
            SetInitColor();
        }
        //修改管理员
        function Edit() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("Region", 2)) {
                alert("您没有修改权限！");
                return false;
            }
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row.length == 0) {
                $.messager.alert("提示", "请选择要修改的行！", "info");
                return false;
            }
            BindDropDownList();
            if (row) {
                var data = { Action: 'FillDataTable', Comd: 'cmd.SelectRegionEdit', Where: "RegionCode='" + row.RegionCode + "'" };
                $.post(url, data, function (result) {
                    var Product = result.rows[0];
                    $('#AddWin').dialog('open').dialog('setTitle', '库区设置--编辑');
                    $('#fm').form('load', Product);
                }, 'json');
            }
  
            $('#txtPageState').val("Edit");
            $("#txtID").textbox("readonly", true);
            SetInitColor();
        }
        //绑定下拉控件
        function BindDropDownList() {
            var data = { Action: 'FillDataTable', Comd: 'cmd.SelectAreaEdit', Where: "1=1" };
            BindComboList(data, 'SelectAreaName', 'AreaCode', 'AreaName');
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
                if (HasExists('CMD_Region', "RegionCode='" + $('#txtID').textbox('getValue') + "'", '类别编码已经存在，请重新修改！')) {
                    $('#txtID').textbox().next('span').find('input').focus();
                    return false;
                }
                data = { Action: 'Add', Comd: 'cmd.InsertRegionEdit', json: query };
                $.post(url, data, function (result) {
                    if (result.status == 1) {
                        ReloadGrid('dg');
                        $('#AddWin').window('close');

                    } else {
                        $.messager.alert('错误', result.msg, 'error');
                    }
                }, 'json');

            }
            else {
                data = { Action: 'Edit', Comd: 'cmd.UpdateRegionEdit', json: query };
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
        //删除管理员
        function Delete() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("Region", 3)) {
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
                            if (HasExists('CMD_Cell', "RegionCode='" + item.RegionCode + "'", "库区编码 " + item.RegionCode + " 已经被其它单据使用，无法删除！"))
                                                            blnUsed = true;
                            deleteCode.push(item.RegionCode);
                        });
                        if (blnUsed)
                            return false;
                        var data = { Action: 'Delete', FormID: FormID, Comd: 'cmd.DeleteRegionEdit', json: "'" + deleteCode.join("','") + "'" };
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
        function CheckRow(rowIndex, rowData) {
            CheckSelectRow('dg', rowIndex, rowData);
        }
    </script> 
</head>
<body class="easyui-layout">
    <table id="dg"  class="easyui-datagrid" 
        data-options="loadMsg: '正在加载数据，请稍等...',fit:true, rownumbers:true,url:'../../Handler/BaseHandler.ashx?Action=PageDate&FormID='+FormID,
                     pagination:true,pageSize:PageSize, pageList:[15, 20, 30, 50],method:'post',striped:true,fitcolumns:true,toolbar:'#tb',singleSelect:false,selectOnCheck:true,checkOnSelect:false,onCheck:CheckRow,onUncheck:CheckRow,onBeforeSortColumn:BeforeSortColumn,idField:'CellCode'"> 
        <thead>
		    <tr>
                <th data-options="field:'',checkbox:true"></th> 
		        <th data-options="field:'WarehouseCode',width:80,sortable:true">仓库编码</th>
                <th data-options="field:'AreaCode',width:130,sortable:true">区域编码</th>
                <th data-options="field:'AreaName',width:130,sortable:true">区域名称</th>
                <th data-options="field:'RegionCode',width:130">库区编码</th>
                <th data-options="field:'RegionName',width:130">库区名称</th>
                <th data-options="field:'Memo',width:130">备注</th>
		    </tr>
        </thead>
    </table>
    <div id="tb" style="padding: 5px; height: auto">  
    
        <table style="width:100%" >
            <tr>
                <td>
                区域名称
                <input id="txtAreaName" class ="easyui-textbox" style="width: 100px" />  
                   库区编码
                    <input id="txtRegionCode" class ="easyui-textbox" style="width: 100px" />  
                    库区名称
                    <input id="txtRegionName" class="easyui-textbox" style="width: 100px" />&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="ReloadGrid('dg')">查询</a> 
                </td>
                <td style="width:*" align="right">
                     <a href="javascript:void(0)" onclick="Add()" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">新增</a>  
                     <a href="javascript:void(0)" onclick="Edit() " class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true">修改</a>  
                     <a href="javascript:void(0)" onclick="Delete()" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">删除</a>
                     <a href="javascript:void(0)" onclick="Exit()" class="easyui-linkbutton" data-options="iconCls:'icon-no',plain:true">离开</a>
                </td>
            </tr>
        </table>
   </div>
      <%-- 弹出操作框--%>
    <div id="AddWin" class="easyui-dialog" style="width: 600px; height: auto; padding: 5px 5px"
        data-options="closed:true,buttons:'#AddWinBtn',modal:true"> 
        <form id="fm" method="post">
              <table id="Table1" class="maintable"  width="100%" align="center">			
				<tr>
                <td align="center" class="musttitle"style="width:90px">
                            仓库编码
                    </td>
                       <td  width="210px">
                            &nbsp;<input id="txtWarehouseCode" name="WarehouseCode" 
                                class="easyui-textbox" data-options="required:true,disabled:true" maxlength="20" style="width:180px"/>
                                <input name="PageState" id="Hidden1" type="hidden" />
                    <td align="center" class="musttitle"style="width:90px">
                            区域名称
                    </td>
                    <td width="210px"> 
                        &nbsp;<input id="SelectAreaName" name="AreaCode" class="easyui-combobox" data-options="required:true,editable:false,valueField:'AreaCode',
                       textField:'AreaName'" maxlength="50" style="width:180px"/>
                    </td>
                </tr>   
                <tr>
                    <td align="center" class="musttitle"style="width:90px">
                            库区编码
                    </td>
                    <td  width="210px">
                            &nbsp;<input id="txtID" name="RegionCode" 
                                class="easyui-textbox" data-options="required:true" maxlength="20" style="width:180px"/>
                                <input name="PageState" id="txtPageState" type="hidden" />
                    </td>
                    <td align="center" class="musttitle"style="width:90px"  >
                           库区名称
                    </td>
                    <td width="210px"> 
                        &nbsp;<input id="txtRegionNameI" name="RegionName" class="easyui-textbox" data-options="required:true" maxlength="50" style="width:180px"/>
                    </td>
                </tr>
                <tr style=" height:80px">
                    <td align="center"  class="smalltitle" style="width:120px;height:80px;">
                        备注
                    </td>
                    <td colspan="3" style="height:80px;">
                       &nbsp;<input 
                            id="txtMemo" name="Memo" class="easyui-textbox" 
                            data-options="multiline:true" style="width:478px; height:72px"/>

                    </td>
                </tr>
               
                	
		</table>
        </form>
    </div>
    <div id="AddWinBtn">
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="Save()">保存</a>
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#AddWin').dialog('close')">关闭</a>
    </div>

</body>
</html>