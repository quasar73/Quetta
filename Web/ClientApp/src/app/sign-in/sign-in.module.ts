import { environment } from './../../environments/environment';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TuiButtonModule } from '@taiga-ui/core';
import { TuiAvatarModule } from '@taiga-ui/kit';

import { SignInRoutingModule } from './sign-in-routing.module';
import { SignInComponent } from './sign-in.component';
import { TranslocoModule, TRANSLOCO_SCOPE } from '@ngneat/transloco';
import {
    GoogleLoginProvider,
    SocialAuthServiceConfig,
    SocialLoginModule,
    VKLoginProvider,
} from 'angularx-social-login';

@NgModule({
    declarations: [SignInComponent],
    providers: [
        { provide: TRANSLOCO_SCOPE, useValue: 'sign-in' },
        {
            provide: 'SocialAuthServiceConfig',
            useValue: {
                autoLogin: false,
                providers: [
                    {
                        id: GoogleLoginProvider.PROVIDER_ID,
                        provider: new GoogleLoginProvider(environment.googleClientId),
                    },
                    {
                        id: VKLoginProvider.PROVIDER_ID,
                        provider: new VKLoginProvider(environment.vkClientId),
                    },
                ],
                onError: err => console.log(err),
            } as SocialAuthServiceConfig,
        },
    ],
    imports: [CommonModule, SignInRoutingModule, SocialLoginModule, TranslocoModule, TuiButtonModule, TuiAvatarModule],
})
export class SignInModule {}
