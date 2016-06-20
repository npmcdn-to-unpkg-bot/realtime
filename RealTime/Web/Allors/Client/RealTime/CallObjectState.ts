namespace Allors.Domain {
    const uniqueIds = {
        Requested: "4706152D-6CA8-4BC7-880D-981F2739CD26".toLowerCase(),
        Accepted: "A95FFD0F-113E-483B-9B37-CF56371AC518".toLowerCase(),
        Rejected: "FFA6492D-C648-488A-9D0B-2767FED7CD33".toLowerCase(),
        Aborted: "E51B1E64-7A87-49FB-B74A-75CB4CB91F59".toLowerCase(),
        Ended: "4706152D-6CA8-4BC7-880D-981F2739CD26".toLowerCase()
    }

    export interface CallObjectState {
        isRequested: boolean;
        isAccepted: boolean;
        isRejected: boolean;
        isAborted: boolean;
        isEnded: boolean;
    }

    extend(CallObjectState,
    {
        get isRequested(): boolean {
            return this.UniqueId && this.UniqueId.toLowerCase() === uniqueIds.Requested;
        },

        get isAccepted(): boolean {
            return this.UniqueId && this.UniqueId.toLowerCase() === uniqueIds.Accepted;
        },

        get isRejected(): boolean {
            return this.UniqueId && this.UniqueId.toLowerCase() === uniqueIds.Rejected;
        },

        get isAborted(): boolean {
            return this.UniqueId && this.UniqueId.toLowerCase() === uniqueIds.Aborted;
        },

        get isEnded(): boolean {
            return this.UniqueId && this.UniqueId.toLowerCase() === uniqueIds.Ended;
        }
    });
}