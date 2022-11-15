import { ClientMessageModel } from './../../models/client-message.model';
import { Observable, Subject } from 'rxjs';
import { Injectable } from '@angular/core';
import { MessageAddedModel } from '@models/message-added.model';

@Injectable({
    providedIn: 'root',
})
export class MessageUpdaterService {
    private readonly sentMessage$ = new Subject<ClientMessageModel | null>();
    private readonly addedMessage$ = new Subject<MessageAddedModel | null>();

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
