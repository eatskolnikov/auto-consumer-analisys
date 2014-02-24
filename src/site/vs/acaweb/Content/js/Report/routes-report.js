
$(function () {
    var currentArrows = [];
    var currentLabels = [];
    var paths = {};
    var packagesurl = base_url + 'Packages/Get/';
    var lineSymbol = { path: google.maps.SymbolPath.FORWARD_CLOSED_ARROW };

    var printRoutes = function (newUrl, callback) {
        $.ajax({
            url: newUrl == null ? packagesurl : newUrl,
            method: 'GET',
            dataType: 'json',
            success: function (packagesGroupedByMac) {
                for (var macIdx in packagesGroupedByMac) {
                    var lastLatLng = '';
                    var currLatLng;
                    var packages = packagesGroupedByMac[macIdx];
                    for (var packageIdx in packages) {
                        var pack = packages[packageIdx];
                        currLatLng = pack.LatLng;
                        if (lastLatLng != '') {
                            if (typeof (paths[lastLatLng]) == "undefined") { paths[lastLatLng] = {}; }
                            if (typeof (paths[lastLatLng][currLatLng]) == "undefined") { paths[lastLatLng][currLatLng] = 0; }
                            paths[lastLatLng][currLatLng]++;
                        }
                        lastLatLng = currLatLng;
                    }
                }
                for (var startPoint in paths) {
                    var start = startPoint.replace('(', '').replace(')', '').replace(' ', '').split(',');
                    var startLatLng = new google.maps.LatLng(start[0], start[1]);
                    for (var endPoint in paths[startPoint]) {
                        var end = endPoint.replace('(', '').replace(')', '').replace(' ', '').split(',');
                        var endLatLng = new google.maps.LatLng(end[0], end[1]);
                        var lineCoordinates = [startLatLng, endLatLng];
                        var labelLat = (endLatLng.lat() + startLatLng.lat()) / 2;
                        var labelLng = (endLatLng.lng() + startLatLng.lng()) / 2;
                        var direction = "→"; 
                       // if (startLatLng.lat() > endLatLng.lat())
                        //    direction = "←";
                        //
                        var label = new MapLabel({
                            map: map,
                            text: paths[startPoint][endPoint] + " personas " + direction,
                            position: new google.maps.LatLng(labelLat, labelLng)
                        });
                        var line = new google.maps.Polyline({
                            path: lineCoordinates,
                            icons: [{icon: lineSymbol, offset: '100%'}],
                            map: map, strokeWeight: 2, strokeOpacity: 0.8
                        });
                        line.label = label;
                        google.maps.event.addListener(line, "mouseover", function () {
                            this.setOptions({ strokeColor: "#FF0000" });
                            this.label.set('fontColor', "#FF0000");
                        });
                        google.maps.event.addListener(line, "mouseout", function () {
                            this.setOptions({ strokeColor: "#000000" });
                            this.label.set('fontColor', "#000000");
                        });
                        currentArrows.push(line);
                        currentLabels.push(label);
                    }
                }
            }
        }).fail(function (jqXHR, textStatus) { alert("Error cargando las rutas"); });
    };
    printRoutes(null);
    var cleanVariables = function() {
        for (var arrow in currentArrows) { currentArrows[arrow].setMap(null); }
        for (var label in currentLabels) { currentLabels[label].setMap(null); }
        paths = [];
    };
    $("#btnFilter").bind('click', function () {
        cleanVariables();
        reloadReport(printRoutes, packagesurl);
    });
    var reloadLoop = function () {
        cleanVariables();
        reloadReport(printRoutes, packagesurl);
        setTimeout(reloadLoop, parseInt($("#refreshingTime").val()) * 1000);
    };
    setTimeout(reloadLoop, parseInt($("#refreshingTime").val())*1000);
});