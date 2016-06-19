namespace Allors.Data {
    export interface InvokeResponse extends Allors.Data.ErrorResponse {
        hasErrors: boolean;
        errorMessage?: string;
        versionErrors?: string[];
        accessErrors?: string[];
        missingErrors?: string[];
        derivationErrors?: Allors.Data.PullResponseDerivationError[];
    }
}