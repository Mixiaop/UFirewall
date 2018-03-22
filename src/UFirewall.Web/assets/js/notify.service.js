var notifyService = {};
notifyService.success = function (title,message) {
    $.notify({
        title: title,
        message: message
    }, {
        placement: {
            from: "top",
            align: "center"
        },
        type: 'success'
    });
}

notifyService.error = function (title, message) {
    $.notify({
        title: title,
        message: message
    }, {
        placement: {
            from: "top",
            align: "center"
        },
        type: 'danger'
    });
}

notifyService.info = function (title, message) {
    $.notify({
        title: title,
        message: message
    }, {
        placement: {
            from: "top",
            align: "center"
        },
        type: 'info'
    });
}