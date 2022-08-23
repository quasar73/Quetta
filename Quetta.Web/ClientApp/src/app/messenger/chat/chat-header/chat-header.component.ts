import { ChatInfoModel } from 'src/app/shared/api-models/chat-info.model';
import { SelectedNotes } from 'src/app/state-manager/actions/selected-notes.actions';
import { Observable } from 'rxjs';
import { SelectedNotesState } from './../../../state-manager/states/selected-notes.state';
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Select, Store } from '@ngxs/store';

@Component({
    selector: 'qtt-chat-header',
    templateUrl: './chat-header.component.html',
    styleUrls: ['./chat-header.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatHeaderComponent {
    @Input() chatInfo!: ChatInfoModel | null;

    @Select(SelectedNotesState.ids) ids$!: Observable<string[]>;

    constructor(private readonly store: Store) {}

    cancel(): void {
        this.store.dispatch(new SelectedNotes.Clear());
    }
}
