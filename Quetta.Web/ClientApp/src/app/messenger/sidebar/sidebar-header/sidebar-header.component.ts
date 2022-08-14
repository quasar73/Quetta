import { InvitesListDialogComponent } from './invites-list-dialog/invites-list-dialog.component';
import { TranslocoService } from '@ngneat/transloco';
import { HttpErrorResponse } from '@angular/common/http';
import { InviteApiService } from './../../../shared/services/api/invite/invite.service';
import { AddChatDialogComponent } from './add-chat-dialog/add-chat-dialog.component';
import { UntypedFormControl } from '@angular/forms';
import { ChangeDetectionStrategy, Component, Inject, OnInit } from '@angular/core';
import { UserInfo } from 'src/app/shared/models/user-info.model';
import { AuthenticationService } from 'src/app/shared/services/auth/authentication.service';
import { TuiAlertService, TuiDialogService, TuiNotification } from '@taiga-ui/core';
import { PolymorpheusComponent } from '@tinkoff/ng-polymorpheus';
import { InviteWebsocketService } from 'src/app/shared/services/websocket/invite-websocket/invite-websocket.service';

@Component({
    selector: 'qtt-sidebar-header',
    templateUrl: './sidebar-header.component.html',
    styleUrls: ['./sidebar-header.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SidebarHeaderComponent implements OnInit {
    userInfo!: UserInfo;
    searchControl = new UntypedFormControl();
    isOpened = false;

    private addChatDialog = this.dialogService.open<string | null>(new PolymorpheusComponent(AddChatDialogComponent), {
        dismissible: true,
        closeable: false,
    });
    private invitesListDialog = this.dialogService.open<void>(new PolymorpheusComponent(InvitesListDialogComponent), {
        dismissible: true,
        closeable: true,
        size: 'm',
    });

    get userFullName(): string {
        return `${this.userInfo?.firstName} ${this.userInfo?.lastName}`;
    }

    get username(): string {
        return `@${this.userInfo?.username}`;
    }

    constructor(
        private readonly authService: AuthenticationService,
        private readonly inviteApiService: InviteApiService,
        private readonly alertService: TuiAlertService,
        private readonly translocoService: TranslocoService,
        public readonly inviteWebsocketService: InviteWebsocketService,
        @Inject(TuiDialogService) private readonly dialogService: TuiDialogService
    ) {}

    ngOnInit(): void {
        this.authService.getUserInfo().subscribe(info => {
            this.userInfo = info;
        });

        this.inviteWebsocketService.startConnection().subscribe(() => {
            this.inviteWebsocketService.addNotificationsListner();
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
                    this.inviteApiService
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

    openInvitesListDialog(): void {
        this.invitesListDialog.subscribe();
    }
}
