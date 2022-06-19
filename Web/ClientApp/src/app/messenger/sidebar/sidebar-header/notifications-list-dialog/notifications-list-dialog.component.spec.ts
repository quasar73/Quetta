import { NotificationsListDialogComponent } from './notifications-list-dialog.component';
import { ComponentFixture, TestBed } from '@angular/core/testing';

describe('NotificationsListDialogComponent', () => {
    let component: NotificationsListDialogComponent;
    let fixture: ComponentFixture<NotificationsListDialogComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [NotificationsListDialogComponent],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(NotificationsListDialogComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
