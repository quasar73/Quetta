import { MessageModel } from './../../shared/api-models/message.model';
import { SelectedChatService } from './../../shared/services/selected-chat/selected-chat.service';
import { ActivatedRoute } from '@angular/router';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MessageApiService } from 'src/app/shared/services/api/message/message.service';

@Component({
    selector: 'qtt-chat',
    templateUrl: './chat.component.html',
    styleUrls: ['./chat.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatComponent implements OnInit {
    messages!: MessageModel[];
    chatId!: string | null;

    constructor(
        private readonly activatedRoute: ActivatedRoute,
        private readonly selectedChatService: SelectedChatService,
        private readonly messagesApiService: MessageApiService,
        private readonly cdr: ChangeDetectorRef
    ) {}

    ngOnInit(): void {
        this.chatId = this.activatedRoute.snapshot.paramMap.get('id');
        this.selectedChatService.setId(this.chatId);
        if (this.chatId) {
            this.messagesApiService.getMessages(this.chatId).subscribe(messages => {
                this.messages = [...(messages ?? [])];
                this.cdr.markForCheck();
            });
        }
    }

    onMessageSent(message: MessageModel): void {
        this.messages = [message, ...this.messages];
        this.cdr.markForCheck();
    }
}
