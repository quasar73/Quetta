import { TuiScrollbarModule } from '@taiga-ui/core';
import { NotificationsListDialogComponent } from './notifications-list-dialog.component';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Component } from '@angular/core';

describe('NotificationsListDialogComponent', () => {
    let component: NotificationsListDialogComponent;
    let fixture: ComponentFixture<NotificationsListDialogComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [NotificationsListDialogComponent, MockNotificationItemComponent],
            imports: [TuiScrollbarModule],
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

@Component({
    selector: 'qtt-notification-item',
    template: '',
})
class MockNotificationItemComponent {}
