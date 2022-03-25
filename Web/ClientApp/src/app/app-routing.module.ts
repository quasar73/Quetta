import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    { path: 'sign-in', loadChildren: () => import('./sign-in/sign-in.module').then(m => m.SignInModule) },
    { path: '', redirectTo: 'sign-in', pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
