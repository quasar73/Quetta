import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TuiButtonModule } from '@taiga-ui/core';
import { TuiAvatarModule } from '@taiga-ui/kit';

import { SignInRoutingModule } from './sign-in-routing.module';
import { SignInComponent } from './sign-in.component';

@NgModule({
    declarations: [SignInComponent],
    imports: [CommonModule, SignInRoutingModule, TuiButtonModule, TuiAvatarModule],
})
export class SignInModule {}
