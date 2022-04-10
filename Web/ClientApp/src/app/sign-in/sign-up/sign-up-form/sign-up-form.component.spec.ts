import { getTranslocoModule } from 'src/app/translate/transloco-testing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignUpFormComponent } from './sign-up-form.component';

describe('SignUpFormComponent', () => {
    let component: SignUpFormComponent;
    let fixture: ComponentFixture<SignUpFormComponent>;
    const expectedSubtitle = 'Testing subtitle';

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [SignUpFormComponent],
            imports: [BrowserAnimationsModule, getTranslocoModule()],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(SignUpFormComponent);
        component = fixture.componentInstance;
        component.subtitle = expectedSubtitle;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should shown expected title', () => {
        const subtitle = fixture.nativeElement.querySelector('.subtitle').textContent;

        expect(subtitle).toEqual(expectedSubtitle);
    });
});
