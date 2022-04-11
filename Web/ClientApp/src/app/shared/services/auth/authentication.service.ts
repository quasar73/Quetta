import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { tap, map, switchMap, catchError } from 'rxjs/operators';
import { AuthService } from 'ngx-auth';

import { TokenStorage } from './token-storage.service';

interface AccessData {
    accessToken: string;
    refreshToken: string;
}

@Injectable()
export class AuthenticationService implements AuthService {
    constructor(private http: HttpClient, private tokenStorage: TokenStorage) {}

    public isAuthorized(): Observable<boolean> {
        return this.tokenStorage.getAccessToken().pipe(map(token => !!token));
    }

    public getAccessToken(): Observable<string> {
        return this.tokenStorage.getAccessToken();
    }

    public refreshToken(): Observable<AccessData> {
        return this.tokenStorage.getRefreshToken().pipe(
            switchMap((refreshToken: string) => this.http.post<AccessData>(`${environment.apiUrl}auth/refresh`, { refreshToken })),
            tap((tokens: AccessData) => this.saveAccessData(tokens)),
            catchError(err => {
                this.logout();
                return throwError(() => new Error(err));
            })
        );
    }

    public refreshShouldHappen(response: HttpErrorResponse): boolean {
        return response.status === 401;
    }

    public verifyTokenRequest(url: string): boolean {
        return url.endsWith('/refresh');
    }

    public logout(): void {
        this.tokenStorage.clear();
        location.reload();
    }

    public saveAccessData({ accessToken, refreshToken }: AccessData) {
        this.tokenStorage.setAccessToken(accessToken).setRefreshToken(refreshToken);
    }
}
