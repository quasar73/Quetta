import { ReactiveFormsModule } from '@angular/forms';
import { TuiInputModule, TuiMarkerIconModule } from '@taiga-ui/kit';
import { NotificationWebsocketService } from './../../../shared/services/websocket/notification-websocket/notification-websocket.service';
import { TranslocoTestingModule } from '@ngneat/transloco';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TuiSidebarModule } from '@taiga-ui/addon-mobile';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { AuthenticationService } from 'src/app/shared/services/auth/authentication.service';

import { SidebarHeaderComponent } from './sidebar-header.component';
import { TuiHintModule, TuiHostedDropdownModule } from '@taiga-ui/core';

describe('SidebarHeaderComponent', () => {
    let component: SidebarHeaderComponent;
    let fixture: ComponentFixture<SidebarHeaderComponent>;
    let mockAuthenticationService: any;
    let mockNotificationWebsocketService: any;

    beforeEach(async () => {
        mockAuthenticationService = jasmine.createSpyObj(['getUserInfo']);
        mockAuthenticationService.getUserInfo.and.returnValue(
            of({
                username: 'testUserName',
                firstName: 'TestFirstName',
                lastName: 'TestLastName',
            })
        );

        mockNotificationWebsocketService = jasmine.createSpyObj(['getNotificationsObserver', 'addNotificationsListner', 'startConnection']);
        mockNotificationWebsocketService.getNotificationsObserver.and.returnValue(of(true));
        mockNotificationWebsocketService.startConnection.and.returnValue(of(true));

        await TestBed.configureTestingModule({
            declarations: [SidebarHeaderComponent],
            imports: [
                TuiSidebarModule,
                HttpClientTestingModule,
                TranslocoTestingModule,
                TuiInputModule,
                TuiHostedDropdownModule,
                TuiMarkerIconModule,
                TuiHintModule,
                ReactiveFormsModule,
            ],
            providers: [
                { provide: AuthenticationService, useValue: mockAuthenticationService },
                { provide: NotificationWebsocketService, useValue: mockNotificationWebsocketService },
            ],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(SidebarHeaderComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should returns right user fullname', () => {
        expect(component.userFullName).toBe('TestFirstName TestLastName');
    });

    it('should returns right username', () => {
        expect(component.username).toBe('@testUserName');
    });
});
