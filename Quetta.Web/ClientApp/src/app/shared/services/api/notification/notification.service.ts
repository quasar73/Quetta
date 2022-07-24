import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IsAnyNotifications } from 'src/app/shared/api-models/is-any-notifications.model';
import { BaseService } from '../base/base.service';

@Injectable({
    providedIn: 'root',
})
export class NotificationService {
    constructor(private base: BaseService) {}

    isAnyNotifications(): Observable<IsAnyNotifications | null> {
        return this.base.get('notifications/any');
    }
}
