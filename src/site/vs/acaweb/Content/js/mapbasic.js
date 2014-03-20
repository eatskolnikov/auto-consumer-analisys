var addMarker;
var ShowAlert = function (message, type) {
    if (type == null){ type = 'info'; }
    $("#showAlert").attr('class', 'alert alert-'+type);
    $("#showAlert").html('<button type="button" class="close" data-dismiss="alert">×</button>El dispositivo fue modificado satisfactoriamente');
};
$(document).ready(function () {
    var map_cols = 8;
    var map_rows = 4;
    var tile_dimension = 168;
    var mapCenter = new google.maps.LatLng(2, -61);
    var mallTypeOptions = {
        getTileUrl: function (coord, zoom) {
                var x = coord.x % map_cols;
                var y = coord.y % map_rows;
                if (x < 0 || y < 0) { return ""; }
                if (y < 0) y = y * -1;
                return base_url + "Content/img/map/3/tiles/" + y + "/" + x + ".png";
            },
        tileSize: new google.maps.Size(tile_dimension, tile_dimension),
        maxZoom: 3,minZoom: 3, name: 'Mall'
    };

    var mallMapType = new google.maps.ImageMapType(mallTypeOptions);
    var mapOptions = {
        center: mapCenter, zoom: 3, disableDefaultUI: true, draggable: false,
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