namespace App
{
    angular.module("app",
        [   "allors",
            
            // Angular
            "ngCookies", "ngSanitize", "ngAnimate",

            // Angular UI
            "ui.router", "ui.bootstrap", "ui.select", 

            // Third Party
            "SignalR",
            "pascalprecht.translate", "tmh.dynamicLocale", "angular-loading-bar", "toastr",
            "ncy-angular-breadcrumb", "textAngular", "ngImgCrop", "blockUI" ]);
}
