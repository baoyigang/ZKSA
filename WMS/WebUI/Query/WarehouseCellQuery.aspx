<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WarehouseCellQuery.aspx.cs" Inherits="WebUI_Query_WarehouseCellQuery" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
    <script type="text/javascript">
        function LoadTree(node, data) {
            $("#whTree li:eq(1)").find("div").addClass("tree-node-selected");   //设置第一个节点高亮  
            var n = $("#whTree").tree("getSelected");
            if (n != null) {
                $("#whTree").tree("select", n.target);
            }   //相当于默认点击了一下第一个节点，执行onSelect方法  
       }
       function SelectNote(node) {
           var tNodeID = node.id;
           var text = node.text;
           var tNodeIDLen = tNodeID.length;
           var hasChild = node.children == undefined ? false : true;
           var tRootID = $('#whTree').tree('getRoot', node.target).id;
           var IsRoot = tRootID == tNodeID ? true : false;
           var sRegionCode = "";
           var sAreaCode = "";
           var sWareHouse = "";
           var ShowCell = "<td style=\"width:30px; height:15px; background-color:Red\"></td><td>异常货位</td>" +
                          "<td style=\"width:30px; height:15px; background-color:Gray\"></td><td>禁用货位</td>" +
                          "<td style=\"width:30px; height:15px; background-color:Blue\"></td><td>有货</td>" +
                          "<td style=\"width:30px; height:15px; background-color:LawnGreen\"></td><td>入库锁定</td>" +
                          "<td style=\"width:30px; height:15px; background-color:Green\"></td><td>出库锁定</td>" +
                          "</tr></table>";
           var ShowCellTag = "<table style=\"width:100%;\" valign=\"middle\"><tr><td style=\"width:30%;\">当前选中的节点：" + text + " </td> <td style=\"width:20%;\"></td>" + ShowCell;
           $('#cc').layout('panel', 'center').panel('setTitle', ShowCellTag);
           if (hasChild) {
               if (IsRoot) { //顶级节点，仓库资料
                   $("#frmMain_warehouse").attr("src", "");
               }
               else {

                   var ParentNodeID = $('#whTree').tree('getParent', node.target).id;
                   if (ParentNodeID == tRootID) { //子节点-仓库资料
                       sWareHouse = tNodeID;
                       $("#frmMain_warehouse").attr("src", "WarehouseCell.aspx?WareHouse=" + sWareHouse + "&AreaCode=" + sAreaCode + "&RegionCode=" + sRegionCode);
                   }
                   else {
                       //区域
                       sAreaCode = tNodeID;
                       $("#frmMain_warehouse").attr("src", "WarehouseCell.aspx?WareHouse=" + ParentNodeID + "&AreaCode=" + sAreaCode + "&RegionCode=" + sRegionCode);
                   }
               }
           }
           else {
               //库区
               var ShelfParentNode = $('#whTree').tree('getParent', node.target);
               sAreaCode = ShelfParentNode.id;
               sRegionCode = tNodeID;
               $("#frmMain_warehouse").attr("src", "WarehouseCell.aspx?WareHouse=" + sWareHouse + "&AreaCode=" + sAreaCode + "&RegionCode=" + sRegionCode);
           }
       }
    </script>
</head>
<body id="cc" class="easyui-layout">
    <div data-options="region:'north',split:true" style="height:40px;">
        <table style="width:100%">
            <tr>
                <td align="right">
                    <a href="javascript:void(0)" onclick="Exit()" class="easyui-linkbutton" data-options="iconCls:'icon-no',plain:true">离开</a>
                </td>
            </tr>
        </table>
    </div>   
    <div data-options="region:'west',split:true," style="width:200px;">
        <ul id="whTree" class="easyui-tree" data-options="url:'WareHouseTree.ashx',onLoadSuccess:LoadTree, onSelect:SelectNote "></ul>
    </div>   
    <div id="ShowDiv" data-options="region:'center',title:'当前选中的节点：'" style="padding:2px;overflow:hidden">   
         <iframe id="frmMain_warehouse" scrolling="no" frameborder="0" width="100%" height="100%" src=""> </iframe>
    </div>
</body>
</html>
