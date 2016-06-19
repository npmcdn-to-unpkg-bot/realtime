﻿namespace Allors.Bootstrap {

    export function registerTemplates($templateCache: angular.ITemplateCacheService): void {

        // Form
        // ----
        FormTemplate.register($templateCache);

        // Object
        // ------
        // Shared
        // ------
        ImageModalTemplate.register($templateCache);

        // Fields
        // ------
        StaticTemplate.register($templateCache);
        StaticEnumTemplate.register($templateCache);
        TextTemplate.register($templateCache);
        TextareaTemplate.register($templateCache);
        TextAngularTemplate.register($templateCache);
        EnumTemplate.register($templateCache);
        SelectTemplate.register($templateCache);
        ImageTemplate.register($templateCache);
        RadioButtonTemplate.register($templateCache);
        DatepickerPopupTemplate.register($templateCache);

        // Field Groups
        // ------------
        // Internals
        LabeledTemplate.register($templateCache);
        LabelTemplate.register($templateCache);
        LabeledInputTemplate.register($templateCache);
        // Controls
        LabeledStaticTemplate.register($templateCache);
        StaticEnumGroupTemplate.register($templateCache);
        LabeledTextTemplate.register($templateCache);
        LabeledTextareaTemplate.register($templateCache);
        LabeledTextAngularTemplate.register($templateCache);
        LabeledEnumTemplate.register($templateCache);
        LabeledSelectTemplate.register($templateCache);
        LabeledImageTemplate.register($templateCache);
        LabeledRadioButtonTemplate.register($templateCache);
        LabeledDatepickerPopupTemplate.register($templateCache);

        // Model
        // -----
        SelectOneTemplate.register($templateCache);
        SelectManyTemplate.register($templateCache);
    }
}