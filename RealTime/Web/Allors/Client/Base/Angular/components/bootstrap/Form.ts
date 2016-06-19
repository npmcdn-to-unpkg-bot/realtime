namespace Allors.Bootstrap {
    export class FormTemplate {

        static name = "allors/bootstrap/form";

        private static view =
`<form ng-class="$ctrl.horizontal ? 'form-horizontal' : ''">
<ng-transclude />
</form>`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(FormTemplate.name, FormTemplate.view);
        }
    }

    export class FormController  {

        static require = {
            form: "^bForm"
        };

        static bindings = {
            horizontal: "<"
        }

        horizontal: boolean;

        static $inject = ["$log"];
        constructor(private $log: angular.ILogService) {
        }
    }

    angular
        .module("allors")
        .component("bForm", {
            controller: FormController,
            templateUrl: FormTemplate.name,
            transclude: true,
            bindings: FormController.bindings
        } as any);
}
