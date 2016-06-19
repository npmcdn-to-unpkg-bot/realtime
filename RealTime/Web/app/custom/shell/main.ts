namespace App.Shell {
    class MainController extends Pages.Page {

        person: Person;
        onlinePeople: Person[];
        
        static $inject = ["allorsService", "$scope"];
        constructor(private allors: Services.AllorsService, $scope: angular.IScope) {
            super("Main", allors, $scope);

            this.$scope.$on(AllorsHub.refreshEventName, () => this.refresh());

            this.refresh();
        }

        protected refresh(): angular.IPromise<any> {
            return this.load()
                .then(() => {
                    this.person = this.objects["person"] as Person;
                    this.onlinePeople = this.collections["onlinePeople"] as Person[];
                });
        }
    }
    angular
        .module("app")
        .controller("mainController", MainController);
}
