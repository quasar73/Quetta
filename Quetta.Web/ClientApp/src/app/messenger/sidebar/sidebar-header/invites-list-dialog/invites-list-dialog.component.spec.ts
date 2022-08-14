import { HttpClientTestingModule } from '@angular/common/http/testing';
import { InviteApiService } from './../../../../shared/services/api/invite/invite.service';
import { TuiScrollbarModule } from '@taiga-ui/core';
import { InvitesListDialogComponent } from './invites-list-dialog.component';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Component } from '@angular/core';

describe('InvitesListDialogComponent', () => {
    let component: InvitesListDialogComponent;
    let fixture: ComponentFixture<InvitesListDialogComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [InvitesListDialogComponent, MockInvitesListDialogComponent],
            imports: [TuiScrollbarModule, HttpClientTestingModule],
            providers: [InviteApiService],
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
});

@Component({
    selector: 'qtt-invites-item',
    template: '',
})
class MockInvitesListDialogComponent {}
