import { MessageModel } from '@api-models/message.model';
import { delayWhen, map, tap } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { from, Observable, Subject } from 'rxjs';
import { TokenStorage } from '../../auth/token-storage.service';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})
export class MessageWebsocketService {
    private readonly messages$ = new Subject<MessageModel>();
    private hubConnection!: HubConnection;

    constructor(private readonly tokenStorage: TokenStorage) {}

    startConnection(): Observable<void> {
        return this.tokenStorage.getAccessToken().pipe(
            tap(token => {
                this.hubConnection = new HubConnectionBuilder()
                    .withUrl(environment.serverUrl + 'message', { accessTokenFactory: () => token })
                    .build();
            }),
            delayWhen(() =>
                from(
                    this.hubConnection
                        .start()
                        .then(() => console.log('Message connection started.'))
                        .catch(err => console.error('Error while starting connection: ' + err))
                )
            ),
            map(() => {
                return;
            })
        );
    }

    addNotificationsListner(): void {
        this.hubConnection.on('NewMessage', message => {
            this.messages$.next(message);
        });
    }

    addToGroup(chatId: string | null): void {
        if (chatId) {
            this.hubConnection.invoke('SetChat', chatId);
        }
    }

    getMessagesObservable(): Observable<MessageModel> {
        return this.messages$.asObservable();
    }
}
