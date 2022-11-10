import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ReadMessagesModel } from '@models/read-messages.model';
import { TokenStorage } from '@services/auth/token-storage.service';
import { delayWhen, from, map, Observable, Subject, tap } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

@Injectable({
    providedIn: 'root',
})
export class ReadWebsocketService {
    private readonly read$ = new Subject<ReadMessagesModel>();
    private hubConnection!: HubConnection;

    constructor(private readonly tokenStorage: TokenStorage) {}

    startConnection(): Observable<void> {
        return this.tokenStorage.getAccessToken().pipe(
            tap(token => {
                this.hubConnection = new HubConnectionBuilder()
                    .withUrl(environment.serverUrl + 'read', { accessTokenFactory: () => token })
                    .build();
            }),
            delayWhen(() =>
                from(
                    this.hubConnection
                        .start()
                        .then(() => console.log('Read connection started.'))
                        .catch(err => console.error('Error while starting connection: ' + err))
                )
            ),
            map(() => {
                return;
            })
        );
    }

    stopConnection(): void {
        this.hubConnection
            .stop()
            .then(() => console.log('Message connection stopped.'))
            .catch(err => console.error('Error while stopping connection: ' + err));
    }

    addNotificationsListner(): void {
        this.hubConnection.on('ReadMessages', readModel => {
            this.read$.next(readModel);
        });
    }

    addToGroup(chatId: string | null): void {
        if (chatId) {
            this.hubConnection.invoke('SetChat', chatId);
        }
    }

    getReadObservable(): Observable<ReadMessagesModel> {
        return this.read$.asObservable();
    }
}
