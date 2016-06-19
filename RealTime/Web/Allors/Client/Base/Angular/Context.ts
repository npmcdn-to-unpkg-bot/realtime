namespace Allors {
    export class Context {
        $q: angular.IQService;

        session: ISession;

        objects: { [name: string]: ISessionObject; } = {};
        collections: { [name: string]: ISessionObject[]; } = {};
        values: { [name: string]: any; } = {};

        constructor(
            public name: string,
            public database: Database,
            public workspace: Workspace) {

            this.$q = this.database.$q;

            this.session = new Session(this.workspace);
        }

        load(params?: any): angular.IPromise<any> {
            return this.$q((resolve, reject) => {

                return this.database.pull(this.name, params)
                    .then((response: Data.PullResponse) => {
                        try {
                            const requireLoadIds = this.workspace.diff(response);

                            if (requireLoadIds.objects.length > 0) {
                                this.database.sync(requireLoadIds)
                                    .then((loadResponse: Data.SyncResponse) => {
                                        this.workspace.sync(loadResponse);
                                        this.update(response);
                                        this.session.reset();
                                        resolve();
                                    })
                                    .catch(e2 => {
                                        reject(e2);
                                    });
                            } else {
                                this.update(response);
                                this.session.reset();
                                resolve();
                            }
                        } catch (e) {
                            reject(e);
                        }
                    })
                    .catch(e => {
                        reject(e);
                    });

            });
        }

        query(service: string, params: any): angular.IPromise<Result> {
            return this.$q((resolve, reject) => {

                this.database.pull(service, params)
                    .then(v => {
                        try {
                            const response = v as Data.PullResponse;
                            const requireLoadIds = this.workspace.diff(response);

                            if (requireLoadIds.objects.length > 0) {
                                this.database.sync(requireLoadIds)
                                    .then(u => {
                                        var loadResponse = u as Data.SyncResponse;
                                        this.workspace.sync(loadResponse);
                                        const result = new Result(this.session, response);
                                        resolve(result);
                                    })
                                    .catch((e2) => reject(e2));
                            } else {
                                const result = new Result(this.session, response);
                                resolve(result);
                            }
                        } catch (e) {
                            reject(e);
                        }
                    })
                    .catch((e) => reject(e));

            });
        }

        save(): angular.IPromise<Data.PushResponse> {
            return this.$q((resolve, reject) => {

                try {
                    const saveRequest = this.session.pushRequest();
                    this.database.push(saveRequest)
                        .then((pushResponse: Data.PushResponse) => {
                            this.session.pushResponse(pushResponse);
                            resolve(pushResponse);
                        })
                        .catch(e => {
                            reject(e);
                        });
                } catch (e) {
                    reject(e);
                }

            });
        }

        invoke(method: Method): angular.IPromise<Data.InvokeResponse>;
        invoke(service: string, args?: any): angular.IPromise<Data.InvokeResponse>;
        invoke(methodOrService: Method | string, args?: any): angular.IPromise<Data.InvokeResponse> {

            if (methodOrService instanceof Method) {
                return this.database.invoke(methodOrService);
            } else {
                return this.database.invoke(methodOrService as string, args);
            }
        }
        
        private update(response: Data.PullResponse): void {

            this.objects = { };
            this.collections  = { };
            this.values = { };

            _.map(response.namedObjects, (v, k) => {
                this.objects[k] = this.session.get(v);
            });

            _.map(response.namedCollections, (v, k) => {
                this.collections[k] = _.map(v, (obj) => { return this.session.get(obj) });
            });

            _.map(response.namedValues, (v, k) => {
                this.values[k] = v;
            });
        }
    }
}