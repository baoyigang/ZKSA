<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoldStrokeTotal.aspx.cs" Inherits="WebUI_Query_MoldStrokeTotal" %>

<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>

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
           });

           function getQueryParams(objname, queryParams) {
               var Where = "1=1 and ModelNo!='' and Weight !=0";
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
               } 
               queryParams.Where = encodeURIComponent(Where);
               return queryParams;
           }

           function resize() {
               var h = document.documentElement.clientHeight - 30;
               $("#rptview").css("height", h);
           }
           function PrintClick() {
               $('#HdnWH').val(document.documentElement.clientWidth + "#" + document.documentElement.clientHeight);
               return true;
           }
           function AddRow(ObjName, RowData) {
               if (ObjName == "SelectWin") {
                   $("#txtProductCode").val(RowData.ProductCode);
               } 
           }
           function BindSelectUrl(objName) {
               var Comd = "Cmd.SelectWeightProduct";
               if (objName == "SelectWin") {
                   $('#dgSelect').datagrid({
                       url: '../../Handler/BaseHandler.ashx?Action=PageDate&Comd=' + Comd,
                       pageNumber: 1,
                       queryParams: { Where: encodeURIComponent("1=1 and ModelNo!='' and Weight !=0") }
                   });
               }
           }
        </script>
   
    </head>
<body  style="overflow:hidden;">
  <form id="form1" runat="server"> 
    <table  style="width:100%;height:100%;" >
        <tr runat ="server" id = "rptform" valign="top">
            <td align="left" style="width:100%; height:30px;" >
                <table class="maintable"  width="100%" align="center" >
                    <tr>
                         <td   align="center" class="musttitle" style="width:12%;">
                            模具编号 
                        </td>
                        <td align="left"   style="width:20%;" >
                         <asp:textbox id="txtProductCode"   runat="server"  Width="83%" 
                                CssClass="TextBox" ></asp:textbox>
                            <input type="button" id="btnProductNo" class="ButtonCss" onclick="SelectWinShow('SelectWin','模具选择')" value="..."/>
                        </td>                       
                        <td class="musttitle" align="center" style="width:5%;" >
                            选项
                         </td>
                         <td width="20%" align="center" style="color:Black">
                             <asp:RadioButton ID="rpt1" runat="server" Checked="True" GroupName="Rpt" Text="全部" />&nbsp;&nbsp;&nbsp;
                             <asp:RadioButton ID="rpt2" runat="server" GroupName="Rpt" Text="超过" />&nbsp;&nbsp;&nbsp;
                             <asp:RadioButton ID="rpt3" runat="server" GroupName="Rpt" Text="不超过" />&nbsp;               
                        </td>
                        <td align= "center" style="border-left:2px solid #ffffff;">
                             &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="ButtonQuery" 
                                 tabIndex="2" Text="查询" Width="58px"  onclick="btnPreview_Click"
                                onclientclick="return PrintClick();" /> &nbsp;&nbsp;&nbsp;
                             <asp:Button ID="btnRefresh" runat="server" CssClass="ButtonReset" 
                                 OnClientClick="return Refresh()" tabIndex="2" 
                                 Text="重新过滤" Width="80px" />
                        </td>  
                                                                     
                    </tr>
                </table>  
            </td>
        </tr>
        <tr>
            <td runat ="server" id = "rptview" valign="top" align="left">
                <table style="width:100%;height:100%;">
                    <tr>
                        <td >           
                            <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" ButtonsPath="images\buttons1"
                                Font-Bold="False" Height = "100%"  OnStartReport="WebReport1_StartReport"
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
                        <th data-options="field:'Weight',width:120">标准冲程</th>
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
</body>
</html>
