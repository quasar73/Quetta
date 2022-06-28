import { Observable } from 'rxjs';
import { BaseService } from './../base/base.service';
import { Injectable } from '@angular/core';
import { RegisterGoogleUserModel } from 'src/app/shared/api-models/register-google-user.model';
import { TokenModel } from 'src/app/shared/api-models/token.model';

@Injectable({
    providedIn: 'root',
})
export class AuthApiService {
    constructor(private readonly baseService: BaseService) {}

    authenticateWithGoogle(idToken: string): Observable<TokenModel | null> {
        return this.baseService.post<TokenModel>('auth/authenticateWithGoogle', { idToken });
    }

    registerGoogleUser(dto: RegisterGoogleUserModel): Observable<TokenModel | null> {
        return this.baseService.post<TokenModel>('auth/registerGoogleUser', dto);
    }

    checkOutUsername(username: string): Observable<boolean | null> {
        return this.baseService.get<boolean>('auth/checkOutUsername', { username });
    }
}
