import { NgDompurifySanitizer } from '@tinkoff/ng-dompurify';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
    TuiRootModule,
    TuiNotificationsModule,
    TUI_SANITIZER,
    TuiThemeNightModule,
    TuiModeModule,
    TuiAlertModule,
    TuiDialogModule,
} from '@taiga-ui/core';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { TranslocoRootModule } from './translate/transloco-root.module';
import { TuiInputModule } from '@taiga-ui/kit';
import { AuthenticationModule } from '@services/auth/authentication.module';
import { NgxsModule } from '@ngxs/store';

import { SelectedNotesState } from './state-manager/states/selected-notes.state';

import { environment } from 'src/environments/environment';

import { AppComponent } from './app.component';
import { SignUpDataState } from './state-manager/states/sign-up-data.state';
@NgModule({
    declarations: [AppComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        TuiRootModule,
        TuiDialogModule,
        TuiNotificationsModule,
        TuiThemeNightModule,
        TuiModeModule,
        TuiInputModule,
        HttpClientModule,
        TranslocoRootModule,
        AuthenticationModule,
        TuiAlertModule,
        NgxsModule.forRoot([SelectedNotesState, SignUpDataState], {
            developmentMode: !environment.production,
        }),
    ],
    providers: [{ provide: TUI_SANITIZER, useClass: NgDompurifySanitizer }],
    bootstrap: [AppComponent],
})
export class AppModule {}
