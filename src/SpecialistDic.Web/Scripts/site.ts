
function cleanup() {
    $(".active").removeClass("active");
}

$(document).ready(() => {
    $(".side-nav a").on("click",
        (e) => {
            cleanup();
            let anchor = $(e.target).attr("href").split("#", 2)[1];
            $("#" + anchor).addClass("active");
        });
});