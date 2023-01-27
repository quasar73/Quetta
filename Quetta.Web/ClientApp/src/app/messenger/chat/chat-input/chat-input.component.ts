import { SendMessageModel } from '@api-models/send-message.model';
import { MessageApiService } from '@api-services/message/message.service';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { ChangeDetectionStrategy, Component, Input, Output, EventEmitter } from '@angular/core';
import { AuthenticationService } from '@services/auth/authentication.service';
import { MessageStatus } from '@enums/message-status.enum';
import { ClientMessageModel } from '@models/client-message.model';
import { GuidService } from '@services/guid/guid.service';
import { MessageAddedModel } from '@models/message-added.model';

@Component({
    selector: 'qtt-chat-input',
    templateUrl: './chat-input.component.html',
    styleUrls: ['./chat-input.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatInputComponent {
    @Output() messageSent = new EventEmitter<ClientMessageModel>();
    @Output() messageAdded = new EventEmitter<MessageAddedModel>();

    @Input() chatId!: string | null;

    messageForm = new UntypedFormGroup({
        text: new UntypedFormControl('', [Validators.required, Validators.maxLength(2000)]),
    });

    constructor(
        private readonly messageApiService: MessageApiService,
        private readonly authService: AuthenticationService,
        private readonly guidService: GuidService
    ) {}

    sendMessage(): void {
        if (this.chatId) {
            const message: SendMessageModel = {
                text: this.messageForm.get('text')?.value,
                chatId: this.chatId,
            };

            const code = this.guidService.getValue().toString();

            this.authService.getUserInfo().subscribe(info => {
                this.messageSent.emit({
                    id: null,
                    username: info.username,
                    text: this.messageForm.get('text')?.value,
                    date: new Date().toString(),
                    status: MessageStatus.Pending,
                    isSelected: false,
                    isSupported: true,
                    readers: [],
                    isOwner: true,
                    code,
                });
            });

            this.messageForm.reset();
            this.messageApiService.sendMessage(message).subscribe(result => {
                if (result?.messageId) {
                    this.messageAdded.emit({ code, messageId: result.messageId });
                }
            });
        }
    }
}
