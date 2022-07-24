import { AuthenticationService } from './../shared/services/auth/authentication.service';
import { UntypedFormControl } from '@angular/forms';
import { RegisterUserDataService } from './../shared/services/register-user-data/register-user-data.service';
import { Router } from '@angular/router';
import { AuthApiService } from './../shared/services/api/auth/auth.service';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { SocialAuthService } from '@abacritt/angularx-social-login';
import { animate, style, transition, trigger } from '@angular/animations';
import { TranslocoService } from '@ngneat/transloco';
import { availableLanguage } from '../shared/consts/languages.const';

@Component({
    selector: 'qtt-sign-in',
    templateUrl: './sign-in.component.html',
    styleUrls: ['./sign-in.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: [
        trigger('signInPageTrigger', [transition(':enter', [style({ opacity: 0 }), animate('0.8s ease-in', style({ opacity: 1 }))])]),
    ],
})
export class SignInComponent implements OnInit {
    languagesControl = new UntypedFormControl('en');

    get languages(): string[] {
        return availableLanguage;
    }

    constructor(
        private readonly socialAuthService: SocialAuthService,
        private readonly authApiService: AuthApiService,
        private readonly router: Router,
        private readonly registerUserDataService: RegisterUserDataService,
        private readonly translocoService: TranslocoService,
        private readonly authService: AuthenticationService
    ) {}

    ngOnInit(): void {
        this.languagesControl.setValue(this.translocoService.getActiveLang());

        this.languagesControl.valueChanges.subscribe(language => {
            this.translocoService.setActiveLang(language);
        });

        this.socialAuthService.authState.subscribe(result => {
            this.authApiService.authenticateWithGoogle(result.idToken).subscribe(token => {
                if (token) {
                    this.authService.saveAccessData(token);
                } else {
                    this.registerUserDataService.setUserData({
                        firstName: result.firstName,
                        lastName: result.lastName,
                        username: result.email.split('@')[0],
                        idToken: result.idToken,
                    });
                    this.router.navigate(['sign-up']);
                }
            });
        });
    }
}
