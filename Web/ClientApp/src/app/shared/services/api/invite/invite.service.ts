import { Observable } from 'rxjs';
import { BaseService } from './../base/base.service';
import { Injectable } from '@angular/core';
import { SendInviteModel } from 'src/app/shared/api-models/send-invite.model';
import { IsAnyNotifications } from 'src/app/shared/api-models/is-any-notifications.model';

@Injectable({
    providedIn: 'root',
})
export class InviteService {
    constructor(private base: BaseService) {}

    sendInvite(model: SendInviteModel): Observable<void | null> {
        return this.base.post('invites', model);
    }

    isAnyNotifications(): Observable<IsAnyNotifications | null> {
        return this.base.get('invites/any');
    }
}
