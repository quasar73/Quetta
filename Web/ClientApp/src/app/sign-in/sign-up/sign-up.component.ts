import { Router } from '@angular/router';
import { RegisterUserDataService } from '../../shared/services/register-user-data/register-user-data.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { lastStepIndex, stepper, usernameMinLength } from 'src/app/shared/consts/sign-on.const';
import { AuthService } from 'src/app/shared/services/api/auth/auth.service';
import { UsernameValidator } from 'src/app/shared/validators/username.validator';
import { TUI_VALIDATION_ERRORS } from '@taiga-ui/kit';

@Component({
    selector: 'qtt-sign-up',
    templateUrl: './sign-up.component.html',
    styleUrls: ['./sign-up.component.scss'],
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
    activeIndex!: number;
    isFormPending!: boolean;

    get getStepper(): string[] {
        return stepper;
    }

    get getFullName(): string {
        return `${this.signUpForm.get('firstName')?.value} ${this.signUpForm.get('lastName')?.value}`;
    }

    constructor(private registerUserDataService: RegisterUserDataService, private authService: AuthService, private router: Router) {}

    ngOnInit(): void {
        this.activeIndex = 0;
        this.signUpForm.controls['username'].addAsyncValidators(UsernameValidator.createValidator(this.authService));

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
