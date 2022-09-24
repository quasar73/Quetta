import { SignUpDataStateModel } from '../models/sign-up-data.model';

export namespace SignUpData {
    export class Set {
        static readonly type = '[Sign Up Data] Set';

        constructor(public model: SignUpDataStateModel) {}
    }

    export class Delete {
        static readonly type = '[Sign Up Data] Delete';

        constructor() {}
    }
}
