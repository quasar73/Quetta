import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TuiButtonModule } from '@taiga-ui/core';
import { TuiAvatarModule } from '@taiga-ui/kit';

import { SignInRoutingModule } from './sign-in-routing.module';
import { SignInComponent } from './sign-in.component';
import { TranslocoModule, TRANSLOCO_SCOPE } from '@ngneat/transloco';

@NgModule({
    declarations: [SignInComponent],
    providers: [{ provide: TRANSLOCO_SCOPE, useValue: 'sign-in' }],
    imports: [CommonModule, SignInRoutingModule, TranslocoModule, TuiButtonModule, TuiAvatarModule],
})
export class SignInModule {}
