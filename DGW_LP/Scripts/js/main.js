$(document).ready(function () {
   
    if (window.location.href.substr(-4) === "#_=_") {
        // do what you need here
        window.history.pushState('page2', 'Title', window.location.href.substring(0, window.location.href.length - 4));
    }


    if ($("#coll").is(":visible")) {
        $(".my-navbar li").css("width", "100%");
        $(".my-navbar li").css("display", "block");
        $(".my-navbar .lg-block").css("display", "block");
        $(".lg-block-2").css("display", "none");
    } else {
        $(".my-navbar .lg-block").css("display", "none");
    }
});

function fbshareCurrentPage() {
    window.open("https://www.facebook.com/sharer/sharer.php?u=" + escape(window.location.href) + "&t=" + document.title, '',
    'menubar=no,toolbar=no,resizable=yes,scrollbars=yes,height=300,width=600');
    return false;
}