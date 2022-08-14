import { tap, map } from 'rxjs/operators';
import { TuiAlertService, TuiNotification } from '@taiga-ui/core';
import { BehaviorSubject, combineLatestWith, Observable } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { TokenStorage } from '../../auth/token-storage.service';
import { InviteApiService } from '../../api/invite/invite.service';

@Injectable({
    providedIn: 'root',
})
export class InviteWebsocketService {
    private readonly invites$ = new BehaviorSubject<boolean>(false);
    private hubConnection!: HubConnection;

    constructor(
        private readonly tokenStorage: TokenStorage,
        private readonly alertService: TuiAlertService,
        private readonly inviteApiService: InviteApiService
    ) {}

    startConnection(): Observable<void> {
        return this.tokenStorage.getAccessToken().pipe(
            combineLatestWith(this.inviteApiService.isAnyInvites()),
            tap(([token, isAnyInvites]) => {
                if (isAnyInvites) {
                    this.invites$.next(isAnyInvites?.hasInvites);
                }

                this.hubConnection = new HubConnectionBuilder()
                    .withUrl(environment.serverUrl + 'invite', { accessTokenFactory: () => token })
                    .build();

                this.hubConnection
                    .start()
                    .then(() => console.log('Invite connection started.'))
                    .catch(err => console.error('Error while starting connection: ' + err));
            }),
            map(() => {
                return;
            })
        );
    }

    addNotificationsListner(): void {
        this.hubConnection.on('Notify', isAnyNotifications => {
            this.invites$.next(isAnyNotifications);
            if (isAnyNotifications) {
                this.alertService.open('New invite', { status: TuiNotification.Info }).subscribe();
            }
        });
    }

    getNotificationsObserver(): Observable<boolean> {
        return this.invites$.asObservable();
    }

    updateInvitessStatus(hasInvites: boolean): void {
        this.invites$.next(hasInvites);
    }
}
