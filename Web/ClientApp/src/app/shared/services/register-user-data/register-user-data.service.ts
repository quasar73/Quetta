import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { GoogleUserData } from '../../models/google-user-data.mode';

@Injectable({
    providedIn: 'root',
})
export class RegisterUserDataService {
    private readonly userData = new BehaviorSubject<GoogleUserData | null>(null);

    constructor() {}

    getUserData(): Observable<GoogleUserData | null> {
        return this.userData.asObservable();
    }

    setUserData(userData: GoogleUserData): void {
        this.userData.next(userData);
    }
}
