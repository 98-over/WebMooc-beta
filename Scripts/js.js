var xmlHttp;

function ajaxFunction() {
    if (XMLHttpRequest) {
        xmlHttp = new XMLHttpRequest();
    }
    else {

        xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
    }

}
function deleteFile() {
    var tdelete = document.getElementsByName("delete");
    for (var i = 0; i < tdelete.length; i++) {
        var t = tdelete[i]

        t.onclick = function () {

            if (confirm("确定要删除吗？")) {

                var fid = this.parentElement.children[0].value;
                ajaxFunction();
                var url = "DeleteCourse.aspx?id=" + fid;
                xmlHttp.open("get", url, true);

                xmlHttp.send();
                xmlHttp.onreadystatechange = function () {
                    if (xmlHttp.readyState == 4 && xmlHttp.status == 200) {

                        window.location.href = document.URL
                        alert("删除成功");


                    }

                }

            }
        }
    }



}
function edit() {

    var td = document.getElementsByName("edit");

    for (var i = 0; i < td.length; i++) {
        var t = td[i]

        t.onclick = function () {
           
            var fid = this.parentElement.children[0].value;


            var url = "Edit.aspx?id=" + fid;
            window.open(url);

            }
        }
    }
function changeValue(obj) {

    $(obj).attr("value", $(obj).val());

}

window.onload = function () {

    deleteFile();
    edit();

}
