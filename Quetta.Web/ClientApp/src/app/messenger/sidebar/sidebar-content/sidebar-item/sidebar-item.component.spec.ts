import { By } from '@angular/platform-browser';
import { TuiAvatarModule } from '@taiga-ui/kit';
import { TuiSvgModule } from '@taiga-ui/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { getTranslocoModule } from 'src/app/translate/transloco-testing.module';
import { testChatItem } from 'src/app/testing/data/chat-item';

import { SidebarItemComponent } from './sidebar-item.component';

describe('SidebarItemComponent', () => {
    let component: SidebarItemComponent;
    let fixture: ComponentFixture<SidebarItemComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [SidebarItemComponent],
            imports: [getTranslocoModule(), TuiSvgModule, TuiAvatarModule],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(SidebarItemComponent);
        component = fixture.componentInstance;
        component.data = testChatItem;
        component.active = false;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should shows last message', () => {
        const lastMessage = fixture.debugElement.query(By.css('.last-message')).nativeElement;

        expect(lastMessage.textContent).toEqual('This is last message');
    });

    it('should shows chat title', () => {
        const title = fixture.debugElement.query(By.css('.title-text')).nativeElement;

        expect(title.textContent).toEqual('Test chat');
    });

    it('should shows no messages text', () => {
        fixture = TestBed.createComponent(SidebarItemComponent);
        component = fixture.componentInstance;
        component.data = { ...testChatItem, lastMessage: null };
        fixture.detectChanges();

        const noMessages = fixture.debugElement.query(By.css('.no-messages'));

        expect(noMessages).not.toBeNull();
    });

    it('should be not active', () => {
        const active = fixture.debugElement.query(By.css('.active'));

        expect(active).toBeNull();
    });

    it('should be active', () => {
        fixture = TestBed.createComponent(SidebarItemComponent);
        component = fixture.componentInstance;
        component.active = true;
        fixture.detectChanges();

        const active = fixture.debugElement.query(By.css('.active'));

        expect(active).not.toBeNull();
    });
});
