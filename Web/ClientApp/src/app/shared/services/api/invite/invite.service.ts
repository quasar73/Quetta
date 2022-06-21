import { Observable } from 'rxjs';
import { BaseService } from './../base/base.service';
import { Injectable } from '@angular/core';
import { SendInviteModel } from 'src/app/shared/api-models/send-invite.model';

@Injectable({
    providedIn: 'root',
})
export class InviteService {
    constructor(private base: BaseService) {}

    sendInvite(model: SendInviteModel): Observable<void | null> {
        return this.base.post('invites', model);
    }
}
