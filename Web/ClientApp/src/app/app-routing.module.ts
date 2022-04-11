import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProtectedGuard, PublicGuard } from 'ngx-auth';

const routes: Routes = [
    { path: '', loadChildren: () => import('./sign-in/sign-in.module').then(m => m.SignInModule), canActivate: [PublicGuard] },
    {
        path: 'messenger',
        loadChildren: () => import('./messenger/messenger.module').then(m => m.MessengerModule),
        canActivate: [ProtectedGuard],
    },
    { path: '', redirectTo: 'sign-in', pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
