import { of } from 'rxjs';
import { By } from '@angular/platform-browser';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NgxsModule, Store } from '@ngxs/store';
import { TuiBadgeModule } from '@taiga-ui/kit';
import { SelectedMessagesState } from 'src/app/state-manager/states/selected-messages.state';
import { chatInfoTest } from 'src/app/testing/data/chat-info';

import { ChatHeaderComponent } from './chat-header.component';

describe('ChatHeaderComponent', () => {
    let component: ChatHeaderComponent;
    let fixture: ComponentFixture<ChatHeaderComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [ChatHeaderComponent],
            imports: [TuiBadgeModule, NgxsModule.forRoot([SelectedMessagesState])],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(ChatHeaderComponent);
        component = fixture.componentInstance;
        component.chatInfo = chatInfoTest;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should shows right chat inforamtion', () => {
        const chatTitle = fixture.debugElement.query(By.css('.chat-title')).nativeElement;

        expect(chatTitle.textContent).toBe('Test chat');
    });

    it('should shows selecting menu', () => {
        const store: Store = TestBed.inject(Store);
        spyOn(store, 'select').and.returnValue(of(['1', '2', '3']));
        fixture = TestBed.createComponent(ChatHeaderComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();

        const selectingMenu = fixture.debugElement.query(By.css('.selecting-menu')).nativeElement;

        expect(selectingMenu).not.toBeUndefined();
    });
});
