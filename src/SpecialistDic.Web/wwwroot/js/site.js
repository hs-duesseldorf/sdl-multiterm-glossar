function cleanup() {
    $(".active").removeClass("active");
}
$(document).ready(function () {
    $(".side-nav a").on("click", function (e) {
        cleanup();
        var anchor = $(e.target).attr("href").split("#", 2)[1];
        $("#" + anchor).addClass("active");
    });
});
