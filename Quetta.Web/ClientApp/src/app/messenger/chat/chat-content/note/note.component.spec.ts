import { of } from 'rxjs';
import { MockProvider } from 'ng-mocks';
import { ReactiveFormsModule } from '@angular/forms';
import { TuiSvgModule } from '@taiga-ui/core';
import { TokenStorage } from './../../../../shared/services/auth/token-storage.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AuthenticationService } from 'src/app/shared/services/auth/authentication.service';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoteComponent } from './note.component';
import { MessageStatus } from 'src/app/shared/enums/message-status.enum';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { TuiCheckboxModule, TuiDropdownContextModule } from '@taiga-ui/kit';
import { By } from '@angular/platform-browser';

describe('NoteComponent', () => {
    let component: NoteComponent;
    let fixture: ComponentFixture<NoteComponent>;
    const message = {
        code: 'code',
        isSelected: false,
        text: 'this is message',
        username: 'username',
        date: '',
        status: MessageStatus.Unreaded,
    };

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                HttpClientTestingModule,
                NoopAnimationsModule,
                TuiCheckboxModule,
                TuiSvgModule,
                TuiDropdownContextModule,
                ReactiveFormsModule,
            ],
            declarations: [NoteComponent],
            providers: [
                MockProvider(AuthenticationService, {
                    getUserInfo: () =>
                        of({
                            firstName: 'First',
                            lastName: 'Last',
                            username: 'username',
                        }),
                }),
                TokenStorage,
            ],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(NoteComponent);
        component = fixture.componentInstance;
        component.message = message;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should shows correct message', () => {
        const messageDiv = fixture.debugElement.query(By.css('.message')).nativeElement;

        expect(messageDiv.textContent).toEqual('this is message');
    });

    it('should shows message as own', () => {
        component.isUserOwner().subscribe(isOwner => {
            const note = fixture.debugElement.query(By.css('.note'))?.nativeElement;

            expect(isOwner).toBeTruthy();
            expect(note.classList.contains('own-message')).toBeTruthy();
        });
    });

    it('should shows message as not own', () => {
        fixture = TestBed.createComponent(NoteComponent);
        component = fixture.componentInstance;
        component.message = {
            ...message,
            username: 'notthisuser',
        };
        fixture.detectChanges();

        fixture.whenStable().then(() => {
            component.isUserOwner().subscribe(isOwner => {
                const note = fixture.debugElement.query(By.css('.note'))?.nativeElement;

                expect(isOwner).toBeFalsy();
                expect(note.classList.contains('own-message')).toBeFalsy();
            });
        });
    });

    it('should have not class if not selected', () => {
        const note = fixture.debugElement.query(By.css('.wrap'))?.nativeElement;

        expect(note.classList.contains('selected')).toBeFalsy();
    });

    it('should have class if selected', () => {
        fixture = TestBed.createComponent(NoteComponent);
        component = fixture.componentInstance;
        component.message = {
            ...message,
            isSelected: true,
        };
        fixture.detectChanges();

        const note = fixture.debugElement.query(By.css('.wrap'))?.nativeElement;

        expect(note.classList.contains('selected')).toBeTruthy();
    });
});
