import { signUpData } from './../../models/sign-up-data.model';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class RegisterUserDataService {
    private readonly userData = new BehaviorSubject<signUpData | null>(null);

    constructor() {}

    getUserData(): Observable<signUpData | null> {
        return this.userData.asObservable();
    }

    setUserData(userData: signUpData): void {
        this.userData.next(userData);
    }
}
