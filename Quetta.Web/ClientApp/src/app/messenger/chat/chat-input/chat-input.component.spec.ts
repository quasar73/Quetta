import { ReactiveFormsModule } from '@angular/forms';
import { getTranslocoModule } from 'src/app/translate/transloco-testing.module';
import { GuidService } from '@services/guid/guid.service';
import { EMPTY, of } from 'rxjs';
import { MockService } from 'ng-mocks';
import { TokenStorage } from '@services/auth/token-storage.service';
import { MessageApiService } from '@api-services/message/message.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AuthenticationService } from '@services/auth/authentication.service';

import { ChatInputComponent } from './chat-input.component';
import { Guid } from 'guid-typescript';
import { MessageStatus } from '@enums/message-status.enum';
import { TuiTextAreaModule } from '@taiga-ui/kit';

describe('ChatInputComponent', () => {
    let component: ChatInputComponent;
    let fixture: ComponentFixture<ChatInputComponent>;
    let messageApiService: MessageApiService;

    beforeEach(async () => {
        const mockAuthService = MockService(AuthenticationService, {
            getUserInfo: () =>
                of({
                    username: 'username',
                    firstName: 'First',
                    lastName: 'Last',
                }),
        });

        const mockGuidService = MockService(GuidService, {
            getValue: () => Guid.createEmpty(),
        });

        await TestBed.configureTestingModule({
            imports: [HttpClientTestingModule, getTranslocoModule(), TuiTextAreaModule, ReactiveFormsModule],
            declarations: [ChatInputComponent],
            providers: [
                MessageApiService,
                TokenStorage,
                { provide: AuthenticationService, useValue: mockAuthService },
                { provide: GuidService, useValue: mockGuidService },
            ],
        }).compileComponents();

        messageApiService = TestBed.inject(MessageApiService);
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(ChatInputComponent);
        component = fixture.componentInstance;
        component.chatId = 'chat-id';
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should call send message method, emit output and call api', () => {
        component.messageForm.setValue({
            text: 'test text',
        });
        spyOn(component.messageSent, 'emit');
        const messageApiServiceSpy = spyOn(messageApiService, 'sendMessage').and.returnValue(EMPTY);

        const button = fixture.nativeElement.querySelector('.send-message-btn');
        button.dispatchEvent(new Event('click'));

        expect(component.messageSent.emit).toHaveBeenCalledWith({
            text: 'test text',
            username: 'username',
            date: new Date().toString(),
            status: MessageStatus.Pending,
            isSelected: false,
            code: '00000000-0000-0000-0000-000000000000',
        });

        expect(messageApiServiceSpy).toHaveBeenCalledWith({
            chatId: 'chat-id',
            text: 'test text',
        });
    });

    it('form should be invalid if validation failed', () => {
        component.messageForm.setValue({ text: '' });
        fixture.detectChanges();

        expect(component.messageForm.invalid).toBeTruthy();

        component.messageForm.setValue({ text: 'a'.repeat(2001) });
        fixture.detectChanges();

        expect(component.messageForm.invalid).toBeTruthy();
    });
});
