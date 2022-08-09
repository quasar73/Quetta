import { ChatInfoModel } from 'src/app/shared/api-models/chat-info.model';
import { ChatApiService } from './../../services/api/chat/chat.service';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ChatInfoResolver implements Resolve<ChatInfoModel | null> {
    constructor(private readonly chatService: ChatApiService) {}

    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): ChatInfoModel | null | Observable<ChatInfoModel | null> | Promise<ChatInfoModel | null> {
        const chatId = route.paramMap.get('id');

        if (chatId) {
            return this.chatService.getChatInfo(chatId);
        } else {
            return null;
        }
    }
}
