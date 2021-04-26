window.onload = function () {
    //播放速度
    var eleSelect = document.getElementById('selRate');
    // 视频播放元素
    var video = document.getElementById('video');
    // 改变播放速率
    eleSelect.addEventListener('change', function () {
        video.playbackRate = this.value;
    });

    //获取视频文件

    var videourl = document.getElementById("DropDownList1")
  
    videourl.addEventListener('change', function () {


        fileName = this.value.lastIndexOf(".");//获取到文件名开始到最后一个“.”的长度。
        fileNameLength = this.value.length;//获取到文件名长度

        fileFormat = this.value.substring(fileName + 1, fileNameLength);//截取后缀名


        if (fileFormat == 'mp4') {


            video.src = this.value;

        }
        else if (fileFormat == 'jpg'  || fileFormat == 'png'){

            window.open(this.value)


        }
        else {
            var i = this.value.lastIndexOf("/");
            
            var tmp_imgname = this.value.slice(i + 1);
            
            window.open("https://localhost:44315/Ui_hs/ToPdf.aspx?name=" + tmp_imgname)

        }
        
    });

        var dl = document.getElementsByName("download");
        for (var i = 0; i < dl.length; i++) {
            var t = dl[i]

            t.onclick = function () {



                var path = previousElementSibling.value;

                window.location.href = "Download.aspx?id=" + path;




            }
        }




}


    
