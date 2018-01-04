<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WarehouseCell.aspx.cs" Inherits="WebUI_Query_WarehouseCell" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>货位信息显示</title>
    <link href="~/Css/Main.css" rel="stylesheet" type="text/css" />
    <link href="~/Css/op.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../EasyUI/jquery.min.js"></script>
    <script src="../../JScript/DataProcess.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var oldcell;
        $(document).ready(function () {
            $(window).resize(function () {
                resize();
            });
        });
        function resize() {
            var h = document.documentElement.clientHeight;
            $("#pnlCell").css("height", h - 20);
        }

        function selectedcell(cellobj) {
            var obj = document.getElementById(cellobj);
            if (oldcell != null) {
                $("#" + oldcell.id).removeClass("cellfalg");

            }

            $("#" + obj.id).addClass("cellfalg");
            oldcell = obj;
        }
        function ShowCellInfo(obj) {
            closeinfo();
            selectedcell(obj);
            var product = document.getElementById("productinfo");
            var url = "../../Handler/BaseHandler.ashx";
            var data = { Action: 'FillDataTable', Comd: 'cmd.SelectWareHouseCellQueryByWhere', Json: "[{\"{0}\": \"CellCode='" + obj + "'\"}]" };
            $.post(url, data, function (result) {
                var json = result.rows[0];
                document.getElementById("RegionName").innerText = json.RegionName;
                document.getElementById("CellName").innerText = json.CellName;
                document.getElementById("Indate").innerText = json.InDate;
                document.getElementById("ProductName").innerText = json.ProductName;
                document.getElementById("BatchNo").innerText = json.BatchNo;
                document.getElementById("SectionName").innerText = json.SectionName;
                document.getElementById("Qty").innerText = json.Qty;
                if (json.IsLock == "1")
                    document.getElementById("Status").innerText = "锁定";
                else if (json.ErrorFlag == "1") {
                    document.getElementById("Status").innerText = "异常";
                }
                else if (json.IsActive == "0") {
                    document.getElementById("Status").innerText = "禁用";
                }
                else { document.getElementById("Status").innerText = "正常"; }

            }, 'json');
            showinfo(obj);
        }
        function showinfo(cellobj) {
            var obj = document.getElementById(cellobj);
            var product = document.getElementById("productinfo");
            var objtop = obj.offsetTop;
            var objheight = obj.clientHeight;
            var objleft = obj.offsetLeft;
            product.style.top = parseFloat(objtop + objheight) + "px";
            if ((objleft + parseFloat(product.style.width)) > document.body.clientWidth) {
                product.style.left = parseFloat(objleft) - parseFloat(product.style.width) + "px";
            }
            else
                product.style.left = parseFloat(objleft) + "px";

            product.style.display = "block";
        }
        function closeinfo() {
            var product = document.getElementById("productinfo");
            product.style.display = "none";
        }
    </script>
    <style type="text/css">
    .cellfalg
    {
         background-image:url(../../images/flag.png);
         background-repeat:no-repeat;   
    }
    .cellinfo
    {
   width:65px;
   font-size:12px;
    }
    .td
    {
        border:1px solid #ffffff;
        color:Black;
        
     }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnlCell" runat="server"  Width="100%" Height="450px"  style="overflow:auto;" >
        </asp:Panel>
        <div id="productinfo"  style=" width:360px; height:230px; position:absolute; background-color:#dbe7fd; display:none; border:1px solid #000;">
            <div id="btclose" style=" width:100%;height:20px ;line-height:20px;">
              <span id="cellcode" style="float:left;"><b>货架信息</b></span>
              <span onclick="closeinfo()"  style=" float:right; width:15px; height:20px;  cursor:pointer">X</span>
            </div>
            <div>    
               <table class="maintable" style="width:100%;">
                   <tr>
                      <td align="right" class=" musttitle" style="width:12%;" >
                             &nbsp;库区:
                      </td>
                      <td id="RegionName" class="td">
                          
                      </td>
                       <td  align="right" class="musttitle"  style="width:12%;">
                             &nbsp;货位:
                      </td>
                      <td id="CellName" class="td"> 
                          
                      </td>
                       
                   </tr>
                   <tr>
                        <td align="right"  class="musttitle"  style="width:12%;">
                             &nbsp;状态:
                        </td>
                        <td id="Status" class="td">
                          
                        </td>
                        <td align="right"  class="musttitle"  style="width:12%;">
                             &nbsp;时间:
                        </td>
                        <td id="Indate" class="td">
                          
                        </td>
                   </tr> 
                    <tr>
                        <td  align="right" class="musttitle" style="width:12%;">
                                &nbsp;产品:
                        </td>
                        <td id="ProductName"class="td" >
                          
                        </td>
                        <td align="right" class="musttitle"  style="width:12%;">
                                &nbsp;批次:
                        </td>
                        <td id="BatchNo"class="td" >
                          
                        </td>
                    </tr>
                     <tr>
                        <td  align="right" class="musttitle" style="width:12%;">
                                &nbsp;阶段:
                        </td>
                        <td id="SectionName"class="td" >
                          
                        </td>
                        <td align="right" class="musttitle"  style="width:12%;">
                                &nbsp;数量:
                        </td>
                        <td id="Qty"class="td" >
                          
                        </td>
                    </tr>
                    
             </table> 
              
            </div>
        </div>
   </form>
</body>

</html>
