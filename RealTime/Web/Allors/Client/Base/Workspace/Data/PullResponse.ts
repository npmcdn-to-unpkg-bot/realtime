namespace Allors.Data {
    export interface PullResponse {
        userSecurityHash: string;

        objects?: string[][];

        namedObjects?: { [id: string]: string; };
        namedCollections?: { [id: string]: string[]; };
        namedValues?: { [id: string]: any; };
    }
}