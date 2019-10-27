mapboxgl.accessToken = 'pk.eyJ1Ijoic2dhaTAwMDIiLCJhIjoiY2sxYXN0dm5mMGI0dTNpb2JvZjV3cDFrbiJ9.q-vke3O9OOqh6CZps2HSgg';
var park = [143.55365, -38.75448];
var map = new mapboxgl.Map({
    container: 'map',
    style: 'mapbox://styles/mapbox/streets-v11',
    center: park,
    zoom: 10
});



map.addControl(new MapboxDirections({
    accessToken: mapboxgl.accessToken
}), 'top-left');

map.addControl(new MapboxGeocoder({
    accessToken: mapboxgl.accessToken,
    mapboxgl: mapboxgl
}));



// create the marker
var marker = new mapboxgl.Marker()
    .setLngLat(park)
    .addTo(map);