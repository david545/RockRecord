
frontApp.controller('genreMenuController', ['$scope', 'apiFactory', function ($scope, apiFactory) {
    var genreApi = apiFactory('Genre');

    genreApi.query(function (data) {
        $scope.genres = data;
    });
}]);