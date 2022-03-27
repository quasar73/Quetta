import { TestBed } from '@angular/core/testing';

import { RegisterUserDataService } from './register-user-data.service';

describe('RegisterUserDataService', () => {
    let service: RegisterUserDataService;

    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(RegisterUserDataService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
