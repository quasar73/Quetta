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
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { TranslocoRootModule } from './translate/transloco-root.module';
import { TuiInputModule } from '@taiga-ui/kit';
import { AuthenticationModule } from './shared/services/auth/authentication.module';
import { NgxsModule } from '@ngxs/store';
import { SelectedMessagesState } from './state-manager/states/selected-messages.state';
import { environment } from 'src/environments/environment';
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
        NgxsModule.forRoot([SelectedMessagesState], {
            developmentMode: !environment.production,
        }),
    ],
    providers: [{ provide: TUI_SANITIZER, useClass: NgDompurifySanitizer }],
    bootstrap: [AppComponent],
})
export class AppModule {}
