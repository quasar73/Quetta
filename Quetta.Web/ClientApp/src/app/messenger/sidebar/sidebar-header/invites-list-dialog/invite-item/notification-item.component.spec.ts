import { InviteItemComponent } from './invite-item.component';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TuiIslandModule } from '@taiga-ui/kit';

describe('InviteItemComponent', () => {
    let component: InviteItemComponent;
    let fixture: ComponentFixture<InviteItemComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [InviteItemComponent],
            imports: [TuiIslandModule],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(InviteItemComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
