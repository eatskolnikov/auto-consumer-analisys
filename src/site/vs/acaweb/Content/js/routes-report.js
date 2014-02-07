
$(function () {
    var currentPaths = Array();
    var currentMarkers = Array();
    var devices = {};
    var packagesurl = base_url + 'Packages/Get/';
    var printRoutes = function (newUrl, callback) {
        $.ajax({
            url: newUrl == null ? packagesurl : newUrl,
            method: 'GET',
            dataType: 'json',
            success: function (packages) {
                var l = packages.length;
                var routes = {};
                for (var i = 0; i < l; i++) {
                    var parsedPackage = packages[i];
                    if (typeof (routes[parsedPackage.MAC]) == 'undefined') {
                        routes[parsedPackage.MAC] = Array();
                        devices[parsedPackage.MAC] = 1;
                    }
                    var latLng = parsedPackage.LatLng.replace('(', '').replace(')', '').replace(' ', '').split(',');
                    var packageLatLng = new google.maps.LatLng(latLng[0], latLng[1]);
                    routes[parsedPackage.MAC].push(packageLatLng);
                }

                var currentColor = 7;
                for (var route in routes) {
                    //currentColor = currentColor % 15;
                    var p = new google.maps.Polyline({
                        path: routes[route],
                        strokeColor: '#FF0000',
                        strokeOpacity: 1.0,
                        strokeWeight: 3
                    });
                    if (Object.keys(routes).length == 1) {
                        for (var index in routes[route]) {
                            var marker = new google.maps.Marker({
                                position: routes[route][index],
                                title: (parseInt(index) + 1).toString(),
                                map: map
                            });
                            currentMarkers.push(marker);
                        }
                    }
                    //currentColor = currentColor + 1;
                    p.setMap(map);
                    currentPaths.push(p);
                }
                if (callback != null) { callback(devices); }
            }
        }).fail(function (jqXHR, textStatus) { alert("Error cargando las rutas"); });
    };
    printRoutes(null,fillMacComboBox);
    $("#btnFilter").bind('click', function () {
        for (var marker in currentMarkers) { currentMarkers[marker].setMap(null); }
        for (var path in currentPaths) { currentPaths[path].setMap(null); }
        reloadReport(printRoutes, packagesurl);
    });
    var reloadLoop = function () {
        for (var marker in currentMarkers) { currentMarkers[marker].setMap(null); }
        for (var path in currentPaths) { currentPaths[path].setMap(null); }
        reloadReport(printRoutes, packagesurl);
        setTimeout(reloadLoop, parseInt($("#refreshingTime").val()) * 1000);
    };
    //setTimeout(reloadLoop, parseInt($("#refreshingTime").val())*1000);
});