import { RegisterUserDataService } from './../shared/services/register-user-data/register-user-data.service';
import { Router } from '@angular/router';
import { AuthService } from './../shared/services/api/auth/auth.service';
import { Component } from '@angular/core';
import { GoogleLoginProvider, SocialAuthService, VKLoginProvider } from 'angularx-social-login';

@Component({
    selector: 'qtt-sign-in',
    templateUrl: './sign-in.component.html',
    styleUrls: ['./sign-in.component.scss'],
})
export class SignInComponent {
    providers = {
        google: GoogleLoginProvider.PROVIDER_ID,
        vk: VKLoginProvider.PROVIDER_ID,
    };

    constructor(
        private socialAuthService: SocialAuthService,
        private authService: AuthService,
        private router: Router,
        private registerUserDataService: RegisterUserDataService
    ) {}

    signInWithService(service: 'google' | 'vk'): void {
        this.socialAuthService.signIn(this.providers[service]).then(result => {
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
                        });
                        this.router.navigate(['sign-on']);
                    }
                }
            );
        });
    }
}
