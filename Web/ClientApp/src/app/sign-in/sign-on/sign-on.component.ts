import { RegisterUserDataService } from './../../shared/services/register-user-data/register-user-data.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { lastStepIndex, stepper } from 'src/app/shared/consts/sign-on.const';
import { AuthService } from 'src/app/shared/services/api/auth/auth.service';

@Component({
    selector: 'qtt-sign-on',
    templateUrl: './sign-on.component.html',
    styleUrls: ['./sign-on.component.scss'],
})
export class SignOnComponent implements OnInit {
    signOnForm = new FormGroup({
        username: new FormControl(),
        firstName: new FormControl(),
        lastName: new FormControl(),
        idToken: new FormControl(),
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

        this.registerUserDataService.getUserData().subscribe(data => {
            if (data) {
                console.log(data);
                this.signOnForm.setValue(data);
            }
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
