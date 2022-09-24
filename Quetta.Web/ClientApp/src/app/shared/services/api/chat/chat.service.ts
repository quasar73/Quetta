import { Observable } from 'rxjs';
import { BaseService } from './../base/base.service';
import { Injectable } from '@angular/core';
import { ChatItemModel } from '@api-models/chat-item.model';
import { ChatInfoModel } from '@api-models/chat-info.model';

@Injectable({
    providedIn: 'root',
})
export class ChatApiService {
    constructor(private readonly base: BaseService) {}

    getChatInfo(chatId: string): Observable<ChatInfoModel | null> {
        return this.base.get<ChatInfoModel>(`chats/info/${chatId}`);
    }

    getChats(): Observable<ChatItemModel[] | null> {
        return this.base.get<ChatItemModel[]>('chats');
    }
}
