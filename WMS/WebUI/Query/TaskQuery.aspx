<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskQuery.aspx.cs" Inherits="WebUI_Query_TaskQuery" %>


<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<%@ Register src="../../UserControl/Calendar.ascx" tagname="Calendar" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title> 
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="~/Css/default.css" />
    <link rel="stylesheet" type="text/css" href="~/Css/icon.css" /> 
    <link href="../../Css/op.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../../EasyUI/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../EasyUI/themes/icon.css" />
    <script type="text/javascript" src="../../EasyUI/jquery.min.js"></script>
    <script type="text/javascript" src="../../EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../JScript/JsAjax.js" ></script>
    <script src="../../JScript/Common.js" type="text/javascript"></script>
    <script src="../../JScript/DataProcess.js" type="text/javascript"></script>
    <script type="text/javascript">
        var url = "../../Handler/BaseHandler.ashx";
        var SessionUrl = '<% =ResolveUrl("~/Login.aspx")%>';
           $(document).ready(function () {
               $(window).resize(function () {
                   resize();
               });
               BindEvent();
           });

           function getQueryParams(objname, queryParams) {
               var Where = "1=1 ";
               if (objname == "dgSelect") {              
                   var ProductCode = $("#txtQueryProductCode").textbox("getValue");
                   var ProductNo = $("#txtQueryProductNo").textbox("getValue");
                   var ProductName = $("#txtQueryProductName").textbox("getValue");

                   if (ProductCode != "") {
                       Where += " and ProductCode like '%" + ProductCode + "%'";
                   }
                   if (ProductNo != "") {
                       Where += " and ProductNo like '%" + ProductNo + "%'";
                   }
                   if (ProductName != "") {
                       Where += " and ProductName like '%" + ProductName + "%'";
                   }

               } else {
                   var ProductCodeMulti = $("#txtQueryProductCodeMulti").textbox("getValue");
                   var ProductNoMulti = $("#txtQueryProductNoMulti").textbox("getValue");
                   var ProductNameMulti = $("#txtQueryProductNameMulti").textbox("getValue");

                   if (ProductCodeMulti != "") {
                       Where += " and ProductCode like '%" + ProductCodeMulti + "%'";
                   }
                   if (ProductNoMulti != "") {
                       Where += " and ProductNo like '%" + ProductNoMulti + "%'";
                   }
                   if (ProductNameMulti != "") {
                       Where += " and ProductName like '%" + ProductNameMulti + "%'";
                   }
               }
               queryParams.Where = encodeURIComponent(Where);
               return queryParams;
           }
           function resize() {
               var h = document.documentElement.clientHeight - 60;
               $("#rptview").css("height", h);
           }
           function BindEvent() {
               $("#txtProductCode").bind("dblclick", function () {
                  SelectWinShow('SelectWin', '模具--选择');

               });
//               $("#txtProductCode").bind("change", function () {
//                   var where = GetWhere();
//                   where += " and ProductNo='" + $('#txtProductCode').val() + "'";
//                   var row = new Object();
//                   row.strWhere = where;
//                   row.strFieldName = "ProductName,ProductNo";
//                   row.TableName = "CMD_Product";
//                   var returnvalue = Ajax("strBaseData", row);
//                   if (returnvalue.length > 0 && returnvalue != "[]") {
//                       var strReturn = jQuery.parseJSON(returnvalue);
//                       $('#txtProductCode').val(unescape(strReturn[0]["ProductNo"]));
//                       $('#txtProductName').val(unescape(strReturn[0]["ProductName"]));
//                   } else {
//                       $('#txtProductCode').val('');
//                       $('#txtProductName').val('');
//                   }
//               });
           }

           function ProductClick() {
               if ($('#btnProduct').val()=="取消指定") {
                   $('#HdnProduct').val("");
                   $('#btnProduct').val("指定");
                   return;
                }
               SelectWinShow('SelectWinTwo', '模具资料--指定');
               return false;            
            }
           function GetWhere() {
               var where = "1=1";
               if ($("#ddlArea").val() != "") {
                   where += " and AreaCode='" + $('#ddlArea').val() + "'";
               }
               return where;
           }

           function PrintClick() {

               $('#HdnWH').val(document.documentElement.clientWidth + "#" + document.documentElement.clientHeight);
               return true;
           }

           function BindSelectUrl(objName) {
               var strWhere = GetWhere();
               var Comd = "Cmd.SelectTaskQueryProduct";
               if (objName == "SelectWin") {
                   $('#dgSelect').datagrid({
                       url: '../../Handler/BaseHandler.ashx?Action=PageDate&Comd=' + Comd,
                       pageNumber: 1,
                       queryParams: { Where: encodeURIComponent(strWhere) }
                   });
               } else {
                   $('#dgSelectTwo').datagrid({
                       url: '../../Handler/BaseHandler.ashx?Action=PageDate&Comd=' + Comd,
                       pageNumber: 1,
                       queryParams: { Where: encodeURIComponent(strWhere) }
                   });
               }
           }
           function AddRow(ObjName, RowData) {
               if (ObjName == "SelectWin") {
                   $("#txtProductCode").val(RowData.ProductCode);
               } else {
                   var HdnProduct = $("#HdnProduct").val();
                   if (HdnProduct=="") {
                       $("#HdnProduct").val("'" + RowData.ProductCode + "'");
                       $('#btnProduct').val("取消指定");

                   } else {
                       $("#HdnProduct").val(HdnProduct + ",'" + RowData.ProductCode + "'");
                   }
               }
           }
    </script>
   
    </head>
<body  style="overflow:hidden;">
  <form id="form1" runat="server"> 
     
    <table  style="width:100%;height:100%;" >
        <tr runat ="server" id = "rptform" valign="top">
            <td align="left" style="width:100%; height:30px;" >
                <table class="maintable"  width="100%" align="center" cellspacing="0" cellpadding="0">
                    <tr  style=" border-bottom:1px solid #ffffff;" >
                        <td   align="center" class="musttitle" style=" width:6%" >
                            作业日期 
                        </td>
                        <td align="left"   style="width:115px;" >
                            <uc1:Calendar ID="txtStartDate" runat="server" /> 
                        </td> 
                        <td align="center" style=" width:3%">
                        至
                        </td>                                
                        <td align="left"   style="width:115px;" >
                             <uc1:Calendar ID="txtEndDate" runat="server" />
                         </td>
                         
                        <td align="center" class="smalltitle" style=" width:6%">
                            任务类型 
                        </td>
                        <td align="left" style="width:6%;">
                            <asp:DropDownList ID="ddlBillType" runat="server" Width="96%">
                            </asp:DropDownList>
                        </td>
                             
                        <td align="center" class="smalltitle" style=" width:6%">
                            任务状态</td>
                        <td align="left" style="width:6%;">
                           
                            <asp:DropDownList ID="ddlState" runat="server" Width="96%">
                                <asp:ListItem Selected="True" Value="0">请选择</asp:ListItem>
                                <asp:ListItem Value="1">未完成</asp:ListItem>
                                <asp:ListItem Value="2">完成</asp:ListItem>
                                <asp:ListItem Value="3">取消</asp:ListItem>
                            </asp:DropDownList>
                           
                        </td>
                         <td align="center" class="smalltitle" style=" width:6%">
                             库区</td>
                        <td align="left" style="width:8%;">
                           
                            <asp:DropDownList ID="ddlArea" runat="server" Width="96%">
                            </asp:DropDownList>
                           
                        </td>                                     
                        <td   align="center" class="smalltitle"  style=" width:6%">
                            模具
                        </td>
                         <td >
                             <asp:TextBox ID="txtProductCode" runat="server" CssClass="TextBox"  Width="96%"></asp:TextBox>
                         </td>
                         
                        <td  style=" width:6%" >
                             <asp:Button ID="btnProduct" runat="server" CssClass="ButtonOption" 
                                Text="指定"  OnClientClick="return ProductClick();" Width="70px" />
                        </td>
                        <td align="center" >
                             &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="ButtonQuery" 
                                 tabIndex="2" onclientclick="return PrintClick();" 
                                Text="查询" Width="58px" onclick="btnPreview_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnRefresh" runat="server" CssClass="ButtonReset" 
                                OnClientClick="return Refresh()" tabIndex="2" Text="重新过滤" Width="80px" />
                        </td>
                        <td colspan="2">
                        </td>                                           
                    </tr>
                </table>  
            </td>
        </tr>
        <tr>
            <td runat ="server" id = "rptview" valign="top" align="left">
                <table style="width:90%;height:100%;">
                    <tr>
                        <td >           
                          <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" ButtonsPath="images\buttons1"
                                Font-Bold="False" Height = "100%" OnStartReport="WebReport1_StartReport"
                                ToolbarColor="Lavender" Width="100%" Zoom="1" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>         
        <input id="HdnProduct" type="hidden" runat="server" />      
       <input id="HdnWH" type="hidden" runat="server" value="0#0" />
   </form>


    <div id="SelectWin" style="width:600px;height:500px;display:none">
       <table id="dgSelect" class="easyui-datagrid"
            data-options="loadMsg: '正在加载数据，请稍等...',fit:true, rownumbers:true,
                         pagination:true,pageSize:PageSize, pageList:[15, 20, 30, 50],method:'post',modal:true,striped:true,fitcolumns:true,toolbar:'#tbSelect',singleSelect:true,onDblClickRow:DblClickRow,onSelect:SelectSingleCheckRow,onUnselect:SelectSingleUnCheckRow"> 
            <thead>
                    <tr>
                        <th data-options="field:'ProductCode',width:130">模具编号</th>
                        <th data-options="field:'ProductNo',width:130">产品编号</th>
                        <th data-options="field:'ProductName',width:150">品名</th>
                        <th data-options="field:'Memo',width:120">备注</th>
                    </tr>
            </thead>            
        </table>
        <div id="tbSelect" style="padding:5px;height:auto">
           <table>
                <tr>
                     <td>
                        模具编号
                        <input id="txtQueryProductCode" class ="easyui-textbox" style="width: 100px" /> 
                        产品编号
                        <input id="txtQueryProductNo" class ="easyui-textbox" style="width: 100px" />   
                        品名
                        <input id="txtQueryProductName" class="easyui-textbox" style="width: 100px" />   
                        
                    </td>
                    <td>
                         <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="ReloadGrid('dgSelect')">查询</a> 
                         <a href="javascript:void(0)"onclick="closeSelectWin()" class="easyui-linkbutton" data-options="iconCls:'icon-return'">确定</a>  
                    </td>
                </tr>          
           </table>
        </div>
    </div>


    <div id="SelectWinTwo" style="width:600px;height:500px;display:none">
       <table id="dgSelectTwo" class="easyui-datagrid"
            data-options="loadMsg: '正在加载数据，请稍等...',fit:true, rownumbers:true,
                         pagination:true,pageSize:PageSize, pageList:[15, 20, 30, 50],method:'post',modal:true,striped:true,fitcolumns:true,toolbar:'#tbSelectTwo',singleSelect:true,selectOnCheck:false,checkOnSelect:false,onCheck:SelectCheckRow,onUncheck:SelectUnCheckRow,onCheckAll:SelectCheckRowAll,onUncheckAll:SelectUnCheckRowAll,onDblClickRow:DblClickRow,onLoadSuccess:SelectLoadSelectSuccess"> 
            <thead>
                     <tr>
                        <th data-options="field:'',checkbox:true"></th>
                        <th data-options="field:'ProductCode',width:130">模具编号</th>
                        <th data-options="field:'ProductNo',width:130">产品编号</th>
                        <th data-options="field:'ProductName',width:150">品名</th>
                        <th data-options="field:'Memo',width:120">备注</th>
                    </tr>
            </thead>            
        </table>
        <div id="tbSelectTwo" style="padding:5px;height:auto">
           <table>
                <tr>
                     <td>
                        模具编号
                        <input id="txtQueryProductCodeMulti" class ="easyui-textbox" style="width: 100px" /> 
                        产品编号
                        <input id="txtQueryProductNoMulti" class ="easyui-textbox" style="width: 100px" />   
                        品名
                        <input id="txtQueryProductNameMulti" class="easyui-textbox" style="width: 100px" />   
                        <a  href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="ReloadGrid('dgSelectTwo')" >查询</a>
                    </td>
                    <td>
                         <a href="javascript:void(0)"onclick="closeSelectWin()" class="easyui-linkbutton" data-options="iconCls:'icon-return'">确定</a>  
                    </td>
                </tr>          
           </table>
        </div>
    </div>
</body>
</html>
