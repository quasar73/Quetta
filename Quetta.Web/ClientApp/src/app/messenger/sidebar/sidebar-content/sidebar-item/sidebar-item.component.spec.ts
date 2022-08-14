import { TuiAvatarModule } from '@taiga-ui/kit';
import { TuiSvgModule } from '@taiga-ui/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { getTranslocoModule } from 'src/app/translate/transloco-testing.module';

import { SidebarItemComponent } from './sidebar-item.component';
import { ChatType } from 'src/app/shared/enums/chat-type.enum';

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
        component.data = {
            id: 'id',
            title: 'chat title',
            chatType: ChatType.Channel,
            lastMessage: 'last message',
        };
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
