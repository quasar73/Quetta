import { Subject, Observable } from 'rxjs';
import { Injectable } from '@angular/core';

export interface ChatUnreadModel {
    amount: number;
    text: string | null;
}

@Injectable({ providedIn: 'root' })
export class ChatUnreadService {
    private chats: { [key: string]: Subject<ChatUnreadModel> } = {};

    constructor() {}

    addChat(chatId: string): void {
        this.chats[chatId] = new Subject();
    }

    updateChat(chatId: string, amount: number, text?: string): void {
        if (chatId in this.chats) {
            this.chats[chatId].next({ amount, text: text ?? null });
        }
    }

    getChatAsObservable(chatId: string): Observable<ChatUnreadModel> | null {
        if (chatId in this.chats) {
            return this.chats[chatId].asObservable();
        }
        return null;
    }
}
