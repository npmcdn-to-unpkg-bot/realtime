namespace Allors.Bootstrap {
    export abstract class Field {
        label;
        placeholder;
        help;
      
        private f;
        private o;
        private r;
        private d;
        private l: (any) => angular.IPromise<any>;
        
        constructor(public $log: angular.ILogService, public $translate: angular.translate.ITranslateService) {
        }

        get form(): FormController {
            return this.f;
        }

        set form(value: FormController) {
            this.f = value;
            this.onBind();
        }
        
        get object(): SessionObject {
            return this.o;
        }

        set object(value: SessionObject) {
            this.o = value;
            this.onBind();
        }

        get relation(): string {
            return this.r;
        }

        set relation(value: string) {
            this.r = value;
            this.onBind();
        }

        get display(): string {
            return this.d;
        }

        set display(value: string) {
            this.d = value;
            this.onBind();
        }

        get lookup(): (any) => angular.IPromise<any> {
            return this.l;
        }

        set lookup(value: (any) => angular.IPromise<any>) {
            this.l = value;
            this.onBind();
        }

        get objectType(): Meta.ObjectType {
            return this.object && this.object.objectType;
        }

        get roleType(): Meta.RoleType {
            return this.object && this.object.objectType.roleTypeByName[this.relation];
        }

        get canRead(): boolean {
            let canRead = false;
            if (this.object) {
                canRead = this.object.canRead(this.relation);
            }

            return canRead;
        }

        get canWrite(): boolean {
            let canWrite = false;
            if (this.object) {
                canWrite = this.object.canWrite(this.relation);
            }

            return canWrite;
        }
        
        get role(): any {
            return this.object && this.object[this.relation];
        }

        set role(value: any) {
            this.object[this.relation] = value;
        }

        get displayValue(): any {
            return this.role && this.role[this.display];
        }

        $onInit() {
            this.derive();
        }

        onBind() {
            this.derive();
        };

        derive() {
            if (this.roleType && this.$translate) {
                if (this.label === undefined) {
                    this.label = null;

                    const key1 = `meta_${this.objectType.name}_${this.roleType.name}_Label`;
                    const key2 = `meta_${this.roleType.objectType}_${this.roleType.name}_Label`;
                    this.translate(key1, key2, (value) => this.label = value);

                    if (this.label === undefined || this.label === null) {
                        this.label = Filters.Humanize.filter(this.relation);

                        const suffix = "Enum";
                        if (this.label.indexOf(suffix, this.label.length - suffix.length) !== -1) {
                            this.label = this.label.substring(0, this.label.length - suffix.length);
                        }
                    }
                }

                if (this.placeholder === undefined) {
                    this.placeholder = null;

                    const key1 = `meta_${this.objectType.name}_${this.roleType.name}_Placeholder`;
                    const key2 = `meta_${this.roleType.objectType}_${this.roleType.name}_Placeholder`;
                    this.translate(key1, key2, (value) => this.placeholder = value);
                }

                if (this.help === undefined) {
                    this.help = null;

                    const key1 = `meta_${this.objectType.name}_${this.roleType.name}_Help`;
                    const key2 = `meta_${this.roleType.objectType}_${this.roleType.name}_Help`;
                    this.translate(key1, key2, (value) => this.help = value);
                }
            }
        }

        translate(key1: string, key2: string, set: (translation: string) => void, setDefault?: () => void) {
            this.$translate(key1)
                .then(translation => {
                    if (key1 !== translation) {
                        set(translation);
                    } else {
                        this.$translate(key2)
                            .then(translation => {
                                if (key2 !== translation) {
                                    set(translation);
                                } else {
                                    if (setDefault) {
                                        setDefault();
                                    }
                                }
                            });
                    }
                });

        }
    }
}
