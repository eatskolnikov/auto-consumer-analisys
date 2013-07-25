var addMarker;
var ShowAlert = function (message, type) {
    if (type == null){ type = 'info'; }
    $("#showAlert").attr('class', 'alert alert-'+type);
    $("#showAlert").html('<button type="button" class="close" data-dismiss="alert">×</button>El dispositivo fue modificado satisfactoriamente');
};
var routeColors = ['#C0C0C0', '#808080', '#000000', '#FF0000', '#800000', '#FFFF00', '#808000', '#00FF00', '#008000', '#00FFFF', '#008080', '#0000FF', '#000080', '#FF00FF', '#800080'];
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
                if (x < 0 || y < 0) { return ""; }
                if (y < 0) y = y * -1;
                return base_url + "Content/img/map/tiles/" + y + "/" + x + ".png";
            },
        tileSize: new google.maps.Size(tile_dimension, tile_dimension),
        isPng: true
    });
    var mapOptions = { center: mapCenter, zoom: 3, maxZoom: 3, minZoom: 3, disableDefaultUI: true, draggable:false };
    var strictBounds = new google.maps.LatLngBounds(new google.maps.LatLng(0, 0), new google.maps.LatLng(0, 0));
    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
    var defaultBounds = map.getBounds();
    var defaultCenter = map.getCenter();
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

    var markerDragCallback = function (marker) {
        return function (e) {
            /*defaultBounds = map.getBounds();
            console.log(defaultBounds);
            if (!defaultBounds.contains(e.LatLng)) {
                console.log('out of bounds');
                //marker.LatLng = defaultCenter;
            }*/
        };
    };

    addMarker = function (position, dragEndCallback) {
        var marker = new google.maps.Marker({
            position: position,
            map: map,
            draggable: true
        });
        //google.maps.event.addListener(marker, 'drag', markerDragCallback(marker));
        if (dragEndCallback != null) {
            google.maps.event.addListener(marker, 'dragend', dragEndCallback);
        }
        return marker;
    };
});