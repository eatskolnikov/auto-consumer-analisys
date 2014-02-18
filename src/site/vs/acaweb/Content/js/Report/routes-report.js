
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

                var lineSymbol = { path: google.maps.SymbolPath.FORWARD_CLOSED_ARROW };
                var l = packages.length;
                var routes = {};
                for (var i = 0; i < l; i++) {
                    var parsedPackage = packages[i];
                    if (typeof (routes[parsedPackage.MAC]) == 'undefined') {
                        routes[parsedPackage.MAC] = [];
                        devices[parsedPackage.MAC] = 1;
                    }
                    var latLng = parsedPackage.LatLng.replace('(', '').replace(')', '').replace(' ', '').split(',');
                    var packageLatLng = new google.maps.LatLng(latLng[0], latLng[1]);
                    routes[parsedPackage.MAC].push(packageLatLng);
                }

                for (var route in routes) {
                    var arr = [];
                    var prevIdx = -1;
                    for (var idx in routes[route]) {
                        arr.push(routes[route][idx]);
                        if (prevIdx != -1) {
                            
                            if (routes[route][prevIdx] == routes[route][idx]) continue;
                            var lineCoordinates = [
                                routes[route][prevIdx],
                                routes[route][idx]
                            ];
                            var line = new google.maps.Polyline({
                                path: lineCoordinates,
                                icons: [{
                                    icon: lineSymbol,
                                    offset: '100%'
                                }],
                                map: map
                            });
                            currentPaths.push(line);
                        }
                        prevIdx = idx;
                    }

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
    setTimeout(reloadLoop, parseInt($("#refreshingTime").val())*1000);
});