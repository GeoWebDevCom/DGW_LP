$(document).ready(function () {
   
    if (window.location.href.substr(-4) === "#_=_") {
        // do what you need here
        window.history.pushState('page2', 'Title', window.location.href.substring(0, window.location.href.length - 4));
    }

});

function fbshareCurrentPage() {
    window.open("https://www.facebook.com/sharer/sharer.php?u=" + escape(window.location.href) + "&t=" + document.title, '',
    'menubar=no,toolbar=no,resizable=yes,scrollbars=yes,height=300,width=600');
    return false;
}