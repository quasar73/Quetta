export namespace SelectedMessages {
    export class Select {
        static readonly type = '[Selected Messages] Select';

        constructor(public id: string) {}
    }

    export class Remove {
        static readonly type = '[Selected Messages] Remove';

        constructor(public id: string) {}
    }

    export class Clear {
        static readonly type = '[Selected Messages] Clear';

        constructor() {}
    }
}
