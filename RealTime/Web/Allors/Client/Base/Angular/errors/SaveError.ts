namespace Allors {
    export class SaveError extends DatabaseError {
        constructor(context: Context, public saveResponse: Data.PushResponse) {
            super(context, saveResponse);
        }
    }
}