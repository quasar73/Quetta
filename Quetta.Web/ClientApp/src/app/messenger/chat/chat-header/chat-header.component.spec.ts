import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TuiBadgeModule } from '@taiga-ui/kit';

import { ChatHeaderComponent } from './chat-header.component';

describe('ChatHeaderComponent', () => {
    let component: ChatHeaderComponent;
    let fixture: ComponentFixture<ChatHeaderComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [ChatHeaderComponent],
            imports: [TuiBadgeModule],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(ChatHeaderComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
