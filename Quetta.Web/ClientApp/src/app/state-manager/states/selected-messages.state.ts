import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext, StateToken } from '@ngxs/store';
import { SelectedMessages } from '../actions/selected-messages.actions';
import { SelectedMessagesModel } from '../models/selected-messages.model';

const SELECTED_MESSAGES_TOKEN = new StateToken<SelectedMessagesModel>('selectedMessages');

@State({
    name: SELECTED_MESSAGES_TOKEN,
    defaults: {
        ids: [],
    },
})
@Injectable()
export class SelectedMessagesState {
    @Action(SelectedMessages.Select)
    selectMessage(ctx: StateContext<SelectedMessagesModel>, action: SelectedMessages.Select): void {
        const state = ctx.getState();
        ctx.setState({
            ids: [...state.ids, action.id],
        });
    }

    @Action(SelectedMessages.Remove)
    removeMessage(ctx: StateContext<SelectedMessagesModel>, action: SelectedMessages.Remove): void {
        const state = ctx.getState();
        ctx.setState({
            ids: [...state.ids.filter(id => id !== action.id)],
        });
    }

    @Action(SelectedMessages.Clear)
    clearMessages(ctx: StateContext<SelectedMessagesModel>): void {
        ctx.setState({
            ids: [],
        });
    }

    @Selector()
    static ids(state: SelectedMessagesModel): string[] | null {
        return state.ids;
    }
}
