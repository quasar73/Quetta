import { TestBed } from '@angular/core/testing';

import { MessageUpdaterService } from './message-updater.service';

describe('MessageUpdaterService', () => {
  let service: MessageUpdaterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MessageUpdaterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
