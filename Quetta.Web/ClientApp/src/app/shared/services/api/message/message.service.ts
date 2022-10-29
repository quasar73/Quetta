import { SendMessageModel } from './../../../api-models/send-message.model';
import { Observable } from 'rxjs';
import { BaseService } from './../base/base.service';
import { Injectable } from '@angular/core';
import { MessageModel } from '@api-models/message.model';

@Injectable({ providedIn: 'root' })
export class MessageApiService {
    constructor(private readonly base: BaseService) {}

    sendMessage(model: SendMessageModel): Observable<{ messageId: string } | null> {
        return this.base.post<{ messageId: string } | null>('messages', model);
    }

    deleteMessages(ids: string[]): Observable<void | null> {
        return this.base.delete<void>('messages', {
            messageIds: ids,
        });
    }

    getMessages(chatId: string | null, lastMessageId: string | null, amount: number): Observable<MessageModel[] | null> {
        return this.base.get<MessageModel[]>('messages', { amount, chatId: chatId ?? '', lastMessageId: lastMessageId ?? '' });
    }

    readMessage(messageId: string): Observable<void | null> {
        return this.base.get<void>('messages/read', { messageId });
    }
}
