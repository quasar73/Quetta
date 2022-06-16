import { RequestsListDialogComponent } from './requests-list-dialog/requests-list-dialog.component';
import { AddChatDialogComponent } from './add-chat-dialog/add-chat-dialog.component';
import { FormControl } from '@angular/forms';
import { ChangeDetectionStrategy, Component, Inject, Injector, OnInit } from '@angular/core';
import { UserInfo } from 'src/app/shared/models/user-info.model';
import { AuthenticationService } from 'src/app/shared/services/auth/authentication.service';
import { TuiDialogService } from '@taiga-ui/core';
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
        new PolymorpheusComponent(RequestsListDialogComponent, this.injector),
        {
            dismissible: true,
            closeable: false,
        }
    );

    get userFullName(): string {
        return `${this.userInfo?.firstName}  ${this.userInfo?.lastName}`;
    }

    get username(): string {
        return `@${this.userInfo?.username}`;
    }

    constructor(
        private authService: AuthenticationService,
        @Inject(TuiDialogService) private readonly dialogService: TuiDialogService,
        @Inject(Injector) private readonly injector: Injector
    ) {}

    ngOnInit(): void {
        this.authService.getUserInfo().subscribe(info => {
            this.userInfo = info;
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
                console.log(username);
            },
        });
    }

    openRequestsListDialog(): void {
        this.requestsListDialog.subscribe();
    }
}
