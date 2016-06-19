namespace App.Pages.Home
{
    class HomeController extends Page {

        me: Person;

        static $inject = ["allorsService", "$scope"];
        constructor(
            private allors: Services.AllorsService,
            $scope: angular.IScope) {

            super("AppHome", allors, $scope);

            this.$scope.$on(AllorsHub.refreshEventName, () => this.clientRefresh());

            this.refresh();
        }

        clientRefresh() {
            this.allors.$log.info("Client Refresh");
        }

        serverRefresh() {
            this.allors.application.hub.serverRefresh();;
        }

        protected refresh(): angular.IPromise<any> {
            return this.load()
                .then(() => {
                    this.me = this.objects["me"] as Person;
                });
        }
    }
    angular
        .module("app")
        .controller("homeController", HomeController);

}