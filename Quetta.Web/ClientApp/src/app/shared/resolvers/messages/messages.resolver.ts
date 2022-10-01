import { MessageApiService } from '@api-services/message/message.service';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { MessageModel } from '@api-models/message.model';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class MessagesResolver implements Resolve<MessageModel[] | null> {
    constructor(private readonly messageService: MessageApiService) {}

    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): MessageModel[] | Observable<MessageModel[] | null> | Promise<MessageModel[] | null> {
        const chatId = route.paramMap.get('id');

        if (chatId) {
            return this.messageService.getMessages(chatId, null, 10);
        } else {
            return [];
        }
    }
}
