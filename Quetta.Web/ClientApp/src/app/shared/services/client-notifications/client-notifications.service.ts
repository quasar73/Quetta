import { Title } from '@angular/platform-browser';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ClientNotificationsService {
    private chats: { [key: string]: number } = {};

    constructor(private readonly title: Title) {}

    updateAmount(chatId: string, amount: number): void {
        this.chats[chatId] = amount;
        let total = 0;
        Object.keys(this.chats).forEach(key => {
            total += this.chats[key];
        });

        this.title.setTitle(total > 0 ? `(${total}) Quetta` : 'Quetta');
    }
}
