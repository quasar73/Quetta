import { MockProvider } from 'ng-mocks';
import { of, EMPTY } from 'rxjs';
import { By } from '@angular/platform-browser';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { InviteApiService } from './../../../../shared/services/api/invite/invite.service';
import { TuiScrollbarModule } from '@taiga-ui/core';
import { InvitesListDialogComponent } from './invites-list-dialog.component';
import { ComponentFixture, TestBed } from '@angular/core/testing';

describe('InvitesListDialogComponent', () => {
    let component: InvitesListDialogComponent;
    let fixture: ComponentFixture<InvitesListDialogComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [InvitesListDialogComponent],
            imports: [TuiScrollbarModule, HttpClientTestingModule],
            providers: [
                MockProvider(InviteApiService, {
                    getInvites: () => of([]),
                    declineInvite: () => EMPTY,
                    acceptInvite: () => EMPTY,
                }),
            ],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(InvitesListDialogComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should shows empty message', () => {
        const emptyMessage = fixture.debugElement.query(By.css('.empty-list-message'));

        expect(emptyMessage).not.toBeNull();
    });

    it('should to decline invites', () => {
        component.onInviteDeclined('invite-id');

        expect(component.removedInvites).toEqual(['invite-id']);
    });

    it('should to accept invites', () => {
        component.onInviteAccepted('invite-id');

        expect(component.removedInvites).toEqual(['invite-id']);
    });
});
