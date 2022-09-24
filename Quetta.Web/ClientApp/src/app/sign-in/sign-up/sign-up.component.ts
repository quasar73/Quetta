import { SignUpDataStateModel } from './../../state-manager/models/sign-up-data.model';
import { Observable } from 'rxjs';
import { TranslocoService } from '@ngneat/transloco';
import { Router } from '@angular/router';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { lastStepIndex, stepper, usernameMinLength } from 'src/app/shared/consts/sign-on.const';
import { AuthApiService } from 'src/app/shared/services/api/auth/auth.service';
import { UsernameValidator } from 'src/app/shared/validators/username.validator';
import { TUI_VALIDATION_ERRORS } from '@taiga-ui/kit';
import { availableLanguage } from 'src/app/shared/consts/languages.const';
import { Select } from '@ngxs/store';

@Component({
    selector: 'qtt-sign-up',
    templateUrl: './sign-up.component.html',
    styleUrls: ['./sign-up.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [
        {
            provide: TUI_VALIDATION_ERRORS,
            useValue: {
                required: 'Enter this!',
                minlength: `Too short! Min length is ${usernameMinLength}`,
                usernameAlreadyExists: 'Username already exists!',
            },
        },
    ],
})
export class SignUpComponent implements OnInit {
    @Select() signUpData$!: Observable<SignUpDataStateModel | null>;

    signUpForm = new UntypedFormGroup({
        username: new UntypedFormControl('', [Validators.required, Validators.minLength(usernameMinLength)]),
        firstName: new UntypedFormControl('', Validators.required),
        lastName: new UntypedFormControl('', Validators.required),
        idToken: new UntypedFormControl('', Validators.required),
    });
    languagesControl = new UntypedFormControl('en');

    activeIndex!: number;
    isFormPending!: boolean;

    get getStepper(): string[] {
        return stepper;
    }

    get getFullName(): string {
        return `${this.signUpForm.get('firstName')?.value} ${this.signUpForm.get('lastName')?.value}`;
    }

    get languages(): string[] {
        return availableLanguage;
    }

    get isLastStep(): boolean {
        return lastStepIndex === this.activeIndex;
    }

    constructor(
        private readonly authService: AuthApiService,
        private readonly router: Router,
        private readonly translocoService: TranslocoService
    ) {}

    ngOnInit(): void {
        this.activeIndex = 0;
        this.signUpForm.controls['username'].addAsyncValidators(UsernameValidator.createValidator(this.authService));
        this.languagesControl.setValue(this.translocoService.getActiveLang());

        this.languagesControl.valueChanges.subscribe(language => {
            this.translocoService.setActiveLang(language);
        });

        this.signUpData$.subscribe(data => {
            console.log(data);
            if (data) {
                this.signUpForm.setValue(data);
            }
        });

        this.signUpForm.statusChanges.subscribe(value => (this.isFormPending = value === 'PENDING'));
    }

    nextStep(): void {
        if (this.activeIndex === lastStepIndex) {
            this.registrateUser();
        } else {
            this.activeIndex++;
        }
    }

    comeBack(): void {
        this.router.navigate(['/sign-in']);
    }

    getState(index: number): 'normal' | 'pass' | 'error' {
        if (index === 0 && this.signUpForm.get('username')?.invalid) {
            return 'error';
        } else if (index === 1 && (this.signUpForm.get('firstName')?.invalid || this.signUpForm.get('lastName')?.invalid)) {
            return 'error';
        }

        if (this.activeIndex > index) {
            return 'pass';
        }

        return 'normal';
    }

    private registrateUser(): void {
        this.authService.registerGoogleUser(this.signUpForm.value).subscribe(data => {
            console.log(data);
        });
    }
}
