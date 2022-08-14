import { TestBed } from '@angular/core/testing';

import { SelectedChatService } from './selected-chat.service';

describe('SelectedChatService', () => {
    let service: SelectedChatService;

    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(SelectedChatService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
