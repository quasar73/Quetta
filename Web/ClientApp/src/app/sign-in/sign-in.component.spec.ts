import { AuthenticationModule } from './../shared/services/auth/authentication.module';
import { ReactiveFormsModule } from '@angular/forms';
import { TuiSelectModule } from '@taiga-ui/kit';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { GoogleLoginProvider, SocialAuthService, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { SignInComponent } from './sign-in.component';
import { environment } from 'src/environments/environment';
import { getTranslocoModule } from '../translate/transloco-testing.module';

describe('SignInComponent', () => {
    let component: SignInComponent;
    let fixture: ComponentFixture<SignInComponent>;
    let mockSocialAuthService: { signIn: any };

    beforeEach(async () => {
        mockSocialAuthService = {
            signIn: jasmine.createSpy('signIn'),
        };

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
                { provide: SocialAuthService, useValue: mockSocialAuthService },
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

    it('should call signIn method when Sign in button is clicked', () => {
        const button = fixture.debugElement.nativeElement.querySelector('.sign-in-button');
        button.click();

        expect(mockSocialAuthService.signIn).toHaveBeenCalledWith('GOOGLE');
    });
});
