import { TokenStorage } from './../../../shared/services/auth/token-storage.service';
import { MessageApiService } from 'src/app/shared/services/api/message/message.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AuthenticationService } from 'src/app/shared/services/auth/authentication.service';

import { ChatInputComponent } from './chat-input.component';

describe('ChatInputComponent', () => {
    let component: ChatInputComponent;
    let fixture: ComponentFixture<ChatInputComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            declarations: [ChatInputComponent],
            providers: [AuthenticationService, MessageApiService, TokenStorage],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(ChatInputComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
