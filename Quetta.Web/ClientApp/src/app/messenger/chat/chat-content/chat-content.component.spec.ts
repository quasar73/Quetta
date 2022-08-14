import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NgxsModule } from '@ngxs/store';
import { ClipboardService } from 'ngx-clipboard';
import { SelectedMessagesState } from 'src/app/state-manager/states/selected-messages.state';

import { ChatContentComponent } from './chat-content.component';

describe('ChatContentComponent', () => {
    let component: ChatContentComponent;
    let fixture: ComponentFixture<ChatContentComponent>;
    let clipboardService: ClipboardService;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [NgxsModule.forRoot([SelectedMessagesState])],
            declarations: [ChatContentComponent],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(ChatContentComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
