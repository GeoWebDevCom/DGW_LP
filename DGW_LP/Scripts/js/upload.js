$(document).ready(function () {
    
    $(".submit").submit(function (e) {
        if (!confirm("Bạn có chắc là muốn xóa video này?")) {
            event.preventDefault();
            return;
        }
    });




});



isSelectingImg = false;



function StarUploadVideo() {
    if (document.getElementById("uploadFile").files.length > 0) {
        isSelectingImg = true;
        var file = document.getElementById("uploadFile").files[0];
        if (file.size < 50000000) {
            UploadVideo();
        } else {
            alert("Video quá lớn. Cho phép dung lượng tối đa là 50 MB");
        }
    } else {
        alert("Chưa chọn video");
    }
   
}
function UploadVideo() {
    // Check data before upload
    var title = $.trim($("#title").val());
    var author = $.trim($("#author").val());
    var desc = $.trim($("#desc").val());

    if (title == "" || author == "" || desc == "") {
        $("#error").show();
        return;
    }

    var file = document.getElementById("uploadFile").files[0];
    var formdata = new FormData();
    $("#upload-modal").modal({
        backdrop: false
    });
    formdata.append("FileUpload", file);
    formdata.append("Title", title);
    formdata.append("Author", author);
    formdata.append("Desc", desc);

    var ajax = new XMLHttpRequest();
    ajax.upload.onprogress = function (e) {
        var percent = Math.round((e.loaded / e.total) * 1000) / 10;
        $("#photo-upload-progressbar").css('width', percent + '%').attr('aria-valuenow', percent);
        $("#photo-upload-progressbar").html(percent + '%');
    };

    ajax.onreadystatechange = function (e) {
        if (4 === this.readyState) {
            $("#upload-modal").modal("hide");
            //Upload done
            if (this.response == "OK") {
                // Reload admin
                location.reload();
            } else {
                alert("Server error");
            }
        }
    };
    ajax.addEventListener("error", function (evt) { UploadFailed(evt); }, false);
    ajax.open("POST", "/Home/UploadVideo");
    ajax.send(formdata);


}
function UploadFailed(e) {
    alert("Upload failed");
}