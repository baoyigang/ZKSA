<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarCodeQuery.aspx.cs" Inherits="WebUI_Query_BarCodeQuery" %>

<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title> 
        <link href="~/Css/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Css/op.css" type="text/css" rel="stylesheet" /> 
   
        <script type="text/javascript" src="../../EasyUI/jquery.min.js"></script>
        <script type="text/javascript" src='<%=ResolveClientUrl("~/JScript/Common.js") %>'></script>
       <script type="text/javascript" src='<%=ResolveClientUrl("~/JScript/DataProcess.js") %>'></script>
       <script type="text/javascript">
           $(document).ready(function () {
               $(window).resize(function () {
                   resize();
               });
               //BindEvent();
           });
           function resize() {
               var h = document.documentElement.clientHeight - 30;
               $("#rptview").css("height", h);
           }
           function ProductClick() {
               var where = getWhere();
               getProductMultiItems("CMD_ProductInStock", "ProductCode", $('#btnProduct'), '#HdnProduct', where);
               return false;
           }
           function BindEvent() {
               $("#txtProductCode").bind("dblclick", function () {
                   var where = getWhere();
                   GetProductOtherValue("CMD_ProductInStock", "txtProductName,txtProductCode", "ProductName,ProductCode", where);
                   return false;
               });
               $("#txtProductCode").bind("change", function () {
                   var where = getWhere();
                   where += " and ProductCode='" + $('#txtProductCode').val() + "'";
                   getWhereBaseData('CMD_ProductInStock', "txtProductName,txtProductCode", "ProductName,ProductCode", where);
               });
           }
           function getWhere() {
               var where = "IsFixed<>'1'";
               if ($("#ddlProductType").val() != "") {
                   where += " and " + escape("CategoryCode='" + $('#ddlProductType').val() + "'");
               }
              
               return where;
           }


           function PrintClick() {
               $('#HdnWH').val(document.documentElement.clientWidth + "#" + document.documentElement.clientHeight);
               return true;
           }
        </script>
   
    </head>
<body  style="overflow:hidden; background-color:White">
  <form id="form1" runat="server"> 
    <table  style="width:100%;height:100%;" >
        <tr runat ="server" id = "rptform" valign="top">
            <td align="left" style="width:100%; height:30px;" >
                <table class="maintable"  width="100%" align="center" >
                    <tr>
                         <td   align="center" class="musttitle" style="width:6%;">
                            库区名称 
                        </td>
                         <td align="left"   style="width:12%;" >
                            <asp:DropDownList ID="ddlProductType" runat="server" Width="90%">
                            </asp:DropDownList>
                        </td>                        
                         <td   align="center" class="musttitle" style="width:5%;">
                            编码
                        </td>
                         <td align="left"   style="width:9%;" >
                         <asp:textbox id="txtProductCode"   runat="server"  Width="90%" CssClass="TextBox" ></asp:textbox>
                        </td>
                        <td class="musttitle" align="center" style="width:5%;" >
                            报表
                        </td>
                         <td width="6%" align="left">
                             <asp:RadioButton ID="rpt1" runat="server" Checked="True" GroupName="Rpt" Text="条码" />&nbsp;          
                        </td>
                        <td align= "left" style="border-left:2px solid #ffffff;width:18%">
                             &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="ButtonQuery" 
                                 onclick="btnPreview_Click" tabIndex="2" Text="普通查询" Width="40%" 
                                onclientclick="return PrintClick();" /> &nbsp;&nbsp;
                             <asp:Button ID="btnRefresh" runat="server" CssClass="ButtonReset" 
                                 OnClientClick="return Refresh()" tabIndex="2" 
                                 Text="重新过滤" Width="40%" />
                        </td>  
                        <td  align="center" class="musttitle" style="width:6%;">
                        范围查询
                        </td>
                        <td align="left" >
                            <asp:textbox ID="SelectBegin" runat="server" Width="35%">
                            </asp:textbox>
                            -
                             <asp:textbox ID="SelectEnd" runat="server" Width="35%">
                            </asp:textbox>
                        </td>     
                        <td align= "left" style="border-left:2px solid #ffffff;width:18%">
                             &nbsp;<asp:Button ID="btnAreaSearch" runat="server" CssClass="ButtonQuery" 
                                 onclick="btnArea_Click" tabIndex="2" Text="范围查询" Width="40%" 
                                onclientclick="return PrintClick();" /> &nbsp;&nbsp;
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
        <tr>
            <td runat ="server" id = "rptview2" valign="top" align="left">
                <table style="width:95%;height:100%;">
                    <tr>
                        <td>           
                     <cc1:WebReport ID="WebReport2" runat="server" BackColor="White" ButtonsPath="images\buttons1"
                                Font-Bold="False" Height = "100%" OnStartReport="WebReport2_StartReport"
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
</body>
</html>