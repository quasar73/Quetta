import { SignOnComponent } from './sign-on/sign-on.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInComponent } from './sign-in.component';

const routes: Routes = [
    { path: 'sign-in', component: SignInComponent },
    { path: 'sign-on', component: SignOnComponent },
    { path: '', redirectTo: 'sign-in', pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class SignInRoutingModule {}
