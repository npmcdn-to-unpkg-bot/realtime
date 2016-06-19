namespace Allors {
    export abstract class Control {

        context: Context;
        events: Events;
        session: ISession;

        constructor(name: string, database: Database, workspace: Workspace, $rootScope: angular.IRootScopeService, protected $scope: angular.IScope, protected $q: angular.IQService, protected $log: angular.ILogService) {
            this.context = new Context(name, database, workspace);
            this.events = new Events(this.context, $rootScope, $scope);

            this.session = this.context.session;

            this.events.onRefresh(() => this.refresh());
        }

        // Context
        get objects(): { [name: string]: ISessionObject; } {
            return this.context.objects;
        }

        get collections(): { [name: string]: ISessionObject[]; } {
            return this.context.collections;
        }

        get values(): { [name: string]: any; } {
            return this.context.values;
        }

        get hasChanges(): boolean {
            return this.context.session.hasChanges;
        }

        // Commands
        load(params?: any): angular.IPromise<any> {
            
            return this.context.load(params)
                    .catch((e) => { throw e });
        }

        save(): angular.IPromise<any> {
            return this.$q((resolve, reject) => {

                this.context
                    .save()
                    .then((saveResponse: Data.PushResponse) => {
                        if (saveResponse.hasErrors) {
                            reject(new SaveError(this.context, saveResponse));
                        } else {
                            resolve(saveResponse);
                        }
                    }, (e) => {
                        throw e;
                    })
                    .finally(() => this.events.broadcastRefresh());

            });
        }

        invoke(method: Method): angular.IPromise<Data.InvokeResponse>;
        invoke(service: string, args?: any): angular.IPromise<Data.InvokeResponse>;
        invoke(methodOrService: Method | string, args?: any): angular.IPromise<Data.InvokeResponse> {
            return this.$q((resolve, reject) => {

                if (methodOrService instanceof Method) {
                    return this.context
                        .invoke(methodOrService)
                        .then((invokeResponse: Data.InvokeResponse) => {
                            if (invokeResponse.hasErrors) {
                                reject(new InvokeError(this.context, invokeResponse));
                            } else {
                                resolve(invokeResponse);
                            }
                        }, (e) => {
                            throw e;
                        })
                        .finally(() => this.events.broadcastRefresh());
                } else {
                    return this.context
                        .invoke(methodOrService as string, args)
                        .then((invokeResponse: Data.InvokeResponse) => {
                            if (invokeResponse.hasErrors) {
                                reject(new InvokeError(this.context, invokeResponse));
                            } else {
                                resolve(invokeResponse);
                            }
                        }, (e) => {
                            throw e;
                        })
                        .finally(() => this.events.broadcastRefresh());
                }

            });
        }

        saveAndInvoke(method: Method): angular.IPromise<Data.InvokeResponse>;
        saveAndInvoke(service: string, args?: any): angular.IPromise<any>;
        saveAndInvoke(methodOrService: Method | string, args?: any): angular.IPromise<any> {
            return this.$q((resolve, reject) => {

                return this.context
                    .save()
                    .then((saveResponse: Data.PushResponse) => {
                            if (saveResponse.hasErrors) {
                                reject(new SaveError(this.context, saveResponse));
                            } else {
                                this.refresh()
                                    .then(() => {
                                        if (methodOrService instanceof Method) {
                                            this.context.invoke(methodOrService)
                                                .then((invokeResponse: Data.InvokeResponse) => {
                                                    if (invokeResponse.hasErrors) {
                                                        reject(new InvokeError(this.context, invokeResponse));
                                                    } else {
                                                        resolve(invokeResponse);
                                                    }
                                                }, (e) => {
                                                    throw e;
                                                })
                                                .finally(() => this.events.broadcastRefresh());
                                        } else {
                                            this.context.invoke(methodOrService as string, args)
                                                .then((invokeResponse: Data.InvokeResponse) => {
                                                    if (invokeResponse.hasErrors) {
                                                        reject(new InvokeError(this.context, invokeResponse));
                                                    } else {
                                                        resolve(invokeResponse);
                                                    }
                                                }, (e) => {
                                                    throw e;
                                                })
                                                .finally(() => this.events.broadcastRefresh());
                                        }
                                    }, (e) => {
                                        throw e;
                                    });
                            }
                    }, (e) => {
                        throw e;
                    });
            });
        }

        query(query: string, args: any): angular.IPromise<any> {
            return this.context
                .query(query, args)
                .catch((e) => { throw e; });
        }

        queryResults(query: string, args: any): angular.IPromise<any> {
            return this.context
                .query(query, args)
                .then(result => {
                    var results = result.collections["results"];
                    return results;
                })
                .catch((e) => { throw e; });
        }

        protected abstract refresh(): angular.IPromise<any>;

        private handleError(e: any): any {
            if (e.hasErrors) {
                return e;
            } else {
                throw e;
            }
        }
    }
}