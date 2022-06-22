import { TuiSidebarModule } from '@taiga-ui/addon-mobile';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { AuthenticationService } from 'src/app/shared/services/auth/authentication.service';

import { SidebarHeaderComponent } from './sidebar-header.component';

describe('SidebarHeaderComponent', () => {
    let component: SidebarHeaderComponent;
    let fixture: ComponentFixture<SidebarHeaderComponent>;
    let mockAuthenticationService: any;

    beforeEach(async () => {
        mockAuthenticationService = jasmine.createSpyObj(['getUserInfo']);
        mockAuthenticationService.getUserInfo.and.returnValue(
            of({
                username: 'testUserName',
                firstName: 'TestFirstName',
                lastName: 'TestLastName',
            })
        );

        await TestBed.configureTestingModule({
            declarations: [SidebarHeaderComponent],
            imports: [TuiSidebarModule],
            providers: [{ provide: AuthenticationService, useValue: mockAuthenticationService }],
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
