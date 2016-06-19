namespace Allors {
    export class DatabaseError extends AllorsError {
        constructor(context: Context, public reponseError: Data.ErrorResponse) {
            super(context);
        }
    }
}