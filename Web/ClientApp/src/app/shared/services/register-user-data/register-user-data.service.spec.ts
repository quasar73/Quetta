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

    it('should return null like default value', () => {
        service.getUserData().subscribe(value => {
            expect(value).toBeNull();
        });
    });

    it('should return passed user data', () => {
        const userData = {
            firstName: 'TestFirstName',
            lastName: 'TestLastName',
            username: 'testUserName',
            idToken: 'testidToken',
        };
        service.setUserData(userData);
        service.getUserData().subscribe(value => {
            expect(value).toEqual(userData);
        });
    });
});
