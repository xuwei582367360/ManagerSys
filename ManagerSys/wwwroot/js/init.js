var title = "主业务管理 2022";
var index = setInterval("initHtml()", 100);
function initHtml() {
    var lent = document.getElementsByTagName("footer");
    if (lent.length > 0) {
        var $html = "";
        $html = '<div class="myglobalFooter"><div class="ant-row"><div class="copyright">' + title + '</div></div></div>';
        lent[0].innerHTML = $html;
        if (typeof index != undefined) {
            clearInterval(index)
        }
    }
}