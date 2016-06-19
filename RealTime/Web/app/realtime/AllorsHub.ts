namespace App {

    export class AllorsHub {

        static refreshEventName = "AllorsHub.Refresh";

        private hub: ngSignalr.Hub;

        constructor(private $rootScope: angular.IRootScopeService, Hub: ngSignalr.HubFactory) {

            this.hub = new Hub("allors",
                {
                    listeners: {
                        'clientRefresh': () => this.clientRefresh()
                    },
                    methods: ["serverRefresh"],
                    errorHandler: this.errorHandler
                });
        }

        clientRefresh() {
            this.$rootScope.$broadcast(AllorsHub.refreshEventName);
        }

        serverRefresh() {
            this.hub.invoke("serverRefresh");
        }
        
        private errorHandler(error: string) {
            throw error;
        }
    }
}