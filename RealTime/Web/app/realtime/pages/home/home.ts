namespace App.Pages.Home
{
    class HomeController extends Page {

        me: Person;
        onlinePeople: Person[];

        calls: Call[];

        callsWhereCaller: Call[];
        callsWhereCallee: Call[];
       
        static $inject = ["allorsService", "$scope"];
        constructor(private allors: Services.AllorsService,$scope: angular.IScope) {
            super("AppHome", allors, $scope);

            this.$scope.$on(AllorsHub.onRefreshEvent, () => this.refresh());

            this.refresh();
        }

        call() {
            const other = this.onlinePeople.filter(v => v !== this.me)[0];

            const call = this.me.session.create("Call") as Call;
            call.Caller = this.me;
            call.Callee = other;

            this
                .save()
                .finally(() => {
                    this.allors.application.hub.refresh(other.UserName);
                });
        }

        accept(call: Call) {
            const other = call.other(this.me);

            this
                .invoke(call.Accept)
                .finally(() => this.allors.application.hub.refresh(other.UserName));
        }

        withdraw(call: Call) {
            const other = call.other(this.me);

            this
                .invoke(call.Withdraw)
                .finally(() => this.allors.application.hub.refresh(other.UserName));
        }

        reject(call: Call) {
            const other = call.other(this.me);

            this
                .invoke(call.Reject)
                .finally(() => this.allors.application.hub.refresh(other.UserName));
        }

        protected refresh(): angular.IPromise<any> {
            return this.load()
                .then(() => {
                    this.me = this.objects["me"] as Person;
                    this.onlinePeople = this.collections["onlinePeople"] as Person[];

                    this.calls = this.me.RequestedCalls;

                    this.callsWhereCallee = this.calls.filter(v => v.isCallee(this.me));
                    this.callsWhereCaller = this.calls.filter(v => v.isCaller(this.me));
                });
        }
    }
    angular
        .module("app")
        .controller("homeController", HomeController);

}