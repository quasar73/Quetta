export namespace SelectedNotes {
    export class Select {
        static readonly type = '[Selected Notes] Select';

        constructor(public id: string) {}
    }

    export class Remove {
        static readonly type = '[Selected Notes] Remove';

        constructor(public id: string) {}
    }

    export class Clear {
        static readonly type = '[Selected Notes] Clear';

        constructor() {}
    }
}
