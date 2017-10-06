var app = angular.module('myApp', ['uiGmapgoogle-maps']);
app.controller('mapController', function ($scope, $http) {
 

    var markericons1 = ["http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|FE7569", "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|ff0000", "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|b30000"];
    var markericons2 = ["http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|00e6e6", "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|0080ff", "http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|0059b3"];
    var colorsred = ["#FE7569", "#ff0000", "#b30000"];
    var colorsblue = ["#00e6e6", "#0080ff", "#0059b3"];
    $scope.map = { center: { latitude: 37.8513769, longitude: 23.7565426 }, zoom: 15 }
    $scope.markers = [];
    $scope.teams = [];
    $scope.items = [];
    var colors2;
    var titles = [];
    var users = [];
    $scope.options = ["now", "10 mins ago","30 mins ago","1 hour ago","last day"];
    $http.get('/api/teams/GetAllTeams').then(function (response) {
        $scope.teams = response.data;
        console.log(response.data);
        for (i = 0; i < response.data.length; i++) {

            titles[i] = response.data[i].title.trim();
        }
    }, function () {
        alert('Error');
    });        
    
    $scope.ShowLocation = function (title,option) {       
        $scope.title = title;
            $http.get('/api/locations/GetTeamMembersLocation', {
                params: {
                    title: title
                }
            }).then(function (response) {
                 $scope.markers = [];
                //set markers colors
                for (j = 0; j < titles.length; j++) {
                    if (title.trim() == titles[j])
                    {
                        var colors = markericons2;
                         colors2 = colorsblue;                       
                    }
                    else {
                        var colors = markericons1;
                         colors2 = colorsred;                       
                    }
                }
                for (i = 0; i < response.data.length; i++) {

                    users[i] = response.data[i].username.trim();
                    $scope.markers.push({
                        title: response.data[i].t_title,
                        coords: { latitude: response.data[i].latitude, longitude: response.data[i].longitude },
                        user: response.data[i].username,
                        userid: response.data[i].UId,
                         icon: colors[i],
                        showWindow: false,
                    });                   
                }

                 $scope.map.center.latitude = response.data[0].latitude;
                 $scope.map.center.longitude = response.data[0].longitude;

            }, function () {
                alert('Error');
            });

        
            if (option == "10 mins ago") {
                var time = 10;
            }
            if (option == "30 mins ago") {
                var time = 30;
            }
            if (option == "1 hour ago") {
                var time = 60;
            }
            if (option == "last day") {
                var time = 1440;
            }
        if(option!="now"){
            $http.get('/api/location_history/GetLocationHistory', {
                params: {
                    title: title,
                    time: time
                }
            }).then(function (response) {

                for (i = 0; i < response.data.length; i++) {

                    if (response.data[i].username == users[0]) {
                        $scope.markers.push({
                            title: response.data[i].t_title,
                            coords: { latitude: response.data[i].latitude, longitude: response.data[i].longitude },
                            user: response.data[i].username,
                            icon: {
                                path: google.maps.SymbolPath.CIRCLE,
                                strokeColor: colors2[0],
                                fillColor: colors2[0],
                                scale: 3
                            },
                            showWindow: false,
                        });
                    }
                    if (response.data[i].username == users[1]) {
                        $scope.markers.push({
                            title: response.data[i].t_title,
                            coords: { latitude: response.data[i].latitude, longitude: response.data[i].longitude },
                            user: response.data[i].username,
                            icon: {
                                path: google.maps.SymbolPath.CIRCLE,
                                strokeColor: colors2[1],
                                fillColor: colors2[1],
                                scale: 3
                            },
                            showWindow: false,
                        });
                    }
                    if (response.data[i].username == users[2]) {
                        $scope.markers.push({
                            title: response.data[i].t_title,
                            coords: { latitude: response.data[i].latitude, longitude: response.data[i].longitude },
                            user: response.data[i].username,
                            icon: {
                                path: google.maps.SymbolPath.CIRCLE,
                                strokeColor: colors2[2],
                                fillColor: colors2[2],
                                scale: 3
                            },
                            showWindow: false,
                        });
                    }
          
                }


            }, function () {
                alert('Error');
            });
        }
     
        }
        $scope.GetUserExtra = function (user,markers) {
            $scope.items = [];
            _.each($scope.markers, function (mker) {
                mker.showWindow = false;
            });
            markers.showWindow = true;
            $http.get('/api/items/Getitems', {
                params: {
                    user: user
                }
            }).then(function (response) {

                for (j = 0; j < response.data.length; j++) {
                    $scope.items.push({
                        id: response.data[j].item_id,
                        kind: response.data[j].item_kind,
                    });
                }

            }, function () {
                alert('Error');
            });
            $http.get('/api/t_members/Get_member', {
                params: {
                    user: user
                }
            }).then(function (response) {
              
                    $scope.identity = response.data;
                
            }, function () {
                alert('Error');
            });
        }
            
});