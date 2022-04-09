import { AuthService } from 'src/app/shared/services/api/auth/auth.service';
import { AbstractControl, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { Observable, timer } from 'rxjs';
import { debounceTime, map, switchMap } from 'rxjs/operators';

export class UsernameValidator {
    static createValidator(authService: AuthService): AsyncValidatorFn {
        return (control: AbstractControl): Observable<ValidationErrors | null> => {
            return timer(500).pipe(
                switchMap(() => {
                    return authService.checkOutUsername(control.value).pipe(
                        debounceTime(500),
                        map((result: boolean | null) => (result ? { usernameAlreadyExists: true } : null))
                    );
                })
            );
        };
    }
}
