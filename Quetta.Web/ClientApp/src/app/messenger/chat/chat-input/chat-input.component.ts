import { MessageModel } from 'src/app/shared/api-models/message.model';
import { SendMessageModel } from './../../../shared/api-models/send-message.model';
import { MessageApiService } from './../../../shared/services/api/message/message.service';
import { UntypedFormGroup, UntypedFormControl } from '@angular/forms';
import { ChangeDetectionStrategy, Component, Input, Output, EventEmitter } from '@angular/core';
import { AuthenticationService } from 'src/app/shared/services/auth/authentication.service';
import { MessageStatus } from 'src/app/shared/enums/message-status.enum';

@Component({
    selector: 'qtt-chat-input',
    templateUrl: './chat-input.component.html',
    styleUrls: ['./chat-input.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatInputComponent {
    @Output() messageSent = new EventEmitter<MessageModel>();

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

            this.authService.getUserInfo().subscribe(info => {
                this.messageSent.emit({
                    username: info.username,
                    text: this.messageForm.get('text')?.value,
                    date: new Date().toLocaleString(),
                    status: MessageStatus.Pending,
                });
            });

            this.messageApiService.sendMessage(message).subscribe(() => {
                this.messageForm.reset();
            });
        }
    }
}
