$(function($) {
    $.each($("#menu li"), function(index, value) {
        if($(value).children("a:first").attr("href") == window.location.pathname) {
            $(value).addClass("selected");
        }
    });

    $(".sampleRequests, .sampleResponses").tabs();
});