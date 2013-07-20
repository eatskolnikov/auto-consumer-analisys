$(function () {
    var printRoutes = function () {
        $.ajax({
            url: base_url + 'Packages/Get/',
            method: 'GET',
            dataType: 'json',
            success: function (packages) {
                console.log(packages);
                var l = packages.length;
                console.log(l);
                var routes = {};
                for (var i = 0; i < l; i++) {
                    var parsedPackage = packages[i];
                    if (typeof (routes[parsedPackage.MAC]) == 'undefined') {
                        routes[parsedPackage.MAC] = Array();
                    }
                    var latLng = parsedPackage.LatLng.replace('(', '').replace(')', '').replace(' ', '').split(',');
                    var packageLatLng = new google.maps.LatLng(latLng[0], latLng[1]);
                    routes[parsedPackage.MAC].push(packageLatLng);
                }
                for (var route in routes) {
                    var path = new google.maps.Polyline({
                        path: routes[route],
                        strokeColor: '#FF0000',
                        strokeOpacity: 1.0,
                        strokeWeight: 2
                    });
                    path.setMap(map);
                }
            }
        }).fail(function (jqXHR, textStatus) {
            alert("Error cargando las rutas");
        });
    };
    printRoutes();
});