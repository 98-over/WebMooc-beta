window.onload = function () {

    
    
    var dl = document.getElementsByName("download");

    for (var i = 0; i < dl.length; i++) {
        var t = dl[i]

        t.onclick = function () {

            var path = this.previousElementSibling.value;
            var begin = path.indexOf("5/");
            var last = path.length;

           

            window.location.href = "Down.aspx?path=" + path.substring(begin + 1, last);
          


        }
    }

    

   




}



