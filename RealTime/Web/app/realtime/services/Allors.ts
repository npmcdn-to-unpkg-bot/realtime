namespace App.Services {
    declare var AdapterJS: any;

    export class AllorsService {
        database: Allors.Database;
        workspace: Allors.Workspace;
        adapter = AdapterJS;

        application: Application;

        static $inject = ["$log", "$http", "$q", "$rootScope", "$window", "$state", "$interval", "$translate", "$sce", "toastr", "Hub"];
        constructor(
            public $log: angular.ILogService,
            public $http: angular.IHttpService,
            public $q: angular.IQService,
            public $rootScope: angular.IRootScopeService,
            public $window: angular.IWindowService,
            public $interval: angular.IIntervalService,
            public $state: angular.ui.IStateService,
            public $translate: angular.translate.ITranslateService,
            public $sce: angular.ISCEService,
            public toastr: angular.toastr.IToastrService,
            Hub: ngSignalr.HubFactory) {

            const prefix = "/Database/";
            const postfix = "/Pull";
            this.database = new Allors.Database(this.$http, this.$q, prefix, postfix);
            this.workspace = new Allors.Workspace(Allors.Data.metaPopulation);

            AdapterJS.webRTCReady( isUsingPlugin => {
                this.application = new Application(this, Hub);
            });
        }
    }
    angular.module("app")
        .service("allorsService",
        AllorsService);
}