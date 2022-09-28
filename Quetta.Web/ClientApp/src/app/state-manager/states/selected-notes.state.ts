import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext, StateToken } from '@ngxs/store';
import { SelectedNotes } from '../actions/selected-notes.actions';
import { SelectedNotesStateModel } from '../models/selected-notes.model';

const SELECTED_NOTES_TOKEN = new StateToken<SelectedNotesStateModel>('selectedNotes');

@State({
    name: SELECTED_NOTES_TOKEN,
    defaults: {
        ids: [],
    },
})
@Injectable()
export class SelectedNotesState {
    @Action(SelectedNotes.Select)
    selectMessage(ctx: StateContext<SelectedNotesStateModel>, action: SelectedNotes.Select): void {
        const state = ctx.getState();
        action.id === null
            ? null
            : ctx.setState({
                  ids: [...state.ids, action.id],
              });
    }

    @Action(SelectedNotes.Remove)
    removeMessage(ctx: StateContext<SelectedNotesStateModel>, action: SelectedNotes.Remove): void {
        const state = ctx.getState();
        action.id === null
            ? null
            : ctx.setState({
                  ids: [...state.ids.filter(id => id !== action.id)],
              });
    }

    @Action(SelectedNotes.Clear)
    clearMessages(ctx: StateContext<SelectedNotesStateModel>): void {
        ctx.setState({
            ids: [],
        });
    }

    @Selector()
    static ids(state: SelectedNotesStateModel): string[] | null {
        return state.ids;
    }
}
