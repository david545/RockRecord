angular.module('ngui.pager', [])
    .directive('pager', [function () {
        return {
            restrict: 'EA',
            template: '<ul class="ngui-pager">\n' +
                          '<li><a href="" ng-click="changePage(currentPage - 1)">&laquo;</a></li>\n' +
                          '<li ng-repeat="page in pages" ng-class="{ active: currentPage==page }"><a href="" ng-click="changePage(page)">{{page}}</a></li>\n' +
                          '<li><a href="" ng-click="changePage(currentPage + 1)">&raquo;</a></li>\n' +
                      '</ul>',
            scope: {
                totalItems: '=',
                onChangePage: ' &'
            },
            link: function (scope, element, attr) {

                scope.$watch('totalItems', function () {
                    makePages(1);
                });

                scope.changePage = function (page) {
                    makePages(page);
                    scope.onChangePage({ page: page });
                };


                function makePages(page) {

                    var pageLength = attr.pageLength ? attr.pageLength : 5;
                    var perPageItems = attr.perPageItems ? attr.perPageItems : 10;
                    var middleLength = Math.floor(pageLength / 2);
                    var allPages = Math.ceil(scope.totalItems / perPageItems);

                    var start;
                    if (page - middleLength < 1) {
                        start = 1;
                    } else if (page + middleLength > allPages) {
                        start = allPages - pageLength + 1;
                    }
                    else {
                        start = page - middleLength;
                    }

                    if (page > 0 && page <= allPages) {
                        scope.currentPage = page;
                    }

                    scope.pages = [];
                    var count = 1;
                    while (count <= pageLength && start <= allPages) {
                        scope.pages.push(start);
                        count++;
                        start++;
                    }
                }
            }
        };
    }]);