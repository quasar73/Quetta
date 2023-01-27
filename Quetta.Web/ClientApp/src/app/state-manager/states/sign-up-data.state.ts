import { Injectable } from '@angular/core';
import { Action, State, StateContext, StateToken } from '@ngxs/store';
import { SignUpData } from '../actions/sign-up-data.actions';
import { SignUpDataStateModel } from '../models/sign-up-data.model';

const SIGN_UP_DATA_TOKEN = new StateToken<SignUpDataStateModel | null>('signUpData');

@State({
    name: SIGN_UP_DATA_TOKEN,
    defaults: null,
})
@Injectable()
export class SignUpDataState {
    @Action(SignUpData.Set)
    setSignUpData(ctx: StateContext<SignUpDataStateModel>, action: SignUpData.Set): void {
        ctx.setState({
            ...action.model,
        });
    }

    @Action(SignUpData.Delete)
    deleteSignUpData(ctx: StateContext<SignUpDataStateModel | null>): void {
        ctx.setState(null);
    }
}
