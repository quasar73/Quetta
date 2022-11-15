import { ReadWebsocketService } from '@services/websocket/read-websocket/read-websocket.service';
import { MessageUpdaterService } from '@services/message-updater/message-updater.service';
import { ChatInfoModel } from '@api-models/chat-info.model';
import { MessageModel } from '@api-models/message.model';
import { combineLatestWith, Subscription } from 'rxjs';
import { AuthenticationService } from '@services/auth/authentication.service';
import { MessageWebsocketService } from '@services/websocket/message-websocket/message-websocket.service';
import { SelectedChatService } from '@services/selected-chat/selected-chat.service';
import { ActivatedRoute } from '@angular/router';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit, OnDestroy } from '@angular/core';
import { ClientMessageModel } from '@models/client-message.model';
import { MessageAddedModel } from '@models/message-added.model';
import { getStatus } from '@utils/status-calculator';

@Component({
    selector: 'qtt-chat',
    templateUrl: './chat.component.html',
    styleUrls: ['./chat.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatComponent implements OnInit, OnDestroy {
    messages!: ClientMessageModel[];
    chatInfo!: ChatInfoModel | null;
    chatId!: string | null;

    private readonly subscriptions = new Subscription();

    constructor(
        private readonly activatedRoute: ActivatedRoute,
        private readonly selectedChatService: SelectedChatService,
        private readonly messageWebsocketService: MessageWebsocketService,
        private readonly authService: AuthenticationService,
        private readonly cdr: ChangeDetectorRef,
        private readonly messageUpdaterService: MessageUpdaterService,
        private readonly readWebsocketService: ReadWebsocketService
    ) {}

    ngOnInit(): void {
        this.chatId = this.activatedRoute.snapshot.paramMap.get('id');
        this.selectedChatService.setId(this.chatId);

        if (this.chatId) {
            this.activatedRoute.data.subscribe(({ messages, chatInfo }) => {
                this.messages = [
                    ...(messages?.map((message: MessageModel) => {
                        return { ...message, code: undefined, isSelected: false, status: getStatus(message) };
                    }) ?? []),
                ];
                this.chatInfo = chatInfo;
                this.cdr.markForCheck();
            });

            this.subscriptions.add(
                this.messageWebsocketService.startConnection().subscribe(() => {
                    this.messageWebsocketService.addToGroup(this.chatId);
                    this.messageWebsocketService.addNotificationsListner();
                })
            );

            this.subscriptions.add(
                this.readWebsocketService.startConnection().subscribe(() => {
                    this.readWebsocketService.addToGroup(this.chatId);
                    this.readWebsocketService.addNotificationsListner();
                })
            );

            this.subscriptions.add(
                this.messageWebsocketService
                    .getMessagesObservable()
                    .pipe(combineLatestWith(this.authService.getUserInfo()))
                    .subscribe(([message, info]) => {
                        if (message.username !== info.username) {
                            this.onMessageSent({ ...message, code: undefined, isSelected: false, status: getStatus(message) });
                        }
                    })
            );
        }
    }

    onMessageSent(message: ClientMessageModel): void {
        this.messageUpdaterService.updateSentMessage(message);
    }

    onMessageAdded(model: MessageAddedModel): void {
        this.messageUpdaterService.updateAddedMessage(model);
    }

    ngOnDestroy(): void {
        this.messageWebsocketService.stopConnection();
        this.readWebsocketService.stopConnection();
        this.subscriptions.unsubscribe();
    }
}
