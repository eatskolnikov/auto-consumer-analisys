$(function () {
    var map_cols = 8;
    var map_rows = 4;
    var tile_dimension = 168;
    var maptiler = new google.maps.ImageMapType({
        getTileUrl:
			function (coord, zoom) {
			    var x = coord.x % map_cols;
			    var y = coord.y % map_rows;
			    if (x < 0 || y < 0) { return ""; }
			    if (y < 0) y = y * -1;
			    return base_url + "public/img/map/tiles/" + y + "/" + x + ".png";
			},
        tileSize: new google.maps.Size(tile_dimension, tile_dimension),
        isPng: true
    });
    var mapOptions = { center: new google.maps.LatLng(45, 45), zoom: 2, maxZoom: 2, minZoom: 2, disableDefaultUI: true };
    var strictBounds = new google.maps.LatLngBounds( new google.maps.LatLng(0, 0), new google.maps.LatLng(0, 0));

    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
    map.overlayMapTypes.insertAt(0, maptiler);

    google.maps.event.addListener(map, 'drag', function (e) {
        if (strictBounds.contains(map.getCenter())) return;
        var c = map.getCenter(), x = c.lng(), y = c.lat(), 
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
    var createDeviceMarker = function (device) {
        var latLng = device.LatLng.split(',');
        var markerStartPosition = 0;
        var marker = new google.maps.Marker({
            position: new google.maps.LatLng(latLng[0], latLng[1]),
            draggable: true,
            title: device.Description
        });

        var infoUrl = base_url + 'ajax_service/forms/DeviceForm.aspx?DeviceId=' + device.DeviceId;

        var infowindow = new google.maps.InfoWindow({
            content: "<style> iframe{ border: none; } </style><iframe src='" + infoUrl + "'></iframe>",
            size: new google.maps.Size(400, 600)
        });
        google.maps.event.addListener(marker, 'dblclick', function (e) {
            infowindow.open(map, marker);
        });

        google.maps.event.addListener(marker, 'drag', function (e) {
            markerStartPosition = e.latLng.toString();
            markerStartPosition = markerStartPosition.substr(1, markerStartPosition.length - 2);
            markerStartPosition = markerStartPosition.replace(' ', '');
        });
        google.maps.event.addListener(marker, 'dragend', function (e) {
            $.ajax({
                url: base_url + 'ajax_service/Devices.aspx',
                method: 'POST',
                data: 'DeviceId=' + device.DeviceId + '&LatLng=' + markerStartPosition,
                success: function (response) {
                    if (response == null || response.success == false)
                        showMessage('Error actualizando la posicion del dispositivo');
                }
            });
        });
        marker.setMap(map);
        markers.push(marker);
        return marker;
    };

    google.maps.event.addListener(map, 'click', function (e) {
        var coord = e.latLng.toString();
        coord = coord.substr(1, coord.length - 2);
        $.ajax({
            url: base_url + 'ajax_service/Devices.aspx',
            method: 'POST',
            data: 'DeviceId=0&LatLng=' + coord + '&Description=New marker',
            success: function (response) {
                var r = JSON.parse(response);
                if (response == null || response.success == false)
                    showMessage('Error insertando dispositivo');
                createDeviceMarker({ LatLng: coord, Description: 'Nuevo dispositivo', Ip: "0.0.0.0", DeviceId: r.messages[1] });
            }
        });
    });
    var get_markers = function () {
        $.ajax({
            url: base_url + 'ajax_service/Devices.aspx',
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                if (data != null && typeof (data.success) != 'undefined' && data.success == true) {
                    var devices = JSON.parse(data.objectData).Devices;
                    var l = devices.length;
                    for (var i = 0; i < l; i++) { createDeviceMarker(devices[i]); }
                } else {
                    showMessage("No se encontraron dispositivos ):");
                }
            }
        }).fail(function (jqXHR, textStatus) {
            showMessage("Request failed: " + textStatus);
        });
    };
    get_markers();
});