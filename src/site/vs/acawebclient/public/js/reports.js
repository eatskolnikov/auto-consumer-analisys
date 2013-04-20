$(function () {
    var map_cols = 8;
    var map_rows = 4;
    var tile_dimension = 168;
    var maptiler = new google.maps.ImageMapType({
        getTileUrl:
			function (coord, zoom) {
			    var x = coord.x % map_cols;
			    var y = coord.y % map_rows;
			    if (x < 0 || y < 0) {
			        return "";
			    }
			    if (y < 0) y = y * -1;
			    return base_url + "public/img/map/tiles/" + y + "/" + x + ".png";
			},
        tileSize: new google.maps.Size(tile_dimension, tile_dimension),
        isPng: true
    });
    var mapOptions = {
        center: new google.maps.LatLng(45, 45),
        zoom: 2,
        maxZoom: 2,
        minZoom: 2,
        disableDefaultUI: true
    };
    var strictBounds = new google.maps.LatLngBounds(
        new google.maps.LatLng(0, 0),
        new google.maps.LatLng(0, 0)
    );

    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
    map.overlayMapTypes.insertAt(0, maptiler);

    google.maps.event.addListener(map, 'drag', function (e) {
        if (strictBounds.contains(map.getCenter())) return;
        var c = map.getCenter(),
            x = c.lng(),
            y = c.lat(),
            maxX = strictBounds.getNorthEast().lng(),
            maxY = strictBounds.getNorthEast().lat(),
            minX = strictBounds.getSouthWest().lng(),
            minY = strictBounds.getSouthWest().lat();
        if (x < minX) x = minX;
        if (x > maxX) x = maxX;
        if (y < minY) y = minY;
        if (y > maxY) y = maxY;
        map.setCenter(new google.maps.LatLng(45, 45));
    });
    var markers = [];
    var get_markers = function () {
        $.ajax({
            url: base_url + 'ajax_service/reports/GetParsedPackages.aspx?StartDate=20121200',
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                var macs = {};
                if (data != null && typeof (data.success) != 'undefined' && data.success == true) {
                    var packages = JSON.parse(data.objectData).ParsedPackages;
                    var coordinates = [];
                    var l = packages.length;
                    for (var i = 0; i < l; i++) {
                        macs[packages[i].MAC] = true;
                        var LatLng = packages[i].LatLng.split(',');
                        markers.push(new google.maps.LatLng(LatLng[0], LatLng[1]));
                    }

                    for (var mac in macs) {
                        $("#devices").append("<option>" + mac + "</option>");
                    }
                    $("#devices").bind('change', function () {
                        var url = base_url + 'ajax_service/reports/GetParsedPackages.aspx?StartDate=' + $("#startdate").val() + '&EndDate=' + $("#enddate").val();
                        if ($("#devices").val() != '0') {
                            url = url + '&MAC=' + encodeURIComponent($("#devices").val());
                        }
                        alert(url);
                        $.ajax({
                            url: url,
                            method: 'GET',
                            dataType: 'json',
                            success: function (data) {
                                heatmap.setMap(null);
                                markers = [];
                                if (data != null && typeof (data.success) != 'undefined' && data.success == true) {
                                    var packages = JSON.parse(data.objectData).ParsedPackages;
                                    var l = packages.length;
                                    for (var i = 0; i < l; i++) {
                                        var LatLng = packages[i].LatLng.split(',');
                                        markers.push(new google.maps.LatLng(LatLng[0], LatLng[1]));
                                    }
                                    heatmap = new google.maps.visualization.HeatmapLayer({ data: markers });
                                    heatmap.setMap(map);
                                }
                            }
                        });
                    });
                    var heatmap = new google.maps.visualization.HeatmapLayer({
                        data: markers
                    });
                    heatmap.setMap(map);
                    var polyline = new google.maps.Polyline({
                        path: markers,
                        map:map
                    });
                } else {
                    console.log('No devices retrieved ):');
                    console.log(data);
                }
            }
        }).fail(function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        });
    };
    get_markers();
});