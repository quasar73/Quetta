import { FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { UserInfo } from 'src/app/shared/models/user-info.model';
import { AuthenticationService } from 'src/app/shared/services/auth/authentication.service';

@Component({
    selector: 'qtt-sidebar-header',
    templateUrl: './sidebar-header.component.html',
    styleUrls: ['./sidebar-header.component.scss'],
})
export class SidebarHeaderComponent implements OnInit {
    userInfo!: UserInfo;
    searchControl = new FormControl();

    get userFullName(): string {
        return this.userInfo?.firstName + ' ' + this.userInfo?.lastName;
    }

    constructor(private authService: AuthenticationService) {}

    ngOnInit(): void {
        this.authService.getUserInfo().subscribe(info => {
            this.userInfo = info;
        });
    }

    logout(): void {
        this.authService.logout();
    }
}
