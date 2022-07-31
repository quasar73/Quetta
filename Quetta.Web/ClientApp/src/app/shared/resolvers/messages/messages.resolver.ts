import { MessageApiService } from 'src/app/shared/services/api/message/message.service';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { MessageModel } from 'src/app/shared/api-models/message.model';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class MessagesResolver implements Resolve<MessageModel[] | null> {
    constructor(private readonly messageService: MessageApiService) {}

    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): MessageModel[] | Observable<MessageModel[] | null> | Promise<MessageModel[] | null> | MessageModel[] {
        const chatId = route.paramMap.get('id');

        if (chatId) {
            return this.messageService.getMessages(chatId);
        } else {
            return [];
        }
    }
}
