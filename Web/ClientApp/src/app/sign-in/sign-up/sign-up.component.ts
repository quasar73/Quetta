import { TranslocoService } from '@ngneat/transloco';
import { Router } from '@angular/router';
import { RegisterUserDataService } from '../../shared/services/register-user-data/register-user-data.service';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { lastStepIndex, stepper, usernameMinLength } from 'src/app/shared/consts/sign-on.const';
import { AuthApiService } from 'src/app/shared/services/api/auth/auth.service';
import { UsernameValidator } from 'src/app/shared/validators/username.validator';
import { TUI_VALIDATION_ERRORS } from '@taiga-ui/kit';
import { availableLanguage } from 'src/app/shared/consts/languages.const';

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
    signUpForm = new FormGroup({
        username: new FormControl('', [Validators.required, Validators.minLength(usernameMinLength)]),
        firstName: new FormControl('', Validators.required),
        lastName: new FormControl('', Validators.required),
        idToken: new FormControl('', Validators.required),
    });
    languagesControl = new FormControl('en');

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
        private readonly registerUserDataService: RegisterUserDataService,
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

        this.registerUserDataService.getUserData().subscribe(data => {
            if (data) {
                console.log(data);
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
