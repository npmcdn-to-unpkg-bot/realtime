namespace App
{
    var app = angular.module("app");
    
    app.config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];
    function config($stateProvider: angular.ui.IStateProvider, $urlRouterProvider: angular.ui.IUrlRouterProvider): void {
        
        $urlRouterProvider.otherwise(($injector, $location) => {
            return "/";
        });

        $stateProvider
            .state("home", {
                url: "/",
                templateUrl: "/app/realtime/pages/home/home.html",
                controller: "homeController",
                controllerAs: "vm"
            });
    }
}
