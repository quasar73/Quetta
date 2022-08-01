import { state, trigger, style, transition, animate } from '@angular/animations';
import { ClientMessageModel } from 'src/app/shared/models/client-message.model';
import { map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output, OnChanges, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/shared/services/auth/authentication.service';
import { MessageStatus } from 'src/app/shared/enums/message-status.enum';
import { FormControl } from '@angular/forms';

@Component({
    selector: 'qtt-note',
    templateUrl: './note.component.html',
    styleUrls: ['./note.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: [
        trigger('selection', [
            state('expanded', style({ width: '24px', opacity: 1 })),
            state('collapsed', style({ width: '0px', opacity: 0, margin: 0 })),
            transition('collapsed<=>expanded', [animate('0.2s')]),
        ]),
    ],
})
export class NoteComponent implements OnInit, OnChanges {
    @Output() messageCopy = new EventEmitter<string>();
    @Output() messageSelected = new EventEmitter<boolean>();

    @Input() message!: ClientMessageModel;
    @Input() isSelectionMode!: boolean;

    readonly selectControl = new FormControl<boolean>(false);

    constructor(private readonly authService: AuthenticationService) {}

    ngOnInit(): void {
        this.selectControl.valueChanges.pipe(tap(value => this.messageSelected.emit(value ?? false))).subscribe();
    }

    ngOnChanges(): void {
        this.selectControl.setValue(this.message.isSelected);
    }

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

    select(): void {
        this.messageSelected.emit(true);
    }
}