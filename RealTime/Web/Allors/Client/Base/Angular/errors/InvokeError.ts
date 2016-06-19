namespace Allors {
    export class InvokeError extends DatabaseError {
        constructor(context: Context, public invokeResponse: Data.InvokeResponse) {
            super(context, invokeResponse);
        }
    }
}