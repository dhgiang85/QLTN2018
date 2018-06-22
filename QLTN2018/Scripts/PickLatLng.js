


var map, ele, mapH, mapW, addEle, mapL, mapN;

ele = 'maps_mapcanvas';
addEle = 'maps_address';
mapLat = 'TaiNan_Lat';
mapLng = 'TaiNan_Lng';
mapArea = 'maps_maparea';

// Call Google MAP API
if (!document.getElementById('googleMapAPI')) {
    var s = document.createElement('script');
    s.type = 'text/javascript';
    s.id = 'googleMapAPI';
    s.src = 'https://maps.googleapis.com/maps/api/js?key=AIzaSyDHasSo8MEV7BJbmm0axYKJ8-4IELQjf1k&libraries=places&callback=controlMap';

    document.body.appendChild(s);
} else {
    controlMap();
}

// Creat map and map tools
function initializeMap() {
    var zoom = 13;
    var lat = map_Lat;
    var lng = map_Lng;



    mapW = $('#' + ele).innerWidth();
    mapH = mapW * 3 / 4;

    // Init MAP
    $('#' + ele).width(mapW).height(mapH > 500 ? 500 : mapH);
    map = new google.maps.Map(document.getElementById(ele), {
        zoom: zoom,
        zoomControl: false,
        mapTypeControl: false,
        disableDefaultUI: true,
        center: new google.maps.LatLng(lat, lng),
        styles: [{ "featureType": "administrative", "elementType": "all", "stylers": [{ "visibility": "on" }, { "saturation": -100 }, { "lightness": 20 }] }, { "featureType": "road", "elementType": "all", "stylers": [{ "visibility": "on" }, { "saturation": -100 }, { "lightness": 40 }] }, { "featureType": "water", "elementType": "all", "stylers": [{ "visibility": "on" }, { "saturation": -10 }, { "lightness": 30 }] }, { "featureType": "landscape.man_made", "elementType": "all", "stylers": [{ "visibility": "simplified" }, { "saturation": -60 }, { "lightness": 10 }] }, { "featureType": "landscape.natural", "elementType": "all", "stylers": [{ "visibility": "simplified" }, { "saturation": -60 }, { "lightness": 60 }] }, { "featureType": "poi", "elementType": "all", "stylers": [{ "visibility": "off" }, { "saturation": -100 }, { "lightness": 60 }] }, { "featureType": "transit", "elementType": "all", "stylers": [{ "visibility": "off" }, { "saturation": -100 }, { "lightness": 60 }] }]
    });

    // Init default marker
    var markers = [];
    markers[0] = new google.maps.Marker({
        map: map,
        position: new google.maps.LatLng(lat, lng),
        draggable: true,
        animation: google.maps.Animation.DROP
    });
    markerdragEvent(markers);

    // Add marker when click on map
    google.maps.event.addListener(map, 'click', function (e) {
        for (var i = 0, marker; marker = markers[i]; i++) {
            marker.setMap(null);
        }

        markers = [];
        markers[0] = new google.maps.Marker({
            map: map,
            position: new google.maps.LatLng(e.latLng.lat(), e.latLng.lng()),
            draggable: true,
            animation: google.maps.Animation.DROP
        });

        markerdragEvent(markers);
    });
    // Init search box
    var input = document.getElementById(addEle);
    var searchBox = new google.maps.places.SearchBox((input));

    google.maps.event.addListener(searchBox, 'places_changed', function () {
        var places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }

        for (var i = 0, marker; marker = markers[i]; i++) {
            marker.setMap(null);
        }

        markers = [];
        var bounds = new google.maps.LatLngBounds();
        for (var i = 0, place; place = places[i]; i++) {
            var marker = new google.maps.Marker({
                map: map,
                position: place.geometry.location,
                draggable: true,
                animation: google.maps.Animation.DROP,
            });

            markers.push(marker);
            bounds.extend(place.geometry.location);


        }

        markerdragEvent(markers);
        map.fitBounds(bounds);
        map.setZoom(16);
        console.log(places);
    });



}

// Show, hide map on select change
function controlMap(manual) {
    $('#' + mapArea).slideDown(100, function () {
        initializeMap();
    });

    return !1;
}

// Map Marker drag event
function markerdragEvent(markers) {
    for (var i = 0, marker; marker = markers[i]; i++) {
        $("#" + mapLat).val(marker.position.lat());
        $("#" + mapLng).val(marker.position.lng());

        google.maps.event.addListener(marker, 'drag', function (e) {
            $("#" + mapLat).val(e.latLng.lat());
            $("#" + mapLng).val(e.latLng.lng());
        });
    }
}
