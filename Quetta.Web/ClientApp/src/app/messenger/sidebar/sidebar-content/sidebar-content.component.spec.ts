import { RouterTestingModule } from '@angular/router/testing';
import { ChatApiService } from './../../../shared/services/api/chat/chat.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarContentComponent } from './sidebar-content.component';
import { MockComponent } from 'ng-mocks';
import { SidebarItemComponent } from './sidebar-item/sidebar-item.component';

describe('SidebarContentComponent', () => {
    let component: SidebarContentComponent;
    let fixture: ComponentFixture<SidebarContentComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [HttpClientTestingModule, RouterTestingModule],
            declarations: [SidebarContentComponent, MockComponent(SidebarItemComponent)],
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
