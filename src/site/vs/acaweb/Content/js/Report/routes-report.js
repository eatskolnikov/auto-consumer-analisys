
var currentArrows = [];
var currentLabels = [];
var currentMarkers = {};
var paths = {};
var packagesurl = base_url + 'Packages/Get/';
var lineSymbol = { path: google.maps.SymbolPath.FORWARD_CLOSED_ARROW };

$(function() {
    var printRoutes = function(newUrl, callback) {
        $.ajax({
            url: newUrl == null ? packagesurl : newUrl, method: 'GET', dataType: 'json',
            success: function(packagesGroupedByMac) {
                for (var macIdx in packagesGroupedByMac) {
                    var lastLatLng = ''; var currLatLng;
                    var packages = packagesGroupedByMac[macIdx];
                    for (var packageIdx in packages) {
                        var pack = packages[packageIdx];
                        currLatLng = pack.LatLng;
                        if (lastLatLng != '' && lastLatLng != currLatLng) {
                            if (typeof (paths[lastLatLng]) == "undefined") { paths[lastLatLng] = { outs: 0, entries: 0 }; }
                            if (typeof (paths[currLatLng]) == "undefined") { paths[currLatLng] = { outs: 0, entries: 0 }; }
                            if (typeof (paths[lastLatLng][currLatLng]) == "undefined") { paths[lastLatLng][currLatLng] = 0; }
                            paths[lastLatLng][currLatLng]++;
                            paths[lastLatLng].outs++;
                            paths[currLatLng].entries++;
                        }
                        lastLatLng = currLatLng;
                    }
                }
                var offset = 0.0;
                for (var startPoint in paths) {
                    var start = startPoint.replace('(', '').replace(')', '').replace(' ', '').split(',');
                    for (var endPoint in paths[startPoint]) {
                        if (endPoint == 'outs' || endPoint == 'entries'){ continue; }
                        console.log(offset);
                        var startLatLng = new google.maps.LatLng(start[0] + offset, start[1] + offset);
                        var end = endPoint.replace('(', '').replace(')', '').replace(' ', '').split(',');
                        var endLatLng = new google.maps.LatLng(end[0] + offset, end[1] + offset);

                        var lineCoordinates = [startLatLng, endLatLng];
                        var labelLat = (endLatLng.lat() + startLatLng.lat()) / 2;
                        var labelLng = (endLatLng.lng() + startLatLng.lng()) / 2;
                        var direction = getDirectionArrow(startLatLng, endLatLng);
                        var label = new MapLabel({ map: map,
                            text: paths[startPoint][endPoint] + " personas " + direction,
                            position: new google.maps.LatLng(labelLat, labelLng )
                        });
                        var line = new google.maps.Polyline({
                            path: lineCoordinates,
                            icons: [{ icon: lineSymbol, offset: '100%' }],
                            map: map, strokeWeight: 2, strokeOpacity: 0.8
                        });
                        line.label = label;
                        google.maps.event.addListener(line, "mouseover", function() {
                            this.setOptions({ strokeColor: "#FF0000" });
                            this.label.set('fontColor', "#FF0000");
                        });
                        google.maps.event.addListener(line, "mouseout", function() {
                            this.setOptions({ strokeColor: "#000000" });
                            this.label.set('fontColor', "#000000");
                        });
                        if (typeof (currentMarkers[endPoint]) == "undefined") {
                            if (typeof (paths[endPoint]) == "undefined") { paths[endPoint] = { entries: 0, outs: 0 }; }
                            currentMarkers[endPoint] = {
                                marker:new google.maps.Marker({ position: endLatLng, map: map }),
                                markerInfoContent: "<p><b>Salidas: </b>" + paths[endPoint].outs + "</p><p><b>Entradas: </b>" + paths[endPoint].entries + "</p>"
                            };
                            currentMarkers[endPoint].marker.infowindow = new google.maps.InfoWindow({ content: currentMarkers[endPoint].markerInfoContent });
                            google.maps.event.addListener(currentMarkers[endPoint].marker, 'click', function () { this.infowindow.open(map, this); });
                        }
                        currentArrows.push(line);
                        currentLabels.push(label);
                    }
                }
            }
        }).fail(function(jqXHR, textStatus) { alert("Error cargando las rutas"); });
    };

    printRoutes(null);
    var cleanVariables = function() {
        for (var arrow in currentArrows) { currentArrows[arrow].setMap(null); }
        for (var label in currentLabels) { currentLabels[label].setMap(null); }
        for (var marker in currentMarkers) { currentMarkers[marker].marker.setMap(null); }
        paths = []; currentArrows = []; currentLabels = []; currentMarkers = {};
    };
    $("#btnFilter").bind('click', function() { cleanVariables(); reloadReport(printRoutes, packagesurl);
    });
    var reloadLoop = function() {
        cleanVariables(); reloadReport(printRoutes, packagesurl);
        setTimeout(reloadLoop, parseInt($("#refreshingTime").val()) * 1000);
    };
    setTimeout(reloadLoop, parseInt($("#refreshingTime").val()) * 1000);
    var getDirectionArrow = function (startLatLng, endLatLng) {
        var direction = "→";
        if(startLatLng.lng() > endLatLng.lng()) {
            if (startLatLng.lat() < endLatLng.lat()) { direction = "↖"; }
            else if (startLatLng.lat() > endLatLng.lat()) { direction = "↙"; }
            else { direction = "←"; }
        }else if (startLatLng.lng() < endLatLng.lng()) {
            if (startLatLng.lat() < endLatLng.lat()) { direction = "↗"; }
            else if (startLatLng.lat() > endLatLng.lat()) { direction = "↘"; }
        }else {
            if (startLatLng.lat() < endLatLng.lat()) { direction = "↑"; }
            else if (startLatLng.lat() > endLatLng.lat()) { direction = "↓"; }
        }
        return direction;
    };
});