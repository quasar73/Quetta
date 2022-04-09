import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SignUpFormComponent } from './sign-up-form/sign-up-form.component';
import { TuiErrorModule, TuiTextfieldControllerModule, TuiSvgModule } from '@taiga-ui/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignUpComponent } from './sign-up.component';
import { getTranslocoModule } from 'src/app/translate/transloco-testing.module';
import { TuiFieldErrorPipeModule, TuiStepperModule, TuiSelectModule, TuiInputModule } from '@taiga-ui/kit';
import { RegisterUserDataService } from 'src/app/shared/services/register-user-data/register-user-data.service';
import { of } from 'rxjs';

describe('SignUpComponent', () => {
    let component: SignUpComponent;
    let fixture: ComponentFixture<SignUpComponent>;
    let mockRegisterUserDataService;

    beforeEach(async () => {
        mockRegisterUserDataService = jasmine.createSpyObj(['getUserData']);
        mockRegisterUserDataService.getUserData.and.returnValue(
            of({
                firstName: 'TestFirstName',
                lastName: 'TestLastName',
                username: 'testUserName',
                idToken: 'testidToken',
            })
        );
        await TestBed.configureTestingModule({
            declarations: [SignUpComponent, SignUpFormComponent],
            imports: [
                HttpClientTestingModule,
                RouterTestingModule,
                getTranslocoModule(),
                ReactiveFormsModule,
                TuiFieldErrorPipeModule,
                TuiStepperModule,
                TuiSelectModule,
                TuiInputModule,
                TuiErrorModule,
                TuiTextfieldControllerModule,
                TuiSvgModule,
                BrowserAnimationsModule,
            ],
            providers: [{ provide: RegisterUserDataService, useValue: mockRegisterUserDataService }],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(SignUpComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('signUpForm should contains right values', () => {
        expect(component.signUpForm.value).toEqual({
            firstName: 'TestFirstName',
            lastName: 'TestLastName',
            username: 'testUserName',
            idToken: 'testidToken',
        });
    });

    it('getFullName should returns right value', () => {
        expect(component.getFullName).toEqual('TestFirstName TestLastName');
    });

    it('stepper state should be normal', () => {
        expect(component.getState(0)).toEqual('normal');
    });

    it('stepper state should be error', () => {
        component.signUpForm.get('username')?.setValue(null);

        expect(component.getState(0)).toEqual('error');
    });

    it('on button click activeIndex should increase', () => {
        component.activeIndex = 0;
        const button = fixture.debugElement.nativeElement.querySelector('.next-button');
        button.click();

        expect(component.activeIndex).toEqual(1);
    });
});
