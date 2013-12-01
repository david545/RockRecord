
frontApp.controller('albumListController', ['$scope', 'apiFactory','$routeParams' , function ($scope, apiFactory, $routeParams) {
    var albumApi = apiFactory('Album');
    $scope.model = {};

    $scope.getData = function (page) {
        $scope.isScrolling = false;
        albumApi.get({ page: page, size: 10, genreId: $routeParams.genreId }, function (data) {
            $scope.model = data;
            $('body').animate({scrollTop: 0});
        });
    };

    $scope.getData(1);

}]);