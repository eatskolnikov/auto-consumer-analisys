$(function () {
    var printRoutes = function () {
        $.ajax({
            url: base_url + 'Packages/Get/',
            method: 'GET',
            dataType: 'json',
            success: function (packages) {
                var l = packages.length;
                var routes = {};
                for (var i = 0; i < l; i++) {
                    var parsedPackage = packages[i];
                    if (typeof (routes[parsedPackage.MAC]) == 'undefined') { routes[parsedPackage.MAC] = Array(); }
                    var latLng = parsedPackage.LatLng.replace('(', '').replace(')', '').replace(' ', '').split(',');
                    var packageLatLng = new google.maps.LatLng(latLng[0], latLng[1]);
                    routes[parsedPackage.MAC].push(packageLatLng);
                }
                var currentColor = 7;
                for (var route in routes) {
                    currentColor = currentColor % 15;
                    var path = new google.maps.Polyline({
                        path: routes[route], strokeColor: routeColors[currentColor],
                        strokeOpacity: 1.0, strokeWeight: 2
                    });
                    if (Object.keys(routes) == 1) {
                        for (var index in routes[route]) {
                            var marker = new google.maps.Marker({
                                position: routes[route][index],
                                map: map
                            });
                        }
                    }
                    currentColor = currentColor + 1;
                    path.setMap(map);
                }
            }
        }).fail(function (jqXHR, textStatus) { alert("Error cargando las rutas"); });
    };
    printRoutes();
});