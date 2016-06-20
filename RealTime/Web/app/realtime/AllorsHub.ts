namespace App {

    export class AllorsHub {

        static onRefreshEvent = "AllorsHub.OnRefresh";
        static onOfferEvent = "AllorsHub.OnOffer";
        static onCandidateEvent = "AllorsHub.OnCandidate";
        static onAnswerEvent = "AllorsHub.OnAnswer";

        private hub: ngSignalr.Hub;

        constructor(private $rootScope: angular.IRootScopeService, Hub: ngSignalr.HubFactory) {

            this.hub = new Hub("allors",
                {
                    listeners: {
                        'onRefresh': () => {
                            this.$rootScope.$broadcast(AllorsHub.onRefreshEvent);
                        },
                        'onOffer': (offer) => {
                            this.$rootScope.$broadcast(AllorsHub.onOfferEvent, offer);
                        },
                        'onCandidate': (candidate) => {
                            this.$rootScope.$broadcast(AllorsHub.onCandidateEvent, candidate);
                        },
                        'onAnswer': (answer) => {
                            this.$rootScope.$broadcast(AllorsHub.onAnswerEvent, answer);
                        }

                    },
                    methods: ["refresh", "candidate", "offer", "answer"],
                    errorHandler: this.errorHandler
                });
        }

        refresh(userName: string) {
            this.hub.invoke("refresh", userName);
        }

        candidate(userName: string, candidate: string) {
            this.hub.invoke("candidate", userName, candidate);
        }

        offer(userName: string, offer: string) {
            this.hub.invoke("offer", userName, offer);
        }

        answer(userName: string, answer: string) {
            this.hub.invoke("answer", userName, answer);
        }

        private errorHandler(error: string) {
            throw error;
        }
    }
}