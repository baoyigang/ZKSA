<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Device.aspx.cs" Inherits="WebUI_CMD_Device" %>

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
        var FormID = "Device";
        var StateValue = [{ StateCode: '1', StateText: '启用' }, { StateCode: '0', StateText: '禁用'}];
        var DeviceValue = [{ DeviceCode: '01', DeviceText: '堆垛机' }, { DeviceCode: '02', DeviceText: '穿梭车' }, { DeviceCode: '03', DeviceText: '提升机'}];
        function getQueryParams(objname, queryParams) {
            var Where = "1=1 ";
            var WareHouseCode = $("#txtWareHouseCode").textbox("getValue");
            var DeviceNo = $("#txtDeviceNo").textbox("getValue");
            var DeviceName = $("#txtDeviceName").textbox("getValue");
            var State = $("#txtState").textbox("getValue");
            if (WareHouseCode != "") {
                Where += " and WareHouseCode  like '%" + WareHouseCode + "%'";
            }
            if (DeviceNo != "") {
                Where += " and DeviceNo like '%" + DeviceNo + "%'";
            }
            if (DeviceName != "") {
                Where += " and DeviceName like '%" + DeviceName + "%'";
            }
            if (State != "") {
                Where += " and State like '%" + State + "%'";
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
            if (!GetPermisionByFormID("Device", 2)) {
                alert("您没有修改权限！");
                return false;
            }
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row.length == 0) {
                $.messager.alert("提示", "请选择要修改的行！", "info");
                return false;
            }
            if (row) {
                var data = { Action: 'FillDataTable', Comd: 'cmd.SelectDevice', Json: "[{\"{0}\": \"DeviceNo='" + row.DeviceNo + "'\"}]"};
                $.post(url, data, function (result) {
                    var Product = result.rows[0];
                    $('#AddWin').dialog('open').dialog('setTitle', '设备--编辑');
                    $('#fm').form('load', Product);

                }, 'json');
            }
            $('#txtPageState').val("Edit");
            $('#txtFlag').val("1");
            $("#txtID").textbox("readonly", true);
            SetInitColor();
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
                data = { Action: 'Edit', Comd: 'cmd.UpdateDevice', json: query };
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
        $(function () {
            $("#dg").datagrid({           
                columns: [[{
                    field: '', checkbox: true
                },
                {
                    field: 'WarehouseCode', title: '仓库编码', width: 100, align: 'center', editor: {
                        type: 'textbox',
                        options: {
                            required: true,
                            disabled: true
                        }
                    }
                },
                  {
                      field: 'DeviceType', title: '设备类型', width: 100, align: 'center', formatter: function (value) {
                          if (value == '01') {
                              return '堆垛机';
                          }
                          else if (value == '02') {
                              return '穿梭车';
                          }
                          else {
                              return '提升机';
                          }
                      }, editor: {
                          type: 'textbox',
                          options: {
                              required: true
                          }
                      }
                  },
                  {
                      field: 'DeviceNo', title: '设备编码', width: 100, align: 'center', editor: {
                          type: 'textbox',
                          options: {
                              required: true
                          }
                      }
                  },
                   {
                       field: 'DeviceName', title: '设备名称', width: 100, align: 'center', editor: {
                           type: 'textbox',
                           options: {
                               required: true
                           }
                       }
                   },
                   {
                       field: 'State', title: '状态', width: 100, align: 'center', formatter: function (value) {
                           if (value == 1) {
                               return '启用';
                           }
                           else {
                               return '未启用';
                           }
                       },
                       editor: {
                           type: 'textbox',
                           options: {
                               required: true
                           }
                       }
                   },
                   {
                       field: 'AlarmCode', title: '报警代码', width: 100, align: 'center', editor: {
                           type: 'textbox',
                           options: {
                               required: true
                           }
                       }
                   },
                  {
                      field: 'Memo', title: '备注', width: 100, align: 'center', editor: {
                          type: 'textbox'
                      }
                  }
                ]]
            })
        })
    </script> 
</head>
<body class="easyui-layout">
    <table id="dg"  class="easyui-datagrid" 
        data-options="loadMsg: '正在加载数据，请稍等...',fit:true, rownumbers:true,url:'../../Handler/BaseHandler.ashx?Action=PageDate&FormID='+FormID,
                     pagination:true,pageSize:PageSize, pageList:[15, 20, 30, 50],method:'post',striped:true,fitcolumns:true,toolbar:'#tb',singleSelect:true,selectOnCheck:false,checkOnSelect:false,onCheck:CheckRow,onUncheck:CheckRow"> 
    </table>
    <div id="tb" style="padding: 5px; height: auto">  
    
        <table style="width:100%" >
            <tr>
                <td>
                    仓库编码
                    <input id="txtWareHouseCode" class ="easyui-textbox" style="width: 100px" />  
                    设备编码
                    <input id="txtDeviceNo" class="easyui-textbox" style="width: 100px" /> 
                    设备名称
                    <input id="txtDeviceName" class="easyui-textbox" style="width: 100px" />
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
                            设备编码
                    </td>
                    <td  width="210px">
                            &nbsp;<input id="txtID" name="DeviceNo" 
                                class="easyui-textbox" data-options="required:true,editable:false" maxlength="2" style="width:180px"/>
                                <input name="PageState" id="txtPageState" type="hidden" />
                                <input name="Flag" id="txtFlag" type="hidden" value="1"   />
                    </td>
                    <td align="center" class="musttitle"style="width:90px"  >
                           设备类型
                    </td>
                    <td width="210px"> 
                        &nbsp;<input id="txtDeviceType" name="DeviceType" class="easyui-combobox" data-options="required:true,editable:false,valueField:'DeviceCode',textField:'DeviceText',data:DeviceValue" maxlength="120" style="width:180px"/>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="musttitle"style="width:90px">
                            设备名称
                    </td>
                    <td  width="210px">
                            &nbsp;<input id="txtAddDeviceName" name="DeviceName" class="easyui-textbox" data-options="required:true"  style="width:180px"/> 
                    </td>
                     <td align="center" class="musttitle"style="width:90px">
                            状态
                    </td>
                    <td  width="210px">
                            &nbsp;<input id="txtState" name="State" class="easyui-combobox" data-options="required:true,valueField:'StateCode',textField:'StateText',data:StateValue"  style="width:180px"/> 
                    </td>
                </tr>
                <tr>
                    <td align="center" class="musttitle"style="width:90px"  >
                         报警代码   
                    </td>
                    <td width="210px"> 
                       &nbsp;<input id="txtAlarmCode" name="AlarmCode" class="easyui-textbox"  maxlength="10" style="width:180px"/>
                    </td>
                    <td align="center" class="musttitle"style="width:90px"  > 
                    PLC名称
                    </td>
                    <td width="210px"> 
                      &nbsp;<input id="txtServiceName" name="ServiceName" class="easyui-textbox"  maxlength="10" style="width:180px"/>
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
