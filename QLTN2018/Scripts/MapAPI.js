var map, mapOptions;
var markers = [];

function controlMap(manual) {
    initializeMap(10.7710325, 106.7081827);
}
function initializeMap(lat, lng) {
    mapOptions = {
        center: new google.maps.LatLng(lat, lng),
        zoom: 17,
        styles: [
            {
                "featureType": "administrative",
                "elementType": "all",
                "stylers": [{ "visibility": "on" }, { "saturation": -100 }, { "lightness": 20 }]
            }, {
                "featureType": "road",
                "elementType": "all",
                "stylers": [{ "visibility": "on" }, { "saturation": -100 }, { "lightness": 40 }]
            }, {
                "featureType": "water",
                "elementType": "all",
                "stylers": [{ "visibility": "on" }, { "saturation": -10 }, { "lightness": 30 }]
            }, {
                "featureType": "landscape.man_made",
                "elementType": "all",
                "stylers": [{ "visibility": "simplified" }, { "saturation": -60 }, { "lightness": 10 }]
            }, {
                "featureType": "landscape.natural",
                "elementType": "all",
                "stylers": [{ "visibility": "simplified" }, { "saturation": -60 }, { "lightness": 60 }]
            }, {
                "featureType": "poi",
                "elementType": "all",
                "stylers": [{ "visibility": "off" }, { "saturation": -100 }, { "lightness": 60 }]
            }, {
                "featureType": "transit",
                "elementType": "all",
                "stylers": [{ "visibility": "off" }, { "saturation": -100 }, { "lightness": 60 }]
            }
        ]
    };
    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
    //var marker = new google.maps.Marker({ position: new google.maps.LatLng(lat, lng) });
    //marker.setMap(map);
}


function setMapOnAll(map) {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}

// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
    setMapOnAll(null);
}

// Shows any markers currently in the array.
function showMarkers() {
    setMapOnAll(map);
}

// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
    clearMarkers();
    markers = [];
}
function clickroute(lat, lng) {
    var latLng = new google.maps.LatLng(lat, lng); //Makes a latlng
    map.panTo(latLng); //Make map global
    map.setZoom(17);
}
function infoWindow(id) {
    google.maps.event.trigger(markers[id], 'click');
}
function formatDate(date) {
    var d = new Date(parseInt(date.substr(6))),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getYear() - 100;
    hours = '' + d.getHours(),
    minutes = '' + d.getMinutes();


    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;
    if (hours.length < 2) hours = '0' + hours;
    if (minutes.length < 2) minutes = '0' + minutes;

    return [day, month, year].join('/') + " " + [hours, minutes].join(':');
}
function iconset(soTV) {

    var src;
    if (soTV > 0) {
        src = '/Images/map-marker-red.png';
    }
    else
        src = '/Images/map-marker-blue.png';
    //var icon = {
    //    path: "M0-48c-9.8 0-17.7 7.8-17.7 17.4 0 15.5 17.7 30.6 17.7 30.6s17.7-15.4 17.7-30.6c0-9.6-7.9-17.4-17.7-17.4z",
    //    fillColor: _fillColor,
    //    fillOpacity: 1,
    //    strokeColor: 'black',
    //    strokeOpacity:0.5,
    //    strokeWeight: 0.5,
    //    scale: 0.75,
    //    //anchor: new google.maps.Point(20, 0)

    //}

    return src;
}