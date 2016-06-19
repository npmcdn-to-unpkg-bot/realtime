namespace App {

    export class Application {

        hub: AllorsHub;

        constructor(public allors: Services.AllorsService, Hub: ngSignalr.HubFactory) {

            this.hub = new AllorsHub(allors.$rootScope, Hub);
        }

    }
}