import { Subject, Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ChatUnreadService {
    private chats: { [key: string]: Subject<number> } = {};

    constructor() {}

    addChat(chatId: string): void {
        this.chats[chatId] = new Subject();
    }

    updateChat(amount: number, chatId: string): void {
        console.log(chatId in this.chats)
        if (chatId in this.chats) {
            return this.chats[chatId].next(amount);
        }
    }

    getChatAsObservable(chatId: string): Observable<number> | null {
        if (chatId in this.chats) {
            return this.chats[chatId].asObservable();
        }
        return null;
    }
}
