angular.module('customService', ['ngResource'])
    .factory("Domain", function () {
        return {
            name: "/"
        };
    })
    .factory('apiFactory', ['$resource', 'Domain', function ($resource, Domain) {
        return function (controllerName) {
            //設定 $resource
            return $resource(
                Domain.name + 'api/:Controller/:Id',
                {
                    Controller: controllerName,
                    Id: "@Id"
                },
                {
                    update: { method: "PUT" },
                    updateArray: { method: "PUT", isArray: true }
                }
            );
        };
    }]);