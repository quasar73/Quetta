import { MessageApiService } from './../../../shared/services/api/message/message.service';
import { ChatInfoModel } from '@api-models/chat-info.model';
import { SelectedNotes } from 'src/app/state-manager/actions/selected-notes.actions';
import { first, Observable } from 'rxjs';
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

    constructor(private readonly store: Store, private readonly messageApiService: MessageApiService) {}

    cancel(): void {
        this.store.dispatch(new SelectedNotes.Clear());
    }

    delete(): void {
        this.ids$.pipe(first()).subscribe(ids => {
            this.messageApiService.deleteMessages(ids).subscribe(() => {
                this.store.dispatch(new SelectedNotes.Clear());
            });
        });
    }
}
