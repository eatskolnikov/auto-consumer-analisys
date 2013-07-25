
var fillMacComboBox = function (macs) {
    for (var mac in macs) {
        $("#MAC").append($('<option>', { value: mac }).text(mac)); 
    }
};
var reloadReport = function (loadFunction, url) {
    var startDate = parseDateField("#startDate");
    var endDate = parseDateField("#endDate");
    loadFunction(url + '?MAC=' + encodeURIComponent($("#MAC").val()) + '&startDate=' + encodeURIComponent(startDate) + '&endDate=' + endDate);
};

var parseDateField = function (fieldId) {
    if (!isDate($(fieldId).val())) {
        if ($(fieldId).val() != "") $(fieldId+'Error').text("La fecha introducida no es valida");
        return "";
    }
    var dateParts = $(fieldId).val().split('/');
    var parsedDate = dateParts[2] + dateParts[1] + dateParts[0];
    return parsedDate;
};

$(function () {
    $("#startDate").datepicker({ dateFormat: 'dd/mm/yy' });
    $("#endDate").datepicker({ dateFormat: 'dd/mm/yy' });
});

//credit to: http://jquerybyexample.blogspot.com/2011/12/validate-date-using-jquery.html
function isDate(txtDate) {
    var currVal = txtDate;
    if (currVal == '')return false;
    //Declare Regex  
    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var dtArray = currVal.match(rxDatePattern); // is format OK?

    if (dtArray == null)return false;
    //Checks for dd/mm/yyyy format.
    var dtMonth = dtArray[3];
    var dtDay = dtArray[1];
    var dtYear = dtArray[5];

    if (dtMonth < 1 || dtMonth > 12)
        return false;
    else if (dtDay < 1 || dtDay > 31)
        return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return false;
    }
    return true;
}