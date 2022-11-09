import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { TokenStorage } from '@services/auth/token-storage.service';
import { delayWhen, from, map, Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

@Injectable({
    providedIn: 'root',
})
export class ReadWebsocketService {
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
        this.hubConnection.on('ReadMessages', messageIds => {
            console.log(messageIds);
            // this.messages$.next(message);
        });
    }

    addToGroup(chatId: string | null): void {
        if (chatId) {
            this.hubConnection.invoke('SetChat', chatId);
        }
    }
}
