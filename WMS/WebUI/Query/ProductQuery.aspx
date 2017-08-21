<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductQuery.aspx.cs" Inherits="WebUI_Query_ProductQuery" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title> 
        <link rel="stylesheet" type="text/css" href="~/Css/default.css" />
        <link rel="stylesheet" type="text/css" href="~/Css/icon.css" /> 
        <link href="../../Css/op.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="../../EasyUI/jquery.min.js"></script>
        <script src="../../JScript/Common.js" type="text/javascript"></script>
        <script src="../../JScript/DataProcess.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $(window).resize(function () {
                    resize();
                });
            });
            function resize() {
                var h = document.documentElement.clientHeight - 30;
                $("#rptview").css("height", h);
            }
           function PrintClick() {
                $('#HdnWH').val(document.documentElement.clientWidth + "#" + document.documentElement.clientHeight);
                return true;
            }
        </script>
   
    <style type="text/css">
        .style1
        {
            width: 133px;
        }
    </style>
   
    </head>
<body  style="overflow:hidden;">
  <form id="form1" runat="server"> 
    <table  style="width:100%;height:100%;" >
        <tr runat ="server" id = "rptform" valign="top">
            <td align="left" style="width:100%; height:30px;" >
                <table class="maintable"  width="100%" align="center" >
                    <tr>
                        <td  align="center" class="smalltitle" style="width:6%;">
                            模具编码
                        </td>
                         <td style="width:10%;" >
                        
                              &nbsp;<asp:textbox id="txtProductCode" tabIndex="1" runat="server" Width="90%" CssClass="TextBox"></asp:textbox>
                        </td>
                        <td   align="center" class="smalltitle" style="width:6%;">
                            托盘编码
                        </td>
                        <td align="left"   style="width:10%;">
                         &nbsp;<asp:textbox id="txtPalletCode"   runat="server"  Width="90%" CssClass="TextBox" ></asp:textbox> 
                        </td>
                        <td   align="center" class="smalltitle" style="width:6%;">
                            产品编号
                        </td>
                        <td align="left"   style="width:10%;">
                         &nbsp;<asp:textbox id="txtProductNo"   runat="server"  Width="90%" CssClass="TextBox" ></asp:textbox>
                        </td>
                        <td   align="center" class="smalltitle" style="width:4%;">
                            品名
                        </td>
                        <td align="left"   style="width:10%;" >
                             &nbsp;<asp:textbox id="txtProductName" tabIndex="1" runat="server" Width="90%" CssClass="TextBox"></asp:textbox>
                       </td>
                       <td class="style1">
                           <asp:CheckBox ID="CheckBox1" runat="server" Text="低于安全存量" />
                        
                       </td>
                        <td align= "left" style="border-left:2px solid #ffffff;">
                             &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="ButtonQuery" 
                                  tabIndex="2" Text="查询" onclick="btnPreview_Click" Width="58px"  OnClientClick="return PrintClick();"/> &nbsp;&nbsp;
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
</body>
</html>
