import { EMPTY } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { ChatApiService } from './../../../shared/services/api/chat/chat.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarContentComponent } from './sidebar-content.component';
import { MockComponent, MockService } from 'ng-mocks';
import { SidebarItemComponent } from './sidebar-item/sidebar-item.component';

describe('SidebarContentComponent', () => {
    let component: SidebarContentComponent;
    let fixture: ComponentFixture<SidebarContentComponent>;

    beforeEach(async () => {
        const mockChatService = MockService(ChatApiService, {
            getChats: () => EMPTY,
        });

        await TestBed.configureTestingModule({
            imports: [HttpClientTestingModule, RouterTestingModule],
            declarations: [SidebarContentComponent, MockComponent(SidebarItemComponent)],
            providers: [{ provide: ChatApiService, useValue: mockChatService }],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(SidebarContentComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should return true for selected chat', () => {
        component.selectedId = 'some-id';

        expect(component.isActive('some-id')).toBeTruthy();
    });

    it('should return false for not selected chat', () => {
        component.selectedId = 'some-id1';

        expect(component.isActive('some-id2')).toBeFalsy();
    });
});
