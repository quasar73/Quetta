import { FormControl } from '@angular/forms';
import { RegisterUserDataService } from './../shared/services/register-user-data/register-user-data.service';
import { Router } from '@angular/router';
import { AuthService } from './../shared/services/api/auth/auth.service';
import { Component, OnInit } from '@angular/core';
import { GoogleLoginProvider, SocialAuthService } from 'angularx-social-login';
import { animate, style, transition, trigger } from '@angular/animations';
import { TranslocoService } from '@ngneat/transloco';
import { availableLanguage } from '../shared/consts/languages.const';

@Component({
    selector: 'qtt-sign-in',
    templateUrl: './sign-in.component.html',
    styleUrls: ['./sign-in.component.scss'],
    animations: [
        trigger('signInPageTrigger', [transition(':enter', [style({ opacity: 0 }), animate('0.8s ease-in', style({ opacity: 1 }))])]),
    ],
})
export class SignInComponent implements OnInit {
    languagesControl = new FormControl('en');

    get languages(): string[] {
        return availableLanguage;
    }

    constructor(
        private socialAuthService: SocialAuthService,
        private authService: AuthService,
        private router: Router,
        private registerUserDataService: RegisterUserDataService,
        private translocoService: TranslocoService
    ) {}

    ngOnInit(): void {
        this.languagesControl.setValue(this.translocoService.getActiveLang());

        this.languagesControl.valueChanges.subscribe(language => {
            this.translocoService.setActiveLang(language);
        });
    }

    signInWithGoogle(): void {
        this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID).then(result => {
            this.authService.authenticateWithGoogle(result.idToken).subscribe(
                token => {
                    console.log(token);
                },
                err => {
                    if (err.status === 401) {
                        this.registerUserDataService.setUserData({
                            firstName: result.firstName,
                            lastName: result.lastName,
                            username: result.email.split('@')[0],
                            idToken: result.idToken,
                        });
                        this.router.navigate(['sign-up']);
                    }
                }
            );
        });
    }
}
