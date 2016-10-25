$(document).ready(function () {
    $("#home").removeClass("active");
    $("#binhchon").removeClass("active");
    $("#tltag").removeClass("active");

    $("#binhchon").addClass("active");

  //  var winHeight = $(window).height();
    $("#thele").css("height", 865);


    $('#myModal').modal({
        backdrop: 'static',
        keyboard: false
    });

});