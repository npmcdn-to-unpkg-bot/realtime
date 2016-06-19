﻿namespace App.Pages {
    import Method = Allors.Method;
    import Data = Allors.Data;

    export abstract class Page extends Allors.Control {

        toastr: angular.toastr.IToastrService;

        constructor(name: string, allors: Services.AllorsService, $scope: angular.IScope) {
            super(name, allors.database, allors.workspace, allors.$rootScope, $scope, allors.$q, allors.$log);

            this.toastr = allors.toastr;
        }

        save(): angular.IPromise<any> {
            return this.$q((resolve, reject) => {
                super.save()
                    .then( (saveResponse: Allors.Data.PushResponse) => {
                        this.toastr.info("Successfully saved.");
                        resolve(saveResponse);
                    })
                    .catch( (e: Allors.DatabaseError) => {
                        this.errorResponse(e.reponseError);
                        reject(e);
                    });
            });
        }

        invoke(method: Method): angular.IPromise<Data.InvokeResponse>;
        invoke(service: string, args?: any): angular.IPromise<Data.InvokeResponse>;
        invoke(methodOrService: Method | string, args?: any): angular.IPromise<Data.InvokeResponse> {
            return this.$q((resolve, reject) => {

                var superInvoke: angular.IPromise<Allors.Data.InvokeResponse>;
                if (methodOrService instanceof Method) {
                    superInvoke = super.invoke(methodOrService);

                } else {
                    superInvoke = super.invoke(methodOrService as string, args);
                }

                superInvoke.then((invokeResponse: Allors.Data.InvokeResponse) => {
                    this.toastr.info("Successfully executed.");
                    resolve(invokeResponse);
                })
                .catch((e: Allors.DatabaseError) => {
                    this.errorResponse(e.reponseError);
                    reject(e);
                });

            });
        }

        saveAndInvoke(method: Method): angular.IPromise<Data.InvokeResponse>;
        saveAndInvoke(service: string, args?: any): angular.IPromise<Data.InvokeResponse>;
        saveAndInvoke(methodOrService: Method | string, args?: any): angular.IPromise<Data.InvokeResponse> {
            return this.$q((resolve, reject) => {

                var superSaveAndInvoke: angular.IPromise<Allors.Data.InvokeResponse>;
                if (methodOrService instanceof Method) {
                    superSaveAndInvoke = super.saveAndInvoke(methodOrService);
                } else {
                    superSaveAndInvoke = super.saveAndInvoke(methodOrService as string, args);
                }

                superSaveAndInvoke.then((invokeResponse: Allors.Data.InvokeResponse) => {
                    this.toastr.info("Successfully executed.");
                    resolve(invokeResponse);
                })
                .catch((e: Allors.DatabaseError) => {
                    this.errorResponse(e.reponseError);
                    reject(e);
                });

            });
        }

        protected errorResponse(error: Allors.Data.ErrorResponse) {
            let title: string;
            var message = "<div class=\"response-errors\">";

            if (error.errorMessage && error.errorMessage.length > 0) {
                title = "General Error";
                message += `<p>${error.errorMessage}</p>`;
            }

            if ((error.versionErrors && error.versionErrors.length > 0) ||
                (error.missingErrors && error.missingErrors.length > 0)) {
                title = "Concurrency Error";
                message += "<p>Modifications were detected since last access.</p>";
            }

            if (error.accessErrors && error.accessErrors.length > 0) {
                title = "Access Error";
                message += "<p>You do not have the required rights.</p>";
            }

            if (error.derivationErrors && error.derivationErrors.length > 0) {
                title = "Derivation Errors";

                message += "<ul>";
                error.derivationErrors.map(derivationError => {
                    message += `<li>${derivationError.m}</li>`;
                });

                message += "</ul>";
            }

            message += "<div>";

            this.toastr.error(message, title, {
                allowHtml: true,
                closeButton: true,
                timeOut: 0
            });
        }
    }
}