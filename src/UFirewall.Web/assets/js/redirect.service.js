var redirectService = {};

redirectService.refresh = function (timeout) {
    setTimeout(function () { window.location.href = window.location.href; }, timeout);
}