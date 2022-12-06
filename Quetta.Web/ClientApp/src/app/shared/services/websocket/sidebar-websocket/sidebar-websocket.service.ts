import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { SidebarModel } from '@models/sidebar.model';
import { TokenStorage } from '@services/auth/token-storage.service';
import { delayWhen, from, map, Observable, Subject, tap } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

@Injectable({
    providedIn: 'root',
})
export class SidebarWebsocketService {
    private readonly sidebar$ = new Subject<SidebarModel>();
    private hubConnection!: HubConnection;

    constructor(private readonly tokenStorage: TokenStorage) {}

    startConnection(): Observable<void> {
        return this.tokenStorage.getAccessToken().pipe(
            tap(token => {
                this.hubConnection = new HubConnectionBuilder()
                    .withUrl(environment.serverUrl + 'sidebar', { accessTokenFactory: () => token })
                    .build();
            }),
            delayWhen(() =>
                from(
                    this.hubConnection
                        .start()
                        .then(() => console.log('Sidebar connection started.'))
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
            .then(() => console.log('Sidebar connection stopped.'))
            .catch(err => console.error('Error while stopping connection: ' + err));
    }

    addNotificationsListner(): void {
        this.hubConnection.on('NewMessage', sidebarModel => {
            this.sidebar$.next(sidebarModel);
        });
    }

    getSidebarObservable(): Observable<SidebarModel> {
        return this.sidebar$.asObservable();
    }
}
