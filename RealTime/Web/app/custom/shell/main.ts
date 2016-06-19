namespace App.Shell {

    declare var AdapterJS: any;

    class MainController extends Pages.Page {

        iceServers = [
            { urls: "stun:stun.l.google.com:19302" },
        ];

        me: Person;
        onlinePeople: Person[];

        localStream: MediaStream;
        remoteStream: MediaStream;

        peerConnection: RTCPeerConnection;

        processedSignals: string[];

        remoteEndPoint: EndPoint;

        static $inject = ["allorsService", "$scope"];
        constructor(private allors: Services.AllorsService, $scope: angular.IScope) {
            super("Main", allors, $scope);

            this.$scope.$on(AllorsHub.refreshEventName, () => this.refresh());

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
            call.Initiator = this.me;
            this.me.EndPoint.Call = call;
            other.EndPoint.Call = call;

            this.save();
        }

        protected refresh(): angular.IPromise<any> {
            return this.load()
                .then(() => {
                    this.me = this.objects["me"] as Person;
                    this.onlinePeople = this.collections["onlinePeople"] as Person[];

                    this.remoteEndPoint = this.objects["remoteEndPoint"] as EndPoint;

                    this.manageCall();
                });
        }

        manageCall() {
            if (this.me.EndPoint.Call) {
                if (!this.peerConnection) {
                    this.processedSignals = new Array<string>();

                    this.peerConnection = new RTCPeerConnection({ iceServers: this.iceServers });
                    this.peerConnection.addStream(this.localStream);

                    this.peerConnection.onicecandidate = event => {
                        if (event.candidate) {
                            const candidate = JSON.stringify({ "candidate": event.candidate });
                            const signal = this.me.session.create("Signal") as Signal;
                            signal.Value = candidate;
                            this.me.EndPoint.AddSignal(signal);
                            this.save()
                                .then(() => this.allors.application.hub.serverRefresh());

                        } else {
                            this.me.EndPoint.Established = true;
                            this.save()
                                .then(() => this.allors.application.hub.serverRefresh());
                        }
                    };

                    // Stream handlers
                    this.peerConnection.onaddstream = event => {
                        this.remoteStream = event.stream;
                        var video = document.getElementById("remoteVideo") as any;
                        AdapterJS.attachMediaStream(video, this.remoteStream);
                        video.play();
                    };

                    this.peerConnection.onremovestream = event => {
                        var video = document.getElementById("remoteVideo") as any;
                        AdapterJS.attachMediaStream(video, null);
                        this.remoteStream = null;
                    };

                    if (this.me.EndPoint.Call.Initiator === this.me) {
                        this.peerConnection.createOffer(desc => {

                            this.allors.$log.debug(`Offer returned ${desc}`, "WebRTC");

                            this.peerConnection.setLocalDescription(desc, () => {
                                this.allors.$log.debug(`Sending signal to the server${this.peerConnection.localDescription}`, "WebRTC");

                                var signal = this.me.session.create("Signal") as Signal;
                                signal.Value = JSON.stringify({ "sdp": this.peerConnection.localDescription });
                                this.me.EndPoint.AddSignal(signal);
                                this.save()
                                    .then( () => this.allors.application.hub.serverRefresh());

                            }, (error) => {
                                this.allors.$log.error(error);
                            });
                        }, error => {
                            this.allors.$log.error("Error creating WebRTC session", "WebRTC", error);
                        });
                    }
                }

                if (this.remoteEndPoint) {
                    const unprocessedSignals = this.remoteEndPoint.Signals.filter(v => this.processedSignals.indexOf(v.Value) < 0);

                    unprocessedSignals.forEach(v => {

                        this.processedSignals.push(v.Value);

                        var signal = JSON.parse(v.Value);
                        var candidate = JSON.parse(signal.candidate);

                        this.allors.$log.debug(`Received Signal: ${JSON.stringify(candidate)}`, "WebRTC");

                        // Route signal based on type
                        if (candidate.sdp) {
                            this.peerConnection.setRemoteDescription(new RTCSessionDescription(candidate.sdp), () => {
                                if (this.peerConnection.remoteDescription.type === "offer") {

                                    //this.notifier.debug("Received offer, sending response...", "WebRTC");
                                    //this.application.allors.onReadyForStreamCallback(connection);

                                    this.peerConnection.createAnswer(desc => {
                                        this.peerConnection.setLocalDescription(desc, () => {

                                            var signal = this.me.session.create("Signal") as Signal;
                                            signal.Value = JSON.stringify({ "sdp": this.peerConnection.localDescription });
                                            this.me.EndPoint.AddSignal(signal);
                                            this.save()
                                                .then(() => this.allors.application.hub.serverRefresh());

                                        }, (error) => {
                                            this.allors.$log.error(`Error creating local description: ${error}`, "WebRTC");
                                        });
                                    },
                                        (error) => {
                                            this.allors.$log.error(`Error creating session description: ${error}`, "WebRTC");
                                        });
                                } else if (this.peerConnection.remoteDescription.type === "answer") {
                                    this.allors.$log.debug("Received answer", "WebRTC");
                                }
                            }, (error) => {
                                this.allors.$log.error(`Error creating remote description: ${error}`, "WebRTC");
                            });
                        } else if (candidate.candidate) {
                            this.peerConnection.addIceCandidate(new RTCIceCandidate(candidate), () => { }, (error) => { });
                        }

                    });
                }

            } else {
                // TODO: If peerconnection then destroy
            }
        }
    }
    angular
        .module("app")
        .controller("mainController", MainController);
}
