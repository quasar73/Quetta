import { ReactiveFormsModule } from '@angular/forms';
import { TuiInputModule, TuiMarkerIconModule } from '@taiga-ui/kit';
import { TranslocoTestingModule } from '@ngneat/transloco';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TuiSidebarModule } from '@taiga-ui/addon-mobile';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { AuthenticationService } from 'src/app/shared/services/auth/authentication.service';

import { SidebarHeaderComponent } from './sidebar-header.component';
import { TuiHintModule, TuiHostedDropdownModule } from '@taiga-ui/core';
import { InviteWebsocketService } from 'src/app/shared/services/websocket/invite-websocket/invite-websocket.service';
import { MockService } from 'ng-mocks';

describe('SidebarHeaderComponent', () => {
    let component: SidebarHeaderComponent;
    let fixture: ComponentFixture<SidebarHeaderComponent>;

    beforeEach(async () => {
        const mockAuthenticationService = MockService(AuthenticationService, {
            getUserInfo: () =>
                of({
                    username: 'testUserName',
                    firstName: 'TestFirstName',
                    lastName: 'TestLastName',
                }),
        });

        const mockInviteWebsocketService = MockService(InviteWebsocketService, {
            getInvitesObserver: () => of(true),
            startConnection: () => of(),
        });

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
                { provide: InviteWebsocketService, useValue: mockInviteWebsocketService },
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
