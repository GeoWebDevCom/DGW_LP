$(document).ready(function () {
    $("#home").removeClass("active");
    $("#binhchon").removeClass("active");
    $("#tltag").removeClass("active");

    $("#tltag").addClass("active");

    var winHeight = $(window).height();
    $(".fullpage-bot").css("height", 865);
});