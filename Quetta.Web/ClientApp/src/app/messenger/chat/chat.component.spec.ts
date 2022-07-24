import { Component } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatComponent } from './chat.component';

describe('ChatComponent', () => {
    let component: ChatComponent;
    let fixture: ComponentFixture<ChatComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [ChatComponent, MockChatContentComponent, MockChatHeaderComponent, MockChatInputComponent],
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

@Component({
    selector: 'qtt-chat-header',
    template: '',
})
class MockChatHeaderComponent {}

@Component({
    selector: 'qtt-chat-content',
    template: '',
})
class MockChatContentComponent {}

@Component({
    selector: 'qtt-chat-input',
    template: '',
})
class MockChatInputComponent {}
