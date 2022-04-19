import { Component, OnInit } from '@angular/core';
import { UserInfoModel } from 'src/app/shared/models/user-info.model';
import { AuthenticationService } from 'src/app/shared/services/auth/authentication.service';

@Component({
    selector: 'qtt-sidebar-header',
    templateUrl: './sidebar-header.component.html',
    styleUrls: ['./sidebar-header.component.scss'],
})
export class SidebarHeaderComponent implements OnInit {
    userInfo!: UserInfoModel;

    get userFullName(): string {
        return this.userInfo?.firstName + ' ' + this.userInfo?.lastName;
    }

    constructor(private authService: AuthenticationService) {}

    ngOnInit(): void {
        this.authService.getUserInfo().subscribe(info => {
            this.userInfo = info;
        });
    }
}
