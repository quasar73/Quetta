import { SendMessageModel } from './../../../shared/api-models/send-message.model';
import { MessageApiService } from './../../../shared/services/api/message/message.service';
import { UntypedFormGroup, UntypedFormControl } from '@angular/forms';
import { ChangeDetectionStrategy, Component, Input, Output, EventEmitter } from '@angular/core';
import { AuthenticationService } from 'src/app/shared/services/auth/authentication.service';
import { MessageStatus } from 'src/app/shared/enums/message-status.enum';
import { Guid } from 'guid-typescript';
import { ClientMessageModel } from 'src/app/shared/models/client-message.model';

@Component({
    selector: 'qtt-chat-input',
    templateUrl: './chat-input.component.html',
    styleUrls: ['./chat-input.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatInputComponent {
    @Output() messageSent = new EventEmitter<ClientMessageModel>();
    @Output() messageAdded = new EventEmitter<string>();

    @Input() chatId!: string | null;

    messageForm = new UntypedFormGroup({
        text: new UntypedFormControl(''),
    });

    constructor(private readonly messageApiService: MessageApiService, private readonly authService: AuthenticationService) {}

    sendMessage(): void {
        if (this.chatId) {
            const message = {
                text: this.messageForm.get('text')?.value,
                chatId: this.chatId,
            } as SendMessageModel;
            const code = Guid.create().toString();

            this.authService.getUserInfo().subscribe(info => {
                this.messageSent.emit({
                    username: info.username,
                    text: this.messageForm.get('text')?.value,
                    date: new Date().toString(),
                    status: MessageStatus.Pending,
                    code,
                });
            });

            this.messageApiService.sendMessage(message).subscribe(() => {
                this.messageForm.reset();
                this.messageAdded.emit(code);
            });
        }
    }
}
