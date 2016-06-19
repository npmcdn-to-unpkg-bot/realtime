namespace App.Services
{
    export class TranslationService {

        static $inject = ["$filter"];
        constructor(private $filter) {
           
        }

        translate(key: string): string {
            return this.$filter("translate")(key);
        }       
    }

    angular
        .module("app")
        .service("translationService",
            TranslationService);
}