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

        offer: RTCSessionDescription;
        answer: RTCSessionDescription;

        me: Person;
        other: Person;
        call: Call;

        isCaller: boolean;

        static $inject = ["allorsService", "$scope"];
        constructor(private allors: Services.AllorsService, $scope: angular.IScope) {
            super("Main", allors, $scope);

            this.$scope.$on(AllorsHub.onCandidateEvent, (event, candidate: string) => this.remoteOnCandidate(candidate));
            this.$scope.$on(AllorsHub.onOfferEvent, (event, offer: string) => this.onOffer(offer));
            this.$scope.$on(AllorsHub.onAnswerEvent, (event, answer: string) => this.onAnswer(answer));
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
                    }
                    
                    if (call) {
                        if (!this.call) {
                            this.call = call;
                            this.other = call.other(this.me);
                            this.isCaller = this.call.isCaller(this.me);

                            this.setup();
                        } else {
                            this.update();
                        }
                    }
                });
        }

        private hangup() {
            AdapterJS.attachMediaStream(this.localVideo, null);
            AdapterJS.attachMediaStream(this.remoteVideo, null);
            this.localStream.stop();
            this.remoteStream.stop();
            this.peerConnection.close();

            this.localStream = null;
            this.remoteStream = null;
            this.peerConnection = null;
            this.other = null;
            this.call = null;
            this.isCaller = null;
            this.offer = null;
            this.answer = null;
        }

        private setup() {
            navigator.getUserMedia(
                {
                    // Permissions to request
                    video: true,
                    audio: false
                },
                (stream: MediaStream) => {

                    this.localStream = stream;
                    this.localVideo = AdapterJS.attachMediaStream(document.getElementById("localVideo"), stream);
                    this.localVideo.play();

                    if (!this.peerConnection) {
                        this.peerConnection = new RTCPeerConnection({ iceServers: this.iceServers });
                        this.peerConnection.addStream(this.localStream);

                        this.peerConnection.onicecandidate = event => this.localOnCandidate(event);

                        // Stream handlers
                        this.peerConnection.onaddstream = event => this.onAddStream(event);

                        this.peerConnection.onremovestream = event => this.onRemoveStream(event);
                    }
                },
                error => {
                    this.allors.$log.error(error);
                }
            );
        }

        private update() {

            if (!this.offer && this.isCaller) {
                this.peerConnection.createOffer(offer => {

                    this.allors.$log.info(`peerConnection.createOffer: ${offer}`);

                    this.offer = offer;
                    this.peerConnection.setLocalDescription(offer);

                }, error => {
                    this.allors.$log.error(error);
                });
            }

            if (this.isCaller && this.offer && !this.answer) {
                this.allors.application.hub.offer(this.other.UserName, JSON.stringify(this.offer));
            }
        }

        private localOnCandidate(event: RTCIceCandidateEvent): void {
            if (event.candidate) {

                this.allors.$log.info(`localOnCandidate: ${event}`);

                this.allors.application.hub.candidate(this.other.UserName, JSON.stringify(event.candidate));
            }
        }

        private remoteOnCandidate(candidate: string) {
            this.allors.$log.info(`remoteOnCandidate: ${candidate}`);

            this.peerConnection.addIceCandidate(new RTCIceCandidate(JSON.parse(candidate)), () => { }, () => { });
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

            this.answer = new RTCSessionDescription(JSON.parse(answer));
            this.peerConnection.setRemoteDescription(this.answer,
                () => {
                    this.allors.$log.info(`peerConnection.setRemoteDescription`);
                },
                () => {
                    this.allors.$log.error(`peerConnection.setRemoteDescription`);
                });
        }

        private onAddStream(event: RTCMediaStreamEvent): void {
            this.allors.$log.info(`onAddStream: ${event}`);

            this.remoteStream = event.stream;
            this.remoteVideo = document.getElementById("remoteVideo") as any;
            this.remoteVideo = AdapterJS.attachMediaStream(this.remoteVideo, this.remoteStream);
            this.remoteVideo.play();
        }

        private onRemoveStream(event: RTCMediaStreamEvent): void {

            this.allors.$log.info(`onRemoveStream: ${event}`);

            var video = document.getElementById("remoteVideo") as any;
            AdapterJS.attachMediaStream(video, null);
            this.remoteStream = null;
        }
    }
    angular
        .module("app")
        .controller("mainController", MainController);
}
