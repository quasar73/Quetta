import { ClientMessageModel } from './../../models/client-message.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { MessageAddedModel } from '@models/message-added.model';

@Injectable({
    providedIn: 'root',
})
export class MessageUpdaterService {
    private readonly sentMessage$ = new BehaviorSubject<ClientMessageModel | null>(null);
    private readonly addedMessage$ = new BehaviorSubject<MessageAddedModel | null>(null);

    constructor() {}

    updateSentMessage(model: ClientMessageModel): void {
        this.sentMessage$.next(model);
    }

    updateAddedMessage(model: MessageAddedModel): void {
        this.addedMessage$.next(model);
    }

    getSentMessage(): Observable<ClientMessageModel | null> {
        return this.sentMessage$.asObservable();
    }

    getAddedMessage(): Observable<MessageAddedModel | null> {
        return this.addedMessage$.asObservable();
    }
}
