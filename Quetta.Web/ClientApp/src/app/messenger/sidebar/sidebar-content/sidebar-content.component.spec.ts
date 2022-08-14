import { RouterTestingModule } from '@angular/router/testing';
import { ChatApiService } from './../../../shared/services/api/chat/chat.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Component } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarContentComponent } from './sidebar-content.component';

describe('SidebarContentComponent', () => {
    let component: SidebarContentComponent;
    let fixture: ComponentFixture<SidebarContentComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [HttpClientTestingModule, RouterTestingModule],
            declarations: [SidebarContentComponent, MockSidebarItemComponent],
            providers: [ChatApiService],
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
});

@Component({
    selector: 'qtt-sidebar-item',
    template: '',
})
class MockSidebarItemComponent {}
