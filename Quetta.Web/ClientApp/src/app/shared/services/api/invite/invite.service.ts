import { Observable } from 'rxjs';
import { BaseService } from './../base/base.service';
import { Injectable } from '@angular/core';
import { SendInviteModel } from '@api-models/send-invite.model';
import { InviteModel } from '@api-models/invite.model';
import { IsAnyInvites } from '@api-models/is-any-invites.model';

@Injectable({
    providedIn: 'root',
})
export class InviteApiService {
    constructor(private base: BaseService) {}

    sendInvite(model: SendInviteModel): Observable<void | null> {
        return this.base.post<void>('invites', model);
    }

    acceptInvite(inviteId: string): Observable<void | null> {
        return this.base.post<void>('invites/accept', { inviteId });
    }

    declineInvite(inviteId: string): Observable<void | null> {
        return this.base.post<void>('invites/decline', { inviteId });
    }

    isAnyInvites(): Observable<IsAnyInvites | null> {
        return this.base.get('invites/any');
    }

    getInvites(): Observable<InviteModel[] | null> {
        return this.base.get<InviteModel[]>('invites');
    }
}
