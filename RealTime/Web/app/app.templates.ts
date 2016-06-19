namespace App {

    templates.$inject = ["$templateCache"];
    function templates($templateCache: angular.ITemplateCacheService): void {
        
        Allors.Bootstrap.registerTemplates($templateCache);
    }

    angular
        .module("app")
        .run(templates);
}