import { AuthenticationModule } from '@services/auth/authentication.module';
import { ReactiveFormsModule } from '@angular/forms';
import { TuiSelectModule } from '@taiga-ui/kit';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { GoogleLoginProvider, SocialAuthService, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { environment } from 'src/environments/environment';
import { getTranslocoModule } from '../translate/transloco-testing.module';
import { MockProvider } from 'ng-mocks';

import { SignInComponent } from './sign-in.component';
import { EMPTY } from 'rxjs';
import { NgxsModule } from '@ngxs/store';
import { SignUpDataState } from '../state-manager/states/sign-up-data.state';

describe('SignInComponent', () => {
    let component: SignInComponent;
    let fixture: ComponentFixture<SignInComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [SignInComponent],
            imports: [
                SocialLoginModule,
                HttpClientTestingModule,
                RouterTestingModule,
                BrowserAnimationsModule,
                getTranslocoModule(),
                TuiSelectModule,
                ReactiveFormsModule,
                AuthenticationModule,
                NgxsModule.forRoot([SignUpDataState]),
            ],
            providers: [
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
                MockProvider(SocialAuthService, {
                    authState: EMPTY,
                }),
            ],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(SignInComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('getLanguages should return application languages', () => {
        expect(component.languages).toEqual(['en', 'ru']);
    });
});
