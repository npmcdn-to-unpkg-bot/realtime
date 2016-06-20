namespace App.Shell {

    class MainController extends Pages.Page {

        // Only on same subnet
        iceServers = [
        ];

        callObjectStates: CallObjectState[];
        
        localStream: MediaStream;
        remoteStream: MediaStream;

        localVideo: any;
        remoteVideo: any;

        peerConnection: RTCPeerConnection;

        me: Person;
        other: Person;
        call: Call;

        isCaller: boolean;

        ready: boolean;

        static $inject = ["allorsService", "$scope"];
        constructor(private allors: Services.AllorsService, $scope: angular.IScope) {
            super("Main", allors, $scope);

            this.$scope.$on("$destroy", () => {
                this.hangup();
            });

            this.$scope.$on(AllorsHub.onRefreshEvent, () => this.refresh());

            this.$scope.$on(AllorsHub.onReadyEvent, (event, callId: string) => this.onReady(callId));
            this.$scope.$on(AllorsHub.onCandidateEvent, (event, callId: string, candidate: string) => this.remoteOnCandidate(callId, candidate));
            this.$scope.$on(AllorsHub.onOfferEvent, (event, callId: string, offer: string) => this.onOffer(callId, offer));
            this.$scope.$on(AllorsHub.onAnswerEvent, (event, callId: string, answer: string) => this.onAnswer(callId, answer));
        }

        hangup() {

            this.allors.$log.info(`hangup`);

            const call = this.call;
            const other = this.other;

            AdapterJS.attachMediaStream(document.getElementById("localVideo"), null);
            AdapterJS.attachMediaStream(document.getElementById("remoteVideo"), null);

            if (this.peerConnection) {
                this.peerConnection.close();
            }

            this.ready = null;
            this.localStream = null;
            this.remoteStream = null;
            this.peerConnection = null;
            this.other = null;
            this.call = null;
            this.isCaller = null;
            this.localVideo = null;
            this.remoteVideo = null;

            call.CurrentObjectState = this.callObjectStates.filter(v => v.isEnded)[0];
            this.context
                .save()
                .finally(() => this.allors.application.hub.refresh(other.UserName));
        }

        protected refresh(): angular.IPromise<any> {
            return this.load()
                .then(() => {

                    this.me = this.objects["me"] as Person;
                    this.callObjectStates = this.collections["callObjectStates"] as CallObjectState[];

                    var call = this.objects["call"] as Call;

                    // hang up current call if new call is accepted
                    if (this.call && this.call !== call) {
                        this.hangup();
                    } else {
                        if (call) {
                            if (!this.call) {
                                this.call = call;
                                this.other = call.other(this.me);
                                this.isCaller = this.call.isCaller(this.me);

                                navigator.getUserMedia({ video: true, audio: false },
                                    (stream: MediaStream) => {

                                        this.localStream = stream;
                                        this.localVideo = AdapterJS.attachMediaStream(document.getElementById("localVideo"), stream);
                                        this.localVideo.play();

                                        if (!this.peerConnection) {
                                            this.peerConnection = new RTCPeerConnection({ iceServers: this.iceServers });
                                            this.peerConnection.addStream(this.localStream);

                                            this.allors.application.hub.ready(this.other.UserName, this.call.id.toString());
                                        }
                                    },
                                    error => {
                                        this.allors.$log.error(error);
                                        this.hangup();
                                    }
                                );
                            }
                        }
                    }
                });
        }

        private onReady(callId: string) {
            if (this.isCaller) {
                if (!this.ready) {
                    this.ready = true;

                    this.peerConnection.onicecandidate = event => this.localOnCandidate(event);
                    this.peerConnection.onaddstream = event => this.onAddStream(event);
                    this.peerConnection.onremovestream = event => this.onRemoveStream(event);

                    this.peerConnection.createOffer(offer => {

                        this.allors.$log.info(`peerConnection.createOffer: ${offer}`);

                        this.peerConnection.setLocalDescription(offer);

                        this.allors.application.hub.offer(this.other.UserName, this.call.id.toString(), JSON.stringify(offer));
                    },
                    error => {
                        this.allors.$log.error(error);
                    });

                }
            } else {
                // ping back
                if (this.peerConnection) {
                    this.allors.application.hub.ready(this.other.UserName, this.call.id.toString());
                }
            }
        }

        private localOnCandidate(event: RTCIceCandidateEvent): void {
            if (event.candidate) {

                this.allors.$log.info(`localOnCandidate: ${event}`);

                this.allors.application.hub.candidate(this.other.UserName, this.call.id.toString(), JSON.stringify(event.candidate));
            }
        }

        private remoteOnCandidate(callId: string, candidate: string) {
            this.allors.$log.info(`remoteOnCandidate: ${candidate}`);

            this.peerConnection.addIceCandidate(new RTCIceCandidate(JSON.parse(candidate)), () => { }, () => { });
        }
        
        private onOffer(callId: string, offer: string) {
            this.allors.$log.info(`onOffer: ${offer}`);

            this.lazyInit();

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

                        this.allors.application.hub.answer(this.other.UserName, this.call.id.toString(), JSON.stringify(answer));
                    },
                    () => {
                        this.allors.$log.error(`peerConnection.setLocalDescription`);
                    });
            },
            error => {
                this.allors.$log.error(`peerConnection.createAnswer`, error);
            });
        }

        private onAnswer(callId: string, answer: string) {
            this.allors.$log.info(`onAnswer: ${answer}`);

            this.lazyInit();

            this.peerConnection.setRemoteDescription(new RTCSessionDescription(JSON.parse(answer)),
                () => {
                    this.allors.$log.info(`peerConnection.setRemoteDescription`);
                },
                () => {
                    this.allors.$log.error(`peerConnection.setRemoteDescription`);
                });
        }

        private onAddStream(event: RTCMediaStreamEvent): void {
            this.allors.$log.info(`onAddStream: ${event}`);

            this.lazyInit();

            this.remoteStream = event.stream;
            this.remoteVideo = AdapterJS.attachMediaStream(document.getElementById("remoteVideo"), this.remoteStream);
            this.remoteVideo.play();
        }

        private onRemoveStream(event: RTCMediaStreamEvent): void {

            this.allors.$log.info(`onRemoveStream: ${event}`);

            this.lazyInit();

            AdapterJS.attachMediaStream(this.remoteVideo, null);
            this.remoteStream = null;
        }

        private lazyInit() {
            if (!this.ready) {
                this.ready = true;

                this.peerConnection.onicecandidate = event => this.localOnCandidate(event);
                this.peerConnection.onaddstream = event => this.onAddStream(event);
                this.peerConnection.onremovestream = event => this.onRemoveStream(event);
            }
        }
    }
    angular
        .module("app")
        .controller("mainController", MainController);
}
