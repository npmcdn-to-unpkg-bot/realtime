namespace App.Pages.Home
{
    class HomeController extends Page {

        me: Person;
        onlinePeople: Person[];

        static $inject = ["allorsService", "$scope"];
        constructor(private allors: Services.AllorsService,$scope: angular.IScope) {
            super("AppHome", allors, $scope);

            this.$scope.$on(AllorsHub.onRefreshEvent, () => this.refresh());

            this.refresh();
        }

        protected refresh(): angular.IPromise<any> {
            return this.load()
                .then(() => {
                    this.me = this.objects["me"] as Person;
                    this.onlinePeople = this.collections["onlinePeople"] as Person[];
                });
        }
    }
    angular
        .module("app")
        .controller("homeController", HomeController);

}