import { SignOnData } from './../../models/sign-on-data.model';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class RegisterUserDataService {
    private readonly userData = new BehaviorSubject<SignOnData | null>(null);

    constructor() {}

    getUserData(): Observable<SignOnData | null> {
        return this.userData.asObservable();
    }

    setUserData(userData: SignOnData): void {
        this.userData.next(userData);
    }
}
