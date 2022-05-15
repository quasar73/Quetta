import { TuiSelectModule } from '@taiga-ui/kit';
import { getTranslocoModule } from 'src/app/translate/transloco-testing.module';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarFooterComponent } from './sidebar-footer.component';

describe('SidebarFooterComponent', () => {
    let component: SidebarFooterComponent;
    let fixture: ComponentFixture<SidebarFooterComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [SidebarFooterComponent],
            imports: [getTranslocoModule(), TuiSelectModule],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(SidebarFooterComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
