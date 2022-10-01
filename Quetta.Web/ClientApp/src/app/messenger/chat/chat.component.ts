import { ChatInfoModel } from '@api-models/chat-info.model';
import { MessageModel } from '@api-models/message.model';
import { combineLatestWith } from 'rxjs';
import { AuthenticationService } from '@services/auth/authentication.service';
import { MessageWebsocketService } from '@services/websocket/message-websocket/message-websocket.service';
import { SelectedChatService } from '@services/selected-chat/selected-chat.service';
import { ActivatedRoute } from '@angular/router';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ClientMessageModel } from '@models/client-message.model';
import { MessageStatus } from '@enums/message-status.enum';
import { MessageAddedModel } from '@models/message-added.model';

@Component({
    selector: 'qtt-chat',
    templateUrl: './chat.component.html',
    styleUrls: ['./chat.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatComponent implements OnInit {
    messages!: ClientMessageModel[];
    chatInfo!: ChatInfoModel | null;
    chatId!: string | null;

    constructor(
        private readonly activatedRoute: ActivatedRoute,
        private readonly selectedChatService: SelectedChatService,
        private readonly messageWebsocketService: MessageWebsocketService,
        private readonly authService: AuthenticationService,
        private readonly cdr: ChangeDetectorRef
    ) {}

    ngOnInit(): void {
        this.chatId = this.activatedRoute.snapshot.paramMap.get('id');
        this.selectedChatService.setId(this.chatId);

        if (this.chatId) {
            this.activatedRoute.data.subscribe(({ messages, chatInfo }) => {
                this.messages = [
                    ...(messages?.map((m: MessageModel) => {
                        return { ...m, code: undefined, isSelected: false };
                    }) ?? []),
                ];
                this.chatInfo = chatInfo;
                this.cdr.markForCheck();
            });

            this.messageWebsocketService.startConnection().subscribe(() => {
                this.messageWebsocketService.addToGroup(this.chatId);
                this.messageWebsocketService.addNotificationsListner();
            });

            this.messageWebsocketService
                .getMessagesObservable()
                .pipe(combineLatestWith(this.authService.getUserInfo()))
                .subscribe(([message, info]) => {
                    if (message.username !== info.username) {
                        this.onMessageSent({ ...message, code: undefined, isSelected: false });
                    }
                });
        }
    }

    onMessageSent(message: ClientMessageModel): void {
        this.messages = [message, ...this.messages];
        this.cdr.markForCheck();
    }

    onMessageAdded(model: MessageAddedModel): void {
        const index = this.messages.findIndex(m => m.code === model.code);
        this.messages[index] = { ...this.messages[index], status: MessageStatus.Unreaded, id: model.messageId };
        this.messages = [...this.messages];
        this.cdr.markForCheck();
    }
}
