var showMessage = function (message) {
    if (typeof (message) == 'undefined') { return; }
    else {
        $('#modalDialog .modal-body')[0].innerHTML = message;
        $('#modalDialog').modal('show');
    }
};

var showHelp = function () {
    $('#modalDialog').modal({ show: true, remote: 'http://ec2-23-20-227-175.compute-1.amazonaws.com/Help.aspx' });
};
