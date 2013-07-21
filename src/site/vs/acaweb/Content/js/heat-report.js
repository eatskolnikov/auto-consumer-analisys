$(function () {
    var radius = 2;
    var printHeat = function () {
        $.ajax({
            url: base_url + 'Packages/GetHeat/',
            method: 'GET',
            dataType: 'json',
            success: function (packages) {
                for (var index in packages) {
                    printStain(packages[index][0].LatLng.replace('(', '').replace(')', '').replace(' ', '').split(','), packages[index].length);
                }
            }
        }).fail(function (jqXHR, textStatus) { alert("Error cargando las rutas"); });
    };

    var printStain = function (latLng, intensity) {
        var heatmapData = [];
        for (var i = 0; i < intensity; ++i) {
            heatmapData.push(new google.maps.LatLng(parseFloat(latLng[0]) + Math.random() * radius, parseFloat(latLng[1]) + Math.random() * radius));
        }
        var heatmap = new google.maps.visualization.HeatmapLayer({ data: heatmapData });
        heatmap.setMap(map);
    };
    printHeat();
});