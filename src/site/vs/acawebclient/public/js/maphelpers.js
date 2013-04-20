var showMessage = function (message) {
    if (typeof (message) == 'undefined') { return; }
    else {
        console.log(
            $('#modalDialog .modal-body'));
        $('#modalDialog .modal-body')[0].innerHTML = message;
        $('#modalDialog').modal('show');
    }
};
