import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { MessageModel } from 'src/app/shared/api-models/message.model';
import { AuthenticationService } from 'src/app/shared/services/auth/authentication.service';
import { MessageStatus } from 'src/app/shared/enums/message-status.enum';

@Component({
    selector: 'qtt-note',
    templateUrl: './note.component.html',
    styleUrls: ['./note.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NoteComponent {
    @Output() messageCopy = new EventEmitter<string>();

    @Input() message!: MessageModel;

    constructor(private readonly authService: AuthenticationService) {}

    isUserOwner(): Observable<boolean> {
        return this.authService.getUserInfo().pipe(
            map(info => {
                return info.username === this.message.username;
            })
        );
    }

    getStatus(): string {
        switch (this.message.status) {
            case MessageStatus.Pending:
                return 'tuiIconTime';
            case MessageStatus.Unreaded:
                return 'tuiIconCheck';
            default:
                return 'tuiIconEyeOpen';
        }
    }

    copyText(): void {
        this.messageCopy.emit(this.message.text);
    }
}
