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
import { lastStepIndex, stepper } from 'src/app/shared/consts/sign-on.const';
import { Router } from '@angular/router';

describe('SignUpComponent', () => {
    let component: SignUpComponent;
    let fixture: ComponentFixture<SignUpComponent>;
    let mockRegisterUserDataService;
    let mockRouter: { navigate: any };

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
        mockRouter = {
            navigate: jasmine.createSpy('navigate'),
        };
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
            providers: [
                { provide: RegisterUserDataService, useValue: mockRegisterUserDataService },
                { provide: Router, useValue: mockRouter },
            ],
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

    it('isLastStep should return true when activeIndex and lastStepIndex are equal', () => {
        component.activeIndex = lastStepIndex;

        expect(component.isLastStep).toBeTruthy();
    });

    it('languages should return application languages', () => {
        expect(component.languages).toEqual(['en', 'ru']);
    });

    it('getStepper should return right value', () => {
        expect(component.getStepper).toEqual(stepper);
    });

    it('should increase activeIndex when next button is clicked', () => {
        component.activeIndex = 0;
        const button = fixture.debugElement.nativeElement.querySelector('.next-button');
        button.click();

        expect(component.activeIndex).toEqual(1);
    });

    it('should navigate to sign-in page when come back button is clicked', () => {
        const button = fixture.debugElement.nativeElement.querySelector('.come-back-button');
        button.click();

        expect(mockRouter.navigate).toHaveBeenCalledWith(['/sign-in']);
    });
});
