var hasDblCick = false;
$(document).ready(function () {
    var infoUrl = base_url + "Device/Add";
    var editUrl = base_url + "Device/Edit";
    var delUrl = base_url + "Device/Delete";
    var infoWindowOpen = false;
    var currentInfoWindow = null;
    var currentMarker = null;
    var deleteDevice = function (deviceId, marker) {
        if (confirm("¿Está seguro que desea borrar el dispositivo? Los paquetes recibidos por este permanecerán en la base de datos")) {
            $.getJSON(delUrl + '?DeviceId=' + deviceId,
                function (response) {
                    marker.setMap(null);
                    ShowAlert(response.message);
                }
            );
        }
    };
    var getInfowindow = function (latlng) {
        var infoWindow = new google.maps.InfoWindow({
            content: "<style> iframe{ border: none; } </style><iframe src='" + infoUrl + '?LatLng=' + latlng + "'></iframe>",
            size: new google.maps.Size(700, 800),
            disableAutoPan: true
        });
        return infoWindow;
    };
    var getEditDeviceWindow = function (deviceId) {
        var infoWindow = new google.maps.InfoWindow({
            content: "<style> iframe{ border: none; } </style><iframe src='" + editUrl + '?DeviceId=' + deviceId + "'></iframe>",
            size: new google.maps.Size(700, 800),
            disableAutoPan: true
        });
        return infoWindow;
    };
    google.maps.event.addListener(map, 'dblclick', function (e) {
        var latlng = e.latLng;
        currentMarker = addMarker(latlng);
        currentInfoWindow = getInfowindow(latlng);
        currentInfoWindow.open(map, currentMarker);
        infoWindowOpen = true;
        google.maps.event.addListener(currentInfoWindow, "closeclick", function () {
            infoWindowOpen = false;
        });
    });
    google.maps.event.addListener(map, 'click', function (e) {
        if (infoWindowOpen) {
            currentInfoWindow.close();
            currentMarker.setMap(null);
            infoWindowOpen = false;
        }
    });
    var dragendCallback = function (device) {
        return function (e) {
            $.getJSON(
                base_url + 'Device/Update/?DeviceId=' + device.DeviceId.toString() + '&LatLng=' + e.latLng.toString(),
                function (data) {
                    ShowAlert('asd');
                });
        };
    };
    var clickCallback = function (device, marker) {
        google.maps.event.addListener(marker, 'click', function (e) {
            if (hasDblCick) {
                e.stop();
                hasDblCick = false;
                return;
            }
            var editWindow = getEditDeviceWindow(device.DeviceId);
            editWindow.open(map, marker);
        });
    };
    var dblClickCallback = function (device, marker) {
        google.maps.event.addListener(marker, 'dblclick', function (e) {
            hasDblCick = true;
            deleteDevice(device.DeviceId, marker);
        });
    };

    $.getJSON(base_url + 'Device/Get', function (devices) {
        var l = devices.length;
        for (var i = 0; i < l; i++) {
            var device = devices[i];
            var latLng = device.LatLng.replace(' ', '').replace('(', '').replace(')', '').split(',');
            var markerLatLng = new google.maps.LatLng(latLng[0], latLng[1]);
            var marker = addMarker(markerLatLng, dragendCallback(device));
            clickCallback(device, marker);
            dblClickCallback(device, marker);
        }
    });
});