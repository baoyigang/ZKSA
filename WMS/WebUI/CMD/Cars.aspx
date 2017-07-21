<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cars.aspx.cs" Inherits="WebUI_CMD_Cars" %>

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
        var FormID = "Car";

        function getQueryParams(objname, queryParams) {
            var Where = "1=1 ";
            var CarNo = $("#txtQueryCarID").textbox("getValue");
            var CarName = $("#txtQueryCarName").textbox("getValue");
            var StateDesc = $("#txtQueryState").textbox("getValue");
            var StationNo = $("#txtQueryStationNo").textbox("getValue");
            if (CarNo != "") {
                Where += " and CarNo  like '%" + CarNo + "%'";
            }
            if (CarName != "") {
                Where += " and CarName like '%" + CarName + "%'";
            }
            if (StateDesc != "") {
                Where += " and StateDesc like '%" + StateDesc + "%'";
            }
            if (StationNo != "") {
                Where += " and StationNo like '%" + StationNo + "%'";
            }

            queryParams.Where = encodeURIComponent(Where);
            //queryParams.t = new Date().getTime(); //使系统每次从后台执行动作，而不是使用缓存。
            return queryParams;

        }
        //添加管理员
        function Edit() {
            if (SessionTimeOut(SessionUrl)) {
                return false;
            }
            if (!GetPermisionByFormID("Factory", 1)) {
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
                var data = { Action: 'FillDataTable', Comd: 'cmd.SelectCar', Where: "CarNo='" + row.CarNo + "'" };
                $.post(url, data, function (result) {
                    var Product = result.rows[0];
                    $('#AddWin').dialog('open').dialog('setTitle', '小车--编辑');
                    $('#fm').form('load', Product);

                }, 'json');
            }
            $('#txtPageState').val("Edit");
            $('#txtFlag').val("1");
            $("#txtID").textbox("readonly", true);
            SetInitColor();
        }
        //绑定下拉控件
        function BindDropDownList() {
            var data = { Action: 'FillDataTable', Comd: 'cmd.SelecState', Where: "TableName='CMD_Car'" };
            BindComboList(data, 'ddlState', 'State', 'StateDesc');

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
            if (test == "Edit") {
                data = { Action: 'Edit', Comd: 'cmd.UpdateCar', json: query };
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
        function CheckRow(rowIndex, rowData) {
            CheckSelectRow('dg', rowIndex, rowData);
        }
 </script> 
</head>
<body class="easyui-layout">
    <table id="dg"  class="easyui-datagrid" 
        data-options="loadMsg: '正在加载数据，请稍等...',fit:true, rownumbers:true,url:'../../Handler/BaseHandler.ashx?Action=PageDate&FormID='+FormID,
                     pagination:true,pageSize:PageSize, pageList:[15, 20, 30, 50],method:'post',striped:true,fitcolumns:true,toolbar:'#tb',singleSelect:true,selectOnCheck:false,checkOnSelect:false,onCheck:CheckRow,onUncheck:CheckRow"> 
        <thead>
		    <tr>
                <th data-options="field:'',checkbox:true"></th> 
		        <th data-options="field:'CarNo',width:80">小车编号</th>
                <th data-options="field:'CarName',width:100">名称</th>
                <th data-options="field:'StateDesc',width:80">状态</th>
                <th data-options="field:'CraneNo',width:100">堆垛机</th>
                <th data-options="field:'StationNo',width:80">站台编号</th>
                <th data-options="field:'Memo',width:100">备注</th>
		    </tr>
        </thead>
    </table>
    <div id="tb" style="padding: 5px; height: auto">  
    
        <table style="width:100%" >
            <tr>
                <td>
                    小车编号
                    <input id="txtQueryCarID" class ="easyui-textbox" style="width: 100px" />  
                    名称
                    <input id="txtQueryCarName" class="easyui-textbox" style="width: 100px" /> 
                    状态
                    <input id="txtQueryState" class="easyui-textbox" style="width: 100px" />
                     站台编号
                    <input id="txtQueryStationNo" class="easyui-textbox" style="width: 100px" />
                    &nbsp;&nbsp;
                    <a href="#" onclick="ReloadGrid('dg')" class="easyui-linkbutton" data-options="iconCls:'icon-search'">查询</a> 
                </td>
                <td style="width:*" align="right">
                     <a href="javascript:void(0)" onclick="Edit() " class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true">修改</a>
                     <a href="javascript:void(0)"  onclick="Exit()" class="easyui-linkbutton" data-options="iconCls:'icon-no',plain:true">离开</a>
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
                            小车编号
                    </td>
                    <td  width="210px">
                            &nbsp;<input id="txtID" name="CarNo" 
                                class="easyui-textbox" data-options="required:true" maxlength="2" style="width:180px"/>
                                <input name="PageState" id="txtPageState" type="hidden" />
                                <input name="Flag" id="txtFlag" type="hidden" value="1"   />
                    </td>
                    <td align="center" class="musttitle"style="width:90px"  >
                           名称
                    </td>
                    <td width="210px"> 
                        &nbsp;<input id="txtCarName" name="CarName" class="easyui-textbox" data-options="required:true" maxlength="20" style="width:180px"/>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="musttitle"style="width:90px">
                            状态
                    </td>
                    <td  width="210px">
                            &nbsp;<input id="ddlState" name="State" class="easyui-combobox" data-options="required:true,editable:false"  style="width:180px"/> 
                    </td>
                     <td align="center" class="musttitle"style="width:90px">
                            堆垛机
                    </td>
                    <td  width="210px">
                            &nbsp;<input id="Text1" name="CraneNo" class="easyui-textbox" data-options="required:true"  style="width:180px"/> 
                    </td>
                </tr>
                <tr>
                    <td align="center" class="smalltitle"style="width:90px"  >
                         站台编号   
                    </td>
                    <td width="210px"> 
                       &nbsp;<input id="txtStationNo" name="StationNo" class="easyui-textbox"  maxlength="10" style="width:180px"/>
                    </td>
                    <td align="center" class="smalltitle"style="width:90px"  > 
                    </td>
                    <td width="210px"> 
                    </td>
                </tr>
                <tr style=" height:80px">
                    <td align="center"  class="smalltitle" style="width:90px;height:80px;">
                        备注
                    </td>
                    <td colspan="3" style="height:80px;">
                       &nbsp;<input 
                            id="txtMemo" name="Memo" maxlength="500" class="easyui-textbox" 
                            data-options="multiline:true" style="width:478px; height:72px"/>

                    </td>
                </tr>     	
		</table>
        </form>
    </div>
    <div id="AddWinBtn">
        <a href="#" onclick="Save()" class="easyui-linkbutton" data-options="iconCls:'icon-ok'">保存</a>
        <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" onclick="javascript:$('#AddWin').dialog('close')">关闭</a>
    </div>

</body>
</html>
