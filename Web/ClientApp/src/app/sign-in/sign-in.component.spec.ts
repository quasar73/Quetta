import { ReactiveFormsModule } from '@angular/forms';
import { TuiSelectModule } from '@taiga-ui/kit';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from 'angularx-social-login';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { SignInComponent } from './sign-in.component';
import { environment } from 'src/environments/environment';
import { getTranslocoModule } from '../translate/transloco-testing.module';

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
});
