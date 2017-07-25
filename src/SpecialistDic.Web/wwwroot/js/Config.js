requirejs.config({
    "baseUrl": "/js",
    "paths": {
        "jquery": "https://cdn.zdv.uni-mainz.de/web/jquery/2.x/jquery.min",
        "Foundation": "https://cdn.zdv.uni-mainz.de/web/zurb-foundation/sites/6.3.0/dist/js/foundation",
        "react-dom": "https://cdn.zdv.uni-mainz.de/web/react/15.x/react-dom.min",
        "react": "https://cdn.zdv.uni-mainz.de/web/react/15.x/react.min",
        "dashboard": "https://cdn.zdv.uni-mainz.de/web/designs/jgudashboard/js/dashboard",
        "moment": "https://cdn.zdv.uni-mainz.de/web/momentjs/2.15/moment",
        "foundation-datepicker": "https://cdn.zdv.uni-mainz.de/web/zurb-foundation/datepicker/1.5/foundation-datepicker.min",
        "jquery-typeahead": "https://cdn.zdv.uni-mainz.de/web/jquery/typeahead/2.7/jquery.typeahead.min",
        "cropper": "https://cdn.zdv.uni-mainz.de/web/jquery/cropper/2.3/cropper.min",
        "core": ".."
    },
    "shim": {
        "foundation-datepicker": { deps: ["jquery"] },
        "Foundation": { deps: ["jquery"] },
        "cropper": { deps: ["jquery"] }
    }
});
define(["jquery", "Foundation", "dashboard"], function ($, fs, dashboard) {
    dashboard.initialize();
});
///TODO: implement this method for general use -> issue in 'foundation.smoothScroll.js' @foundation github
function scrollToLoc(targetId, containerId) {
    $().foundation('scrollToLoc', targetId);
    /*
    var scrollPos = 0;
    var titleBarOffset = 112;

    if ($(containerId).scrollTop() === 0) {
        scrollPos = $(targetId).offset().top;
    } else if (($(containerId).scrollTop() + titleBarOffset) === $(targetId).offset().top) {
        return;
    }

    console.log("scroll-POS");
    console.log(scrollPos);

    $(containerId).stop(true).animate({ scrollTop: scrollPos - titleBarOffset }, 500, "linear");
    console.log("scroll-top");
    console.log($(containerId).scrollTop());
    //$(containerId).scrollTop(0);
    //console.log($(targetId));
    */
}
;
