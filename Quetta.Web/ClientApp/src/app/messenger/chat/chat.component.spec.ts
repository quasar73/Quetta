import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AuthenticationService } from '@services/auth/authentication.service';
import { TokenStorage } from '@services/auth/token-storage.service';
import { MessageWebsocketService } from '@services/websocket/message-websocket/message-websocket.service';
import { ActivatedRoute } from '@angular/router';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MockComponents } from 'ng-mocks';

import { ChatComponent } from './chat.component';
import { ChatContentComponent } from './chat-content/chat-content.component';
import { ChatHeaderComponent } from './chat-header/chat-header.component';
import { ChatInputComponent } from './chat-input/chat-input.component';

describe('ChatComponent', () => {
    let component: ChatComponent;
    let fixture: ComponentFixture<ChatComponent>;
    let mockActivatedRoute: any;

    beforeEach(async () => {
        mockActivatedRoute = {
            snapshot: {
                paramMap: {
                    get: (id: string) => undefined,
                },
            },
        };

        await TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            declarations: [ChatComponent, ...MockComponents(ChatContentComponent, ChatHeaderComponent, ChatInputComponent)],
            providers: [
                { provide: ActivatedRoute, useValue: mockActivatedRoute },
                MessageWebsocketService,
                AuthenticationService,
                TokenStorage,
            ],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(ChatComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
