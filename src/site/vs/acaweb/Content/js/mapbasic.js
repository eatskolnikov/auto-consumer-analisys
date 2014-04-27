var addMarker;
var ShowAlert = function (message, type) {
    if (type == null){ type = 'info'; }
    $("#showAlert").attr('class', 'alert alert-'+type);
    $("#showAlert").html('<button type="button" class="close" data-dismiss="alert">×</button>El dispositivo fue modificado satisfactoriamente');
};
var changeFloor = function(floor) {
    var map_cols = 8;
    var map_rows = 4;
    current_floor = floor;
    var tile_dimension = 256;
    var mapCenter = new google.maps.LatLng(46.241271092100405, 0.22968749999995097);

    var mallTypeOptions = {
        getTileUrl: function (coord, zoom) {
            var x = coord.x % map_cols;
            var y = coord.y % map_rows;
            if (x < 0 || y < 0) { return ""; }
            if (y < 0) y = y * -1;
            return tiles_url + current_floor + '/' + y + "/" + x + ".png";
        },
        tileSize: new google.maps.Size(tile_dimension, tile_dimension),
        maxZoom: 2, minZoom: 2, name: 'Mall'
    };

    var mallMapType = new google.maps.ImageMapType(mallTypeOptions);
    var mapOptions = {
        center: mapCenter, zoom: 2, disableDefaultUI: true, draggable: false,
        mapTypeControlOptions: {
            mapTypeIds: ['mall']
        }
    };
    map.setOptions(mapOptions);
    map.mapTypes.set('mall', mallMapType);
    map.setMapTypeId('mall');
};

$(document).ready(function () {
    var map_cols = 8;
    var map_rows = 4;
    var tile_dimension = 256;
    var mapCenter = new google.maps.LatLng(46.241271092100405, 0.22968749999995097);

    var mallTypeOptions = {
        getTileUrl: function (coord, zoom) {
                var x = coord.x % map_cols;
                var y = coord.y % map_rows;
                if (x < 0 || y < 0) { return ""; }
                if (y < 0) y = y * -1;
                return tiles_url + current_floor+'/' + y + "/" + x+ ".png";
            },
        tileSize: new google.maps.Size(tile_dimension, tile_dimension),
        maxZoom: 2,minZoom: 2, name: 'Mall'
    };

    var mallMapType = new google.maps.ImageMapType(mallTypeOptions);
    var mapOptions = {
        center: mapCenter, zoom: 2, disableDefaultUI: true, draggable: false,
        mapTypeControlOptions: {
            mapTypeIds: ['mall']
        }
    };
    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
    map.mapTypes.set('mall', mallMapType);
    map.setMapTypeId('mall');


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
    $('.datetimepickercontroller').datetimepicker();
});