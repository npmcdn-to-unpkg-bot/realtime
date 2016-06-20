namespace App {

    export class AllorsHub {

        static onRefreshEvent = "AllorsHub.OnRefresh";
        static onReadyEvent = "AllorsHub.OnReady";
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
                        'onReady': (callId, offer) => {
                            this.$rootScope.$broadcast(AllorsHub.onReadyEvent, callId, offer);
                        },
                        'onOffer': (callId, offer) => {
                            this.$rootScope.$broadcast(AllorsHub.onOfferEvent, callId, offer);
                        },
                        'onCandidate': (callId, candidate) => {
                            this.$rootScope.$broadcast(AllorsHub.onCandidateEvent, callId, candidate);
                        },
                        'onAnswer': (callId, answer) => {
                            this.$rootScope.$broadcast(AllorsHub.onAnswerEvent, callId, answer);
                        }
                    },
                    methods: ["refresh", "candidate", "offer", "answer","ready"],
                    errorHandler: this.errorHandler
                });
        }

        refresh(userName: string) {
            this.hub.invoke("refresh", userName);
        }

        ready(userName: string, callId: string) {
            this.hub.invoke("ready", userName, callId);
        }

        candidate(userName: string, callId: string, candidate: string) {
            this.hub.invoke("candidate", userName, callId, candidate);
        }

        offer(userName: string, callId: string, offer: string) {
            this.hub.invoke("offer", userName, callId, offer);
        }

        answer(userName: string, callId: string, answer: string) {
            this.hub.invoke("answer", userName, callId, answer);
        }
       
        private errorHandler(error: string) {
            throw error;
        }
    }
}