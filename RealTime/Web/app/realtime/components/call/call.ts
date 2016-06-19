namespace App.Components.Call {
    
    declare var AdapterJS: any;

    class CallController {

        static $inject = ["allorsService"];
        constructor(private allors: Services.AllorsService) {

            navigator.getUserMedia(
                {
                    // Permissions to request
                    video: true,
                    audio: true
                },
                (stream: MediaStream) => {

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
        }

    }
    angular
        .module("app")
        .component("call", {
            controller: CallController,
            templateUrl: "/app/realtime/components/call/call.html",
            bindings: {
                person: "<"
            }
        } as any);
}