﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="v3.aspx.cs" Inherits="GoogleMapDemo.v3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="http://code.google.com/apis/maps/documentation/javascript/examples/default.css" rel="stylesheet" type="text/css" /> 
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script> 
    <script type="text/javascript">
        function initialize() {
            var myOptions = {
                zoom: 10,
                center: new google.maps.LatLng(-33.9, 151.2),
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            var map = new google.maps.Map(document.getElementById("map_canvas"),
                                myOptions);

            setMarkers(map, beaches);
        }

        /**
        * Data for the markers consisting of a name, a LatLng and a zIndex for
        * the order in which these markers should display on top of each
        * other.
        */
        var beaches = [
  ['Bondi Beach', -33.890542, 151.274856, 4],
  ['Coogee Beach', -33.923036, 151.259052, 5],
  ['Cronulla Beach', -34.028249, 151.157507, 3],
  ['Manly Beach', -33.80010128657071, 151.28747820854187, 2],
  ['Maroubra Beach', -33.950198, 151.259302, 1]
];

        function setMarkers(map, locations) {
            // Add markers to the map

            // Marker sizes are expressed as a Size of X,Y
            // where the origin of the image (0,0) is located
            // in the top left of the image.

            // Origins, anchor positions and coordinates of the marker
            // increase in the X direction to the right and in
            // the Y direction down.
            var image = new google.maps.MarkerImage('gif007.gif',
            // This marker is 20 pixels wide by 32 pixels tall.
      new google.maps.Size(20, 32),
            // The origin for this image is 0,0.
      new google.maps.Point(0, 0),
            // The anchor for this image is the base of the flagpole at 0,32.
      new google.maps.Point(0, 32));
            var shadow = new google.maps.MarkerImage('images/beachflag_shadow.png',
            // The shadow image is larger in the horizontal dimension
            // while the position and offset are the same as for the main image.
      new google.maps.Size(37, 32),
      new google.maps.Point(0, 0),
      new google.maps.Point(0, 32));
            // Shapes define the clickable region of the icon.
            // The type defines an HTML <area> element 'poly' which
            // traces out a polygon as a series of X,Y points. The final
            // coordinate closes the poly by connecting to the first
            // coordinate.
            var shape = {
                coord: [1, 1, 1, 20, 18, 20, 18, 1],
                type: 'poly'
            };
            for (var i = 0; i < locations.length; i++) {
                var beach = locations[i];
                var myLatLng = new google.maps.LatLng(beach[1], beach[2]);
                var marker = new google.maps.Marker({
                    position: myLatLng,
                    map: map,
                    shadow: shadow,
                    icon: image,
                    shape: shape,
                    title: beach[0],
                    zIndex: beach[3]
                });
            }
        }
</script> 
</head>
<body onload="initialize();">
    <form id="form" runat="server">
    <div> 
    <input onclick="clearOverlays();" type=button value="Clear Overlays"/> 
    <input onclick="showOverlays();" type=button value="Show All Overlays"/> 
    <input onclick="deleteOverlays();" type=button value="Delete Overlays"/> 
  </div> 
  <div id="map_canvas" style="top:30px;width:600px; height:500px"></div> 
    </form>
</body>
</html>