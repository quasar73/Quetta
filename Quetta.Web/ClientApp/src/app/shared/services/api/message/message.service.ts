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

    getMessages(chatId: string): Observable<MessageModel[] | null> {
        return this.base.get<MessageModel[] | null>('messages', { chatId });
    }
}
