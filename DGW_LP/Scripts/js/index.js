$(document).ready(function () {
    $("#home").removeClass("active");
    $("#binhchon").removeClass("active");
    $("#tltag").removeClass("active");

    $("#home").addClass("active");


    var winHeight = $(window).height();
    $(".fullpage").css("height", winHeight - 60);
    $(".fullpage-bot").css("height", winHeight - 100);
});


function ShowDesc(index, htmlItem) {
    $(".step-active").removeClass("step-active");
    $(htmlItem).addClass("step-active");
    $(".content-inside").hide();
    $("#desc" + index).show();
}




