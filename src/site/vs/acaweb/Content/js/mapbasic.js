var addMarker;
$(document).ready(function () {
    var map_cols = 8;
    var map_rows = 4;
    var tile_dimension = 168;
    var mapCenter = new google.maps.LatLng(2, -61);
    var maptiler = new google.maps.ImageMapType({
        getTileUrl:
            function (coord) {
                var x = coord.x % map_cols;
                var y = coord.y % map_rows;
                if (x < 0 || y < 0) {
                    return "";
                }
                if (y < 0) y = y * -1;
                return base_url + "Content/img/map/tiles/" + y + "/" + x + ".png";
            },
        tileSize: new google.maps.Size(tile_dimension, tile_dimension),
        isPng: true
    });
    var mapOptions = { center: mapCenter, zoom: 3, maxZoom: 3, minZoom: 3, disableDefaultUI: true };
    var strictBounds = new google.maps.LatLngBounds(new google.maps.LatLng(0, 0), new google.maps.LatLng(0, 0));

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
        map.setCenter(mapCenter);
    });
    addMarker = function (position, dragEndCallback) {
        var marker = new google.maps.Marker({
            position: position,
            map: map,
            draggable: true
        });
        if (dragEndCallback != null) {
            google.maps.event.addListener(marker, 'dragend', dragEndCallback);
        }
        return marker;
    };
});