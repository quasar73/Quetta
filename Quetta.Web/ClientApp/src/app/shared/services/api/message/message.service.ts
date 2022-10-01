import { SendMessageModel } from './../../../api-models/send-message.model';
import { Observable } from 'rxjs';
import { BaseService } from './../base/base.service';
import { Injectable } from '@angular/core';
import { MessageModel } from '@api-models/message.model';

@Injectable({ providedIn: 'root' })
export class MessageApiService {
    constructor(private readonly base: BaseService) {}

    sendMessage(model: SendMessageModel): Observable<void | null> {
        return this.base.post<void | null>('messages', model);
    }

    deleteMessages(ids: string[]): Observable<void | null> {
        return this.base.delete<void | null>('messages', {
            messageIds: ids,
        });
    }

    getMessages(chatId: string | null, lastMessageId: string | null, amount: number): Observable<MessageModel[] | null> {
        return this.base.get<MessageModel[] | null>('messages', { amount, chatId: chatId ?? '', lastMessageId: lastMessageId ?? '' });
    }
}
