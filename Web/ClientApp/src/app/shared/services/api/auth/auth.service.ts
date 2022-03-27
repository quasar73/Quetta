import { Observable } from 'rxjs';
import { BaseService } from './../base/base.service';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    constructor(private baseService: BaseService) {}

    authenticateWithGoogle(idToken: string): Observable<string | null> {
        return this.baseService.post<string>('auth/authenticateWithGoogle', { idToken });
    }
}
