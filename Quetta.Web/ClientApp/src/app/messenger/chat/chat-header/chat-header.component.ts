import { SelectedMessages } from 'src/app/state-manager/actions/selected-messages.actions';
import { Observable } from 'rxjs';
import { SelectedMessagesState } from './../../../state-manager/states/selected-messages.state';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Select, Store } from '@ngxs/store';

@Component({
    selector: 'qtt-chat-header',
    templateUrl: './chat-header.component.html',
    styleUrls: ['./chat-header.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatHeaderComponent {
    @Select(SelectedMessagesState.ids) ids$!: Observable<string[]>;

    constructor(private readonly store: Store) {}

    cancel(): void {
        this.store.dispatch(new SelectedMessages.Clear());
    }
}
