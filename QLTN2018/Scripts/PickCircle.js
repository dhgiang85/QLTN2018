var map, ele, mapH, mapW, addEle, mapL, mapN;

ele = 'maps_mapcanvas';
addEle = 'maps_address';
mapLat = 'DiemDen_Lat';
mapLng = 'DiemDen_Lng';
mapRadius = 'DiemDen_Radius';
mapArea = 'maps_maparea';
circleRadius = map_Radius;
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
    var zoom = 17;
    var lat = map_Lat;
    var lng = map_Lng;



    mapW = $('#' + ele).innerWidth();
    mapH = mapW * 3 / 4;

    // Init MAP
    $('#' + ele).width(mapW).height(mapH > 600 ? 600 : mapH);
    map = new google.maps.Map(document.getElementById(ele), {
        zoom: zoom,
        zoomControl: false,
        mapTypeControl: false,
        disableDefaultUI: true,
        center: new google.maps.LatLng(lat, lng),
        styles: [{ "featureType": "administrative", "elementType": "all", "stylers": [{ "visibility": "on" }, { "saturation": -100 }, { "lightness": 20 }] }, { "featureType": "road", "elementType": "all", "stylers": [{ "visibility": "on" }, { "saturation": -100 }, { "lightness": 40 }] }, { "featureType": "water", "elementType": "all", "stylers": [{ "visibility": "on" }, { "saturation": -10 }, { "lightness": 30 }] }, { "featureType": "landscape.man_made", "elementType": "all", "stylers": [{ "visibility": "simplified" }, { "saturation": -60 }, { "lightness": 10 }] }, { "featureType": "landscape.natural", "elementType": "all", "stylers": [{ "visibility": "simplified" }, { "saturation": -60 }, { "lightness": 60 }] }, { "featureType": "poi", "elementType": "all", "stylers": [{ "visibility": "off" }, { "saturation": -100 }, { "lightness": 60 }] }, { "featureType": "transit", "elementType": "all", "stylers": [{ "visibility": "off" }, { "saturation": -100 }, { "lightness": 60 }] }]
    });

    // Init default marker
    var circles = [];
    circles[0] = new google.maps.Circle({
        fillColor: '#AA0000',
        fillOpacity: 0.2,
        strokeWeight: 1,
        clickable: false,
        editable: true,
        draggable: true,
        map: map,
        radius: circleRadius,
        center: new google.maps.LatLng(lat, lng),
        zIndex: 1
    });
    markerdragEvent(circles);
    // Add marker when click on map
    google.maps.event.addListener(map, 'click', function (e) {
        for (var i = 0, circle; circle = circles[i]; i++) {
            circle.setMap(null);
        }

        circles = [];
      
        circles[0] = new google.maps.Circle({
            fillColor: '#AA0000',
            fillOpacity: 0.2,
            strokeWeight: 1,
            clickable: false,
            editable: true,
            draggable: true,
            map: map,
            radius: circleRadius,
            center: new google.maps.LatLng(e.latLng.lat(), e.latLng.lng()),
            zIndex: 1
        });
 
        markerdragEvent(circles);
    });
     //Init search box
    var input = document.getElementById(addEle);
    var searchBox = new google.maps.places.SearchBox((input));

    google.maps.event.addListener(searchBox, 'places_changed', function () {
        var places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }

        for (var i = 0, circle; circle = circles[i]; i++) {
            circle.setMap(null);
        }

        circles = [];
        var bounds = new google.maps.LatLngBounds();
        for (var i = 0, place; place = places[i]; i++) {
            var circle = new google.maps.Circle({
                fillColor: '#AA0000',
                fillOpacity: 0.2,
                strokeWeight: 1,
                clickable: false,
                editable: true,
                draggable: true,
                map: map,
                radius: circleRadius,
                center: place.geometry.location,
      
                animation: google.maps.Animation.DROP,
            });

            circles.push(circle);
            bounds.extend(place.geometry.location);


        }

        markerdragEvent(circles);
        map.fitBounds(bounds);
        map.setZoom(17);
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
function markerdragEvent(circles) {
    for (var i = 0, circle; circle = circles[i]; i++) {
        $("#" + mapLat).val(circle.getCenter().lat());
        $("#" + mapLng).val(circle.getCenter().lng());
        $("#" + mapRadius).val(circle.getRadius());
      

        google.maps.event.addListener(circle, 'center_changed', function () {
            $("#" + mapLat).val(this.getCenter().lat());
            $("#" + mapLng).val(this.getCenter().lng());             
        });
  
        google.maps.event.addListener(circle, 'radius_changed', function () {
            $("#" + mapRadius).val(this.getRadius());
            circleRadius = this.getRadius();
        });


    }
}
