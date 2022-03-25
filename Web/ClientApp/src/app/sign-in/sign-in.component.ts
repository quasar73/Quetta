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

    constructor(private authService: SocialAuthService) {}

    signInWithService(service: 'google' | 'vk'): void {
        this.authService.signIn(this.providers[`${service}`]).then(result => {
            console.log(result);
        });
    }
}
