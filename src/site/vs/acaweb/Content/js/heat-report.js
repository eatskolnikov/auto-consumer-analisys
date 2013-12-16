$(function () {
    var radius = 3;
    var currentHeatmaps = Array();
    var heatUrl = base_url + 'Packages/GetHeat/';
    var printHeat = function (newUrl) {
        $.ajax({
            url: newUrl == null ? heatUrl : newUrl,
            method: 'GET',
            dataType: 'json',
            success: function (packages) {
                for (var index in packages) {
                    printStain(packages[index][0].LatLng.replace('(', '').replace(')', '').replace(' ', '').split(','), packages[index].length);
                }
            }
        }).fail(function (jqXHR, textStatus) { alert("Error cargando las rutas"); });
    };

    var printStain = function(latLng, intensity) {
        var heatmapData = [];
        for (var i = 0; i < intensity; ++i) {
            heatmapData.push(new google.maps.LatLng(parseFloat(latLng[0]) + Math.random() * radius, parseFloat(latLng[1]) + Math.random() * radius));
        }
        var heatmap = new google.maps.visualization.HeatmapLayer({ data: heatmapData });
        currentHeatmaps.push(heatmap);
        heatmap.setMap(map);
    };
    printHeat();

    var reloadLoop = function () {
        reloadReport(printHeat, heatUrl);
        setTimeout(reloadLoop, 10000);
    };
    setTimeout(reloadLoop, 5000);
    $("#btnFilter").bind('click', function () {
        for (var heatMap in currentHeatmaps) { currentHeatmaps[heatMap].setMap(null); }
        reloadReport(printHeat, heatUrl);
    });
});