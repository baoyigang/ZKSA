

//获得WebService中的方法。
function Ajax(code, para) {
    var xmlhttp = createxmlhttp();
    if (xmlhttp==null) {
        alert("你的浏览器不支持XMLHttpRequest");
        return;
    }
    xmlhttp.open("POST", HostPath + "WebServices/BaseService.asmx", false);
    xmlhttp.setRequestHeader("Content-Type", "application/soap+xml");
    var strPara = parseXmlPara(para);
    xmlhttp.send(getData(code, strPara));
    return getResult(xmlhttp);

}
function getResult(xmlhttp) {
    var result = "";
    result = xmlhttp.responseText;
    var type = result.substring(result.indexOf("<type>") + 6, result.indexOf("</type>"))
    kcajax_rowcount = result.substring(result.indexOf("<rows>") + 6, result.indexOf("</rows>"))
    if (result.indexOf("<data>") == -1) return "";
    result = result.substring(result.indexOf("<data>") + 6, result.indexOf("</data>"))
    result = $.trim(result).replace(/&amp;/g, '&');
    switch (type) {
        case "System.Byte":
        case "System.Int16":
        case "System.Int32":
        case "System.Int64":
            return parseInt(result);
        case "System.Decimal":
            return parseFloat(result);
        case "System.DateTime":
            return new Date(result);
        case "System.Boolean":
            return result == "True";
        case "ErrMsg":
            alert(result);
            return null;
        case "null":
            return null;
    }
    return result;
 }
function getData(code, strpara) {
    var data;
    data = '<?xml version="1.0" encoding="utf-8"?>';
    data = data + '<soap12:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap12="http://www.w3.org/2003/05/soap-envelope">';
    data = data + '<soap12:Body>';
    data = data + '<' + code + ' xmlns="http://tempuri.org/" >';
    if (strpara!=null) {
        data = data + strpara;
    }
    data = data + '</' + code + '>';
    data = data + '</soap12:Body>';
    data = data + '</soap12:Envelope>';
    return data;
}
function parseXmlPara(para) {
    if (para == null) return "";
    return "<xmlpara>"+parseJson(para)+"</xmlpara>";

}
function parseJson(para) {
    return "[{"+getJsonRowStr(para)+"}]";

}
function getJsonRowStr(para) {
    if (para == null) return "";
    var strpara = "";
    for (key in para) {
        if (strpara.length == 0)
            strpara = strpara + "\"" + key +"\":\"" + para[key] + "\"";
        else
            strpara = strpara + ",\"" + key + "\":\"" + para[key] + "\"";
    }
    return strpara;
}
function createxmlhttp() {

    var xmlhttp = window.XMLHttpRequest ? new window.XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHttp");
    return xmlhttp;
 }