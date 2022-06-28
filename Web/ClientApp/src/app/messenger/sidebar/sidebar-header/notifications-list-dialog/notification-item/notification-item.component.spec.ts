import { NotificationItemComponent } from './notification-item.component';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TuiIslandModule } from '@taiga-ui/kit';

describe('NotificationItemComponent', () => {
    let component: NotificationItemComponent;
    let fixture: ComponentFixture<NotificationItemComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [NotificationItemComponent],
            imports: [TuiIslandModule],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(NotificationItemComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
