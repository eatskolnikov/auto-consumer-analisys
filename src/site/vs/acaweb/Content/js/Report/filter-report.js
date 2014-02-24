var reloadReport = function (loadFunction, url) {
    var startDate = $("#startDate input").val();
    var endDate = $("#endDate input").val();
    loadFunction(url + '?startDate=' + encodeURIComponent(startDate) + '&endDate=' + endDate);
};
