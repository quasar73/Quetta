import { tap, map } from 'rxjs/operators';
import { NotificationService } from './../../api/notification/notification.service';
import { TuiAlertService, TuiNotification } from '@taiga-ui/core';
import { BehaviorSubject, combineLatestWith, Observable } from 'rxjs';
import { environment } from './../../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { TokenStorage } from '../../auth/token-storage.service';

@Injectable({
    providedIn: 'root',
})
export class NotificationWebsocketService {
    private hubConnection!: HubConnection;
    private notifications$ = new BehaviorSubject<boolean>(false);

    constructor(
        private readonly tokenStorage: TokenStorage,
        private readonly alertService: TuiAlertService,
        private readonly notificationService: NotificationService
    ) {}

    startConnection(): Observable<void> {
        return this.tokenStorage.getAccessToken().pipe(
            combineLatestWith(this.notificationService.isAnyNotifications()),
            tap(([token, isAnyNotifications]) => {
                if (isAnyNotifications) {
                    this.notifications$.next(isAnyNotifications?.hasNotifications);
                }

                this.hubConnection = new HubConnectionBuilder()
                    .withUrl(environment.serverUrl + 'notification', { accessTokenFactory: () => token })
                    .build();

                this.hubConnection
                    .start()
                    .then(() => console.log('Notification connection started.'))
                    .catch(err => console.error('Error while starting connection: ' + err));
            }),
            map(() => {
                return;
            })
        );
    }

    addNotificationsListner(): void {
        this.hubConnection.on('Notify', isAnyNotifications => {
            this.notifications$.next(isAnyNotifications);
            if (isAnyNotifications) {
                this.alertService.open('New notification', { status: TuiNotification.Info }).subscribe();
            }
        });
    }

    getNotificationsObserver(): Observable<boolean> {
        return this.notifications$.asObservable();
    }

    updateNotificationsStatus(hasNotifications: boolean): void {
        this.notifications$.next(hasNotifications);
    }
}
