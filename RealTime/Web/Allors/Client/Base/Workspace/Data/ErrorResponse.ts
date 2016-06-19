namespace Allors.Data {
    export interface ErrorResponse {
        hasErrors: boolean;

        errorMessage?: string;

        versionErrors?: string[];

        accessErrors?: string[];

        missingErrors?: string[];

        derivationErrors?: Allors.Data.PullResponseDerivationError[];
    }
}