namespace Allors.Bootstrap {
    export class LabeledTemplate {
        static name = "allors/bootstrap/labeled";

        private static view = 
`<div class="form-group" ng-if="$ctrl.field.canRead">
<ng-transclude/>
</div>`;

        static register(templateCache: angular.ITemplateCacheService) {
            templateCache.put(LabeledTemplate.name, LabeledTemplate.view);
        }
    }

    class LabeledComponent {
        field: Field;

        static $inject = ["$log"];
        constructor($log: angular.ILogService) {
        }
    }

    angular
        .module("allors")
        .component("bLabeled", {
            controller: LabeledComponent,
            transclude: true,
            templateUrl: LabeledTemplate.name,
            require: {
                form: "^bForm"
            },
            bindings: {
                field: "<"
            }
        } as any);
}
