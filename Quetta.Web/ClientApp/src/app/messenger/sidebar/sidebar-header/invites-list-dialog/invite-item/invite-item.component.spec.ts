import { getTranslocoModule } from 'src/app/translate/transloco-testing.module';
import { By } from '@angular/platform-browser';
import { InviteItemComponent } from './invite-item.component';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TuiIslandModule } from '@taiga-ui/kit';
import { testInviteModel } from 'src/app/testing/data/invite';

describe('InviteItemComponent', () => {
    let component: InviteItemComponent;
    let fixture: ComponentFixture<InviteItemComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [InviteItemComponent],
            imports: [TuiIslandModule, getTranslocoModule()],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(InviteItemComponent);
        component = fixture.componentInstance;
        component.invite = testInviteModel;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should shows sender username', () => {
        const senderUsername = fixture.debugElement.query(By.css('.sender-username')).nativeElement;

        expect(senderUsername.textContent).toEqual('@username');
    });

    it('should shows date', () => {
        const date = fixture.debugElement.query(By.css('.date')).nativeElement;

        expect(date.textContent).toEqual('16.03.2002');
    });

    it('should emit output on decline', () => {
        spyOn(component.inviteDeclined, 'emit');
        const decline = fixture.nativeElement.querySelector('.decline');

        decline.dispatchEvent(new Event('click'));
        fixture.detectChanges();

        expect(component.inviteDeclined.emit).toHaveBeenCalledTimes(1);
    });

    it('should emit output on accept', () => {
        spyOn(component.inviteAccepted, 'emit');
        const accept = fixture.nativeElement.querySelector('.accept');

        accept.dispatchEvent(new Event('click'));
        fixture.detectChanges();

        expect(component.inviteAccepted.emit).toHaveBeenCalledTimes(1);
    });
});
