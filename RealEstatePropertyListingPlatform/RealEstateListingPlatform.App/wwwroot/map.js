var map;

function initialize() {
    var letlng = new google.maps.LatLng(40.7128, -74.0060);
    var options = {
        zoom: 8,
        center: letlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("map"), options);
}