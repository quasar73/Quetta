import { Observable } from 'rxjs';
import { BaseService } from './../base/base.service';
import { Injectable } from '@angular/core';
import { RegisterGoogleUserDto } from 'src/app/shared/dto/register-google-user.dto';
import { TokenDto } from 'src/app/shared/dto/token.dto';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    constructor(private baseService: BaseService) {}

    authenticateWithGoogle(idToken: string): Observable<TokenDto | null> {
        return this.baseService.post<TokenDto>('auth/authenticateWithGoogle', { idToken });
    }

    registerGoogleUser(dto: RegisterGoogleUserDto): Observable<TokenDto | null> {
        return this.baseService.post<TokenDto>('auth/registerGoogleUser', dto);
    }

    checkOutUsername(username: string): Observable<boolean | null> {
        return this.baseService.get<boolean>('auth/checkOutUsername', { username });
    }
}
