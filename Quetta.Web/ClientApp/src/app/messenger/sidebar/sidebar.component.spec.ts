import { Component } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TuiScrollbarModule } from '@taiga-ui/core';

import { SidebarComponent } from './sidebar.component';

describe('SidebarComponent', () => {
    let component: SidebarComponent;
    let fixture: ComponentFixture<SidebarComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [SidebarComponent, MockSidebarHeaderComponent, MockSidebarContentComponent, MockSidebarFooterComponent],
            imports: [TuiScrollbarModule],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(SidebarComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});

@Component({
    selector: 'qtt-sidebar-header',
    template: '',
})
class MockSidebarHeaderComponent {}

@Component({
    selector: 'qtt-sidebar-content',
    template: '',
})
class MockSidebarContentComponent {}

@Component({
    selector: 'qtt-sidebar-footer',
    template: '',
})
class MockSidebarFooterComponent {}
