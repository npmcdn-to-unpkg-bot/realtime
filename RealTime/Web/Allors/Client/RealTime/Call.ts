namespace Allors.Domain {
   export interface Call {
       isCaller(person: Person): boolean;

       isCallee(person: Person): boolean;

       other(me: Person): Person;
   }

   extend(Call,
        {
            isCaller(person: Person): boolean {
                return this.Caller === person;
            },

            isCallee(person: Person): boolean {
                return this.Callee === person;
            },

            other(me: Person): Person {
                if (this.isCaller(me)) {
                    return this.Callee;
                }

                return this.Caller;
            }
        });
}