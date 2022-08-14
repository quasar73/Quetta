import { ChatInfoModel } from 'src/app/shared/api-models/chat-info.model';
import { MessageModel } from 'src/app/shared/api-models/message.model';
import { combineLatestWith } from 'rxjs';
import { AuthenticationService } from 'src/app/shared/services/auth/authentication.service';
import { MessageWebsocketService } from './../../shared/services/websocket/message-websocket/message-websocket.service';
import { SelectedChatService } from './../../shared/services/selected-chat/selected-chat.service';
import { ActivatedRoute } from '@angular/router';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ClientMessageModel } from 'src/app/shared/models/client-message.model';
import { MessageStatus } from 'src/app/shared/enums/message-status.enum';

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

    onMessageAdded(code: string): void {
        const index = this.messages.findIndex(m => m.code === code);
        this.messages[index] = { ...this.messages[index], status: MessageStatus.Unreaded };
        this.messages = [...this.messages];
        this.cdr.markForCheck();
    }
}
