import { RegisterUserDataService } from './../../shared/services/register-user-data/register-user-data.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

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
    });

    constructor(private registerUserDataService: RegisterUserDataService) {}

    ngOnInit(): void {
        this.registerUserDataService.getUserData().subscribe(data => {
            if (data) {
                this.signOnForm.setValue(data);
            }
        });
    }
}
