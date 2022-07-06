import { NotificationWebsocketService } from './../../../shared/services/websocket/notification-websocket/notification-websocket.service';
import { InvitesListDialogComponent } from './invites-list-dialog/invites-list-dialog.component';
import { TranslocoService } from '@ngneat/transloco';
import { HttpErrorResponse } from '@angular/common/http';
import { InviteService } from './../../../shared/services/api/invite/invite.service';
import { AddChatDialogComponent } from './add-chat-dialog/add-chat-dialog.component';
import { FormControl } from '@angular/forms';
import { ChangeDetectionStrategy, Component, Inject, Injector, OnInit } from '@angular/core';
import { UserInfo } from 'src/app/shared/models/user-info.model';
import { AuthenticationService } from 'src/app/shared/services/auth/authentication.service';
import { TuiAlertService, TuiDialogService, TuiNotification } from '@taiga-ui/core';
import { PolymorpheusComponent } from '@tinkoff/ng-polymorpheus';

@Component({
    selector: 'qtt-sidebar-header',
    templateUrl: './sidebar-header.component.html',
    styleUrls: ['./sidebar-header.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SidebarHeaderComponent implements OnInit {
    userInfo!: UserInfo;
    searchControl = new FormControl();
    isOpened = false;

    private addChatDialog = this.dialogService.open<string | null>(new PolymorpheusComponent(AddChatDialogComponent, this.injector), {
        dismissible: true,
        closeable: false,
    });
    private requestsListDialog = this.dialogService.open<string | null>(
        new PolymorpheusComponent(InvitesListDialogComponent, this.injector),
        {
            dismissible: true,
            closeable: true,
            size: 'l',
        }
    );

    get userFullName(): string {
        return `${this.userInfo?.firstName} ${this.userInfo?.lastName}`;
    }

    get username(): string {
        return `@${this.userInfo?.username}`;
    }

    constructor(
        private readonly authService: AuthenticationService,
        private readonly inviteService: InviteService,
        private readonly alertService: TuiAlertService,
        private readonly translocoService: TranslocoService,
        public readonly notificationWebsocketService: NotificationWebsocketService,
        @Inject(TuiDialogService) private readonly dialogService: TuiDialogService,
        @Inject(Injector) private readonly injector: Injector
    ) {}

    ngOnInit(): void {
        this.authService.getUserInfo().subscribe(info => {
            this.userInfo = info;
        });

        this.notificationWebsocketService.startConnection().subscribe(() => {
            this.notificationWebsocketService.addNotificationsListner();
        });
    }

    logout(): void {
        this.authService.logout();
    }

    toggle(open: boolean) {
        this.isOpened = open;
    }

    openAddChatDialog(): void {
        this.addChatDialog.subscribe({
            next: (username: string | null) => {
                if (username) {
                    this.inviteService
                        .sendInvite({
                            receiverUsername: username,
                            isGroupChat: false,
                            chatId: null,
                        })
                        .subscribe({
                            next: () => {
                                this.alertService
                                    .open(this.translocoService.translate<string>('messenger.invite.alert.success.message', { username }), {
                                        label: this.translocoService.translate<string>('messenger.invite.alert.success.title'),
                                        status: TuiNotification.Success,
                                    })
                                    .subscribe();
                            },
                            error: (response: HttpErrorResponse): void => {
                                if (response.status === 404) {
                                    this.alertService
                                        .open(this.translocoService.translate<string>('messenger.invite.alert.notFound'), {
                                            status: TuiNotification.Warning,
                                        })
                                        .subscribe();
                                } else if (response.status === 409) {
                                    this.alertService
                                        .open(this.translocoService.translate<string>('messenger.invite.alert.alreadyInvited'), {
                                            status: TuiNotification.Warning,
                                        })
                                        .subscribe();
                                }
                            },
                        });
                }
            },
        });
    }

    openRequestsListDialog(): void {
        this.requestsListDialog.subscribe();
    }
}
