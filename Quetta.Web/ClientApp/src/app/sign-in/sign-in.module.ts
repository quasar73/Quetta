import { environment } from './../../environments/environment';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TuiButtonModule, TuiDataListModule, TuiErrorModule, TuiSvgModule, TuiTextfieldControllerModule } from '@taiga-ui/core';
import {
    TuiAvatarModule,
    TuiDataListWrapperModule,
    TuiFieldErrorPipeModule,
    TuiInputModule,
    TuiSelectModule,
    TuiStepperModule,
} from '@taiga-ui/kit';

import { SignInRoutingModule } from './sign-in-routing.module';
import { SignInComponent } from './sign-in.component';
import { TranslocoModule, TRANSLOCO_SCOPE } from '@ngneat/transloco';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';
import { SignUpComponent } from './sign-up/sign-up.component';
import { SignUpFormComponent } from './sign-up/sign-up-form/sign-up-form.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
    declarations: [SignInComponent, SignUpComponent, SignUpFormComponent],
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
        TuiSvgModule,
        TuiFieldErrorPipeModule,
        TuiErrorModule,
        TuiSelectModule,
        TuiDataListModule,
        TuiDataListWrapperModule,
    ],
})
export class SignInModule {}
