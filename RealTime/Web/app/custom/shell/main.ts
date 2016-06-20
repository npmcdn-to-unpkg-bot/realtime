namespace App.Shell {

    class MainController extends Pages.Page {

        // Only on same subnet
        iceServers = [
        ];

        me: Person;
        onlinePeople: Person[];

        other: Person;

        calls: Call[];
        callObjectStates: CallObjectState[];

        requestedCalls: Call[];
        acceptedCalls: Call[];

        requestedCallsWhereCaller: Call[];
        requestedCallsWhereCallee: Call[];

        localStream: MediaStream;
        remoteStream: MediaStream;

        peerConnection: RTCPeerConnection;

        static $inject = ["allorsService", "$scope"];
        constructor(private allors: Services.AllorsService, $scope: angular.IScope) {
            super("Main", allors, $scope);

            this.$scope.$on(AllorsHub.onRefreshEvent, () => this.refresh());
            this.$scope.$on(AllorsHub.onCandidateEvent, (event, candidate: string) => this.onCandidate(candidate));
            this.$scope.$on(AllorsHub.onOfferEvent, (event, offer: string) => this.onOffer(offer));
            this.$scope.$on(AllorsHub.onAnswerEvent, (event, answer: string) => this.onAnswer(answer));

            navigator.getUserMedia(
                {
                    // Permissions to request
                    video: true,
                    audio: false
                },
                (stream: MediaStream) => {

                    this.localStream = stream;
                    var video = document.getElementById("localVideo") as any;
                    AdapterJS.attachMediaStream(video, stream);
                    video.play();
                },
                error => {
                    if (error.name === "ConstraintNotSatisfiedError") {
                    } else if (error.name === "PermissionDeniedError") {
                    }

                    allors.$log.error(error);
                }
            );

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
            call.CurrentObjectState = this.callObjectStates.filter(v=>v.isAccepted)[0];
            this.save()
                .then(() => this.allors.application.hub.refresh(other.UserName));
        }

        protected refresh(): angular.IPromise<any> {
            return this.load()
                .then(() => {

                    this.me = this.objects["me"] as Person;
                    this.onlinePeople = this.collections["onlinePeople"] as Person[];
                    this.calls = this.collections["calls"] as Call[];
                    this.callObjectStates = this.collections["callObjectStates"] as CallObjectState[];

                    this.requestedCalls = this.calls.filter(v => v.CurrentObjectState.isRequested);
                    this.acceptedCalls = this.calls.filter(v => v.CurrentObjectState.isAccepted);

                    this.requestedCallsWhereCallee = this.requestedCalls.filter(v => v.isCallee(this.me));
                    this.requestedCallsWhereCaller = this.requestedCalls.filter(v => v.isCaller(this.me));

                    this.manageCall();
                });
        }

        private manageCall() {
            if (this.acceptedCalls && this.acceptedCalls.length === 1) {

                var acceptedCall = this.acceptedCalls[0];
                this.other = acceptedCall.other(this.me);

                if (!this.peerConnection) {
                    this.peerConnection = new RTCPeerConnection({ iceServers: this.iceServers });
                    this.peerConnection.addStream(this.localStream);

                    this.peerConnection.onicecandidate = event => {
                        if (event.candidate) {

                            this.allors.$log.info(`peerConnection.onicecandidate: ${event}`);

                            this.allors.application.hub.candidate(this.other.UserName, JSON.stringify(event.candidate));
                        }
                    };

                    // Stream handlers
                    this.peerConnection.onaddstream = event => {

                        this.allors.$log.info(`peerConnection.onaddstream: ${event}`);

                        this.remoteStream = event.stream;
                        var video = document.getElementById("remoteVideo") as any;
                        AdapterJS.attachMediaStream(video, this.remoteStream);
                        video.play();
                    };

                    this.peerConnection.onremovestream = event => {

                        this.allors.$log.info(`peerConnection.onremovestream: ${event}`);

                        var video = document.getElementById("remoteVideo") as any;
                        AdapterJS.attachMediaStream(video, null);
                        this.remoteStream = null;
                    };


                    if (acceptedCall.isCaller(this.me)) {
                        this.peerConnection.createOffer(offer => {

                            this.allors.$log.info(`peerConnection.createOffer: ${offer}`);
                            
                            this.peerConnection.setLocalDescription(offer);
                            this.allors.application.hub.offer(this.other.UserName, JSON.stringify(offer));
                        }, error => {
                            this.allors.$log.error(error);
                        });
                    }
                }
            }
        }

        private onCandidate(candidate: string) {
            this.allors.$log.info(`onCandidate: ${candidate}`);

            this.peerConnection.addIceCandidate(new RTCIceCandidate(JSON.parse(candidate)), () => {}, () => {});
        }

        private onOffer(offer: string) {
            this.allors.$log.info(`onOffer: ${offer}`);

            this.peerConnection.setRemoteDescription(new RTCSessionDescription(JSON.parse(offer)),
                () => {
                    this.allors.$log.info(`peerConnection.setRemoteDescription`);
                },
                () => {
                    this.allors.$log.error(`peerConnection.setRemoteDescription`);
                });

            this.peerConnection.createAnswer( answer => {
                this.allors.$log.info(`peerConnection.createAnswer`);

                this.peerConnection.setLocalDescription(answer,
                    () => {
                        this.allors.$log.info(`peerConnection.setLocalDescription`);

                        this.allors.application.hub.answer(this.other.UserName, JSON.stringify(answer));
                    },
                    () => {
                        this.allors.$log.error(`peerConnection.setLocalDescription`);
                    });
            },
            error => {
                this.allors.$log.error(`peerConnection.createAnswer`);
            });
        }

        private onAnswer(answer: string) {
            this.allors.$log.info(`onAnswer: ${answer}`);

            this.peerConnection.setRemoteDescription(new RTCSessionDescription(JSON.parse(answer)),
                () => {
                    this.allors.$log.info(`peerConnection.setRemoteDescription`);
                },
                () => {
                    this.allors.$log.error(`peerConnection.setRemoteDescription`);
                });
        }
    }
    angular
        .module("app")
        .controller("mainController", MainController);
}
