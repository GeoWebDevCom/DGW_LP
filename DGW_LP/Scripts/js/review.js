$(document).ready(function () {
    $("#home").removeClass("active");
    $("#binhchon").removeClass("active");
    $("#tltag").removeClass("active");

    $("#binhchon").addClass("active");

  //  var winHeight = $(window).height();
    $("#thele").css("height", $(window).height() - 200);


    $('#myModal').modal({
        backdrop: 'static',
        keyboard: false
    });

    if ($("#hasvoted").text() != "0") {
        hasVoted = true;
    }


    if ($("#coll").is(":visible")) {
        $(".laptop-bg").hide();
        $(".rv-desc").hide();
        $("#fake-rv-desc").show();
        $("#fake-laptop-bg").show();
        $(".modal-lg").css("width", "auto");
        $(".modal-body").css("padding", "10px");
        $(".modal-open").css("margin-right", "0px");
        $(".modal-open .navbar-fixed-top").css("margin-right", "0px");
        $(".hearts-text").css("text-align", "left");
        $(".rv-author").css("margin-bottom", "10px");
        $("#fake-btn").show();
    } else {
        $("#fake-rv-desc").hide();
        $("#fake-laptop-bg").hide();
    }

});

var page = 1;
function LoadMoreComment(htmlItem, vId) {
    page++;
    $(htmlItem).prop("disabled", true);
    $(htmlItem).html("<i class='fa fa-spinner fa-pulse fa-fw'></i> Đang tải bình luận");

    $.ajax({
        method: "POST",
        url: "/Home/GetMoreComment",
        data: { vId: vId , page: page}
    }).success(function (data) {
        if (data.data != "[]") {
            var list = jQuery.parseJSON(data);
            for (var i = 0; i < list.data.length; i++){
                $("#comment-list").append("<div class='comment-item'><div class='avatar'>" 
                  +  "<img style='height:100%;width:100%' src='" + list.data[i].Avatar + "' /> </div>"
                                            + "<div class='comment-content'><label>" + list.data[i].Name + "</label> "
                                               + "<div>" + list.data[i].Content + "</div></div></div>");
            }

            if (list.final) {
                $(htmlItem).hide();
            } else {
                $(htmlItem).prop("disabled", false);
                $(htmlItem).html("Xem thêm các bình luận khác");
            }
         
        }
    });

  
}

hasVoted = false;

function VoteVideo(vId, htmlItem) {
    if (!hasVoted) {
        hasVoted = true;
        $.ajax({
            method: "POST",
            url: "/Home/VoteVideo",
            data: { videoId: vId }
        }).success(function (result) {
            if (result == "OK") {
                $(htmlItem).addClass("voted");
                var currentCount = parseInt($.trim($(htmlItem).text()));
                $(htmlItem).html((currentCount + 1) + " <i class='fa fa-lg fa-heart' aria-hidden='true'></i>");
                $(htmlItem).prop('title', 'Bạn đã bình chọn video này');

                $(htmlItem).attr("onclick", "CancelVote(" + vId + ",'#unique')");
            }
        });
    }
}



function CancelVote(vId, htmlItem) {
    if (hasVoted) {
        hasVoted = false;
        $.ajax({
            method: "POST",
            url: "/Home/CancelVote",
            data: { videoId: vId }
        }).success(function (result) {
            if (result == "OK") {
                $(htmlItem).removeClass("voted");
                var currentCount = parseInt($.trim($(htmlItem).text()));
                $(htmlItem).html((currentCount - 1) + " <i class='fa fa-lg fa-heart' aria-hidden='true'></i>");
                $(htmlItem).prop('title', 'Bình chọn video này');

                $(htmlItem).attr("onclick", "VoteVideo(" + vId + ",'#unique')");
            }
        });
    }
}
