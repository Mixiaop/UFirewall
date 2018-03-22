var page = {};

page.open = function(url, w, h) {

    if (w == null && w <= 400) {
        w = "400";
    }
    if (h == null && h <= 620) {
        w = "620";
    }
    window.open(url, "", "width=" + w + ",height=" + h + ",location=no,menubar=no,status=no,resizable=yes,scrollbars=yes,top=20,left=20");
}