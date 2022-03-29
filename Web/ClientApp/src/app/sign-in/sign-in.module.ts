import { environment } from './../../environments/environment';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TuiButtonModule, TuiTextfieldControllerModule } from '@taiga-ui/core';
import { TuiAvatarModule, TuiInputModule, TuiStepperModule } from '@taiga-ui/kit';

import { SignInRoutingModule } from './sign-in-routing.module';
import { SignInComponent } from './sign-in.component';
import { TranslocoModule, TRANSLOCO_SCOPE } from '@ngneat/transloco';
import {
    GoogleLoginProvider,
    SocialAuthServiceConfig,
    SocialLoginModule,
    VKLoginProvider,
} from 'angularx-social-login';
import { SignOnComponent } from './sign-on/sign-on.component';
import { SignOnFormComponent } from './sign-on/sign-on-form/sign-on-form.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
    declarations: [SignInComponent, SignOnComponent, SignOnFormComponent],
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
    imports: [
        CommonModule,
        SignInRoutingModule,
        SocialLoginModule,
        TranslocoModule,
        ReactiveFormsModule,
        TuiButtonModule,
        TuiAvatarModule,
        TuiInputModule,
        TuiStepperModule,
        TuiTextfieldControllerModule,
    ],
})
export class SignInModule {}
