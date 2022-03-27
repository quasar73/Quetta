import { RegisterUserDataService } from './../../shared/services/register-user-data/register-user-data.service';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'qtt-sign-on',
    templateUrl: './sign-on.component.html',
    styleUrls: ['./sign-on.component.scss'],
})
export class SignOnComponent implements OnInit {
    constructor(private registerUserDataService: RegisterUserDataService) {}

    ngOnInit(): void {
        this.registerUserDataService.getUserData().subscribe(data => {
            console.log(data);
        });
    }
}
