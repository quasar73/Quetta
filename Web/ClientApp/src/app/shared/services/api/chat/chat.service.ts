import { Observable } from 'rxjs';
import { BaseService } from './../base/base.service';
import { Injectable } from '@angular/core';
import { ChatItemModel } from 'src/app/shared/api-models/chat-item.model';

@Injectable({
    providedIn: 'root',
})
export class ChatService {
    constructor(private readonly base: BaseService) {}

    getChats(): Observable<ChatItemModel[] | null> {
        return this.base.get<ChatItemModel[]>('chat');
    }
}
