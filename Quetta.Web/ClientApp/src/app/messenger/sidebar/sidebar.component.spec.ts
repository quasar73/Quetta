import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TuiScrollbarModule } from '@taiga-ui/core';
import { MockComponents } from 'ng-mocks';
import { SidebarContentComponent } from './sidebar-content/sidebar-content.component';
import { SidebarFooterComponent } from './sidebar-footer/sidebar-footer.component';
import { SidebarHeaderComponent } from './sidebar-header/sidebar-header.component';

import { SidebarComponent } from './sidebar.component';

describe('SidebarComponent', () => {
    let component: SidebarComponent;
    let fixture: ComponentFixture<SidebarComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [SidebarComponent, ...MockComponents(SidebarHeaderComponent, SidebarContentComponent, SidebarFooterComponent)],
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
