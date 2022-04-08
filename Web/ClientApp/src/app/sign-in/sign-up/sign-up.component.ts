import { RegisterUserDataService } from '../../shared/services/register-user-data/register-user-data.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { lastStepIndex, stepper } from 'src/app/shared/consts/sign-on.const';
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
                usernameAlreadyExists: 'Username already exists!',
            },
        },
    ],
})
export class SignUpComponent implements OnInit {
    signOnForm = new FormGroup({
        username: new FormControl('', Validators.required),
        firstName: new FormControl('', Validators.required),
        lastName: new FormControl('', Validators.required),
        idToken: new FormControl('', Validators.required),
    });
    activeIndex!: number;

    get getStepper(): string[] {
        return stepper;
    }

    get getFullName(): string {
        return `${this.signOnForm.get('firstName')?.value} ${this.signOnForm.get('lastName')?.value}`;
    }

    constructor(private registerUserDataService: RegisterUserDataService, private authService: AuthService) {}

    ngOnInit(): void {
        this.activeIndex = 0;
        this.signOnForm.controls['username'].addAsyncValidators(UsernameValidator.createValidator(this.authService));

        this.registerUserDataService.getUserData().subscribe(data => {
            if (data) {
                console.log(data);
                this.signOnForm.setValue(data);
            }
        });

        this.signOnForm.valueChanges.subscribe(() => {
            console.log(this.signOnForm.get('username')?.hasError('usernameAlreadyExists'));
        });
    }

    onCompleted(): void {
        if (this.activeIndex === lastStepIndex) {
            this.registrateUser();
        } else {
            this.activeIndex++;
        }
    }

    getState(index: number): 'normal' | 'pass' | 'error' {
        if (this.activeIndex > index) {
            return 'pass';
        }

        return 'normal';
    }

    private registrateUser(): void {
        this.authService.registerGoogleUser(this.signOnForm.value).subscribe(data => {
            console.log(data);
        });
    }
}
