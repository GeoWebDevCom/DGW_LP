$(document).ready(function () {
    $("#home").removeClass("active");
    $("#binhchon").removeClass("active");
    $("#tltag").removeClass("active");

    $("#binhchon").addClass("active");

    var winHeight = $(window).height();
    $(".fullpage-bot").css("height", 865);



    $("#pagination").pagination({
        items: 60,
        itemsOnPage: 6,
        ellipsePageSet: false,
        cssStyle: 'light-theme',
        nextText: "Tiếp"
    });

});