namespace App.Pages.Home
{
    class HomeController extends Page {

        me: Person;
        onlinePeople: Person[];

        calls: Call[];

        requestedCalls: Call[];
        acceptedCalls: Call[];

        requestedCallsWhereCaller: Call[];
        requestedCallsWhereCallee: Call[];

        callObjectStates: CallObjectState[];
        
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
            call.CurrentObjectState = this.callObjectStates.filter(v => v.isAccepted)[0];
            this.save()
                .then(() => this.allors.application.hub.refresh(other.UserName));
        }

        protected refresh(): angular.IPromise<any> {
            return this.load()
                .then(() => {
                    this.me = this.objects["me"] as Person;
                    this.onlinePeople = this.collections["onlinePeople"] as Person[];

                    this.calls = this.collections["calls"] as Call[];

                    this.requestedCalls = this.calls.filter(v => v.CurrentObjectState.isRequested);
                    this.acceptedCalls = this.calls.filter(v => v.CurrentObjectState.isAccepted);

                    this.requestedCallsWhereCallee = this.requestedCalls.filter(v => v.isCallee(this.me));
                    this.requestedCallsWhereCaller = this.requestedCalls.filter(v => v.isCaller(this.me));

                    this.callObjectStates = this.collections["callObjectStates"] as CallObjectState[];
                });
        }
    }
    angular
        .module("app")
        .controller("homeController", HomeController);

}