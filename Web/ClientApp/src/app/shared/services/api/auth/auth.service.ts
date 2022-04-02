import { Observable } from 'rxjs';
import { BaseService } from './../base/base.service';
import { Injectable } from '@angular/core';
import { RegisterGoogleUserDto } from 'src/app/shared/dto/register-google-user.dto';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    constructor(private baseService: BaseService) {}

    authenticateWithGoogle(idToken: string): Observable<string | null> {
        return this.baseService.post<string>('auth/authenticateWithGoogle', { idToken });
    }

    registerGoogleUser(dto: RegisterGoogleUserDto): Observable<string | null> {
        return this.baseService.post<string>('auth/registerGoogleUser', dto);
    }
}
