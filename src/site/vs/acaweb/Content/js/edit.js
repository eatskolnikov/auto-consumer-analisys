$(document).ready(function () {
    var infoUrl = base_url + "Device/Add";
    var infoWindowOpen = false;
    var currentInfoWindow = null;
    var currentMarker = null;
    var markerStartPosition = null;
    var getInfowindow = function (latlng) {
        var infoWindow = new google.maps.InfoWindow({
            content: "<style> iframe{ border: none; } </style><iframe src='" + infoUrl + '?LatLng=' + latlng + "'></iframe>",
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
                    
            });
        };
    };

    $.getJSON(base_url + 'Device/Get', function (devices) {
        var l = devices.length;
        for (var i = 0; i < l; i++) {
            var device = devices[i];
            var latLng = device.LatLng.replace(' ', '').replace('(', '').replace(')', '').split(',');
            addMarker(new google.maps.LatLng(latLng[0], latLng[1]), dragendCallback(device));
        }
    });
});