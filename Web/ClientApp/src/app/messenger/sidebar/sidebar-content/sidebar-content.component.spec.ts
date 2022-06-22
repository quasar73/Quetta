import { Component } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarContentComponent } from './sidebar-content.component';

describe('SidebarContentComponent', () => {
    let component: SidebarContentComponent;
    let fixture: ComponentFixture<SidebarContentComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [SidebarContentComponent, MockSidebarItemComponent],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(SidebarContentComponent);
        component = fixture.componentInstance;
        component.testData = [];
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
