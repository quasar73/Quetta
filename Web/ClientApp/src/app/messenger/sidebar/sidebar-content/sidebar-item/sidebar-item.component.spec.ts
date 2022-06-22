import { TuiAvatarModule } from '@taiga-ui/kit';
import { TuiSvgModule } from '@taiga-ui/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { getTranslocoModule } from 'src/app/translate/transloco-testing.module';

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
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
