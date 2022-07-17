import { Observable } from 'rxjs';
import { TuiAlertService, TuiNotification } from '@taiga-ui/core';
import { InviteModel } from './../../../../shared/api-models/invite.model';
import { InviteService } from './../../../../shared/services/api/invite/invite.service';
import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
    selector: 'qtt-invites-list-dialog',
    templateUrl: './invites-list-dialog.component.html',
    styleUrls: ['./invites-list-dialog.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class InvitesListDialogComponent implements OnInit {
    readonly removedInvites: string[] = [];
    invites$!: Observable<InviteModel[] | null>;

    constructor(private readonly inviteService: InviteService, private readonly alertService: TuiAlertService) {}

    ngOnInit(): void {
        this.invites$ = this.inviteService.getInvites();
    }

    onInviteAccepted(inviteId: string): void {
        this.removeInvite(inviteId);
        this.inviteService.acceptInvite(inviteId).subscribe(() => {
            this.alertService.open('Invite successfully accepted', { status: TuiNotification.Success }).subscribe();
        });
    }

    onInviteDeclined(inviteId: string): void {
        this.removeInvite(inviteId);
        this.inviteService.declineInvite(inviteId).subscribe(() => {
            this.alertService.open('Invite declined', { status: TuiNotification.Info }).subscribe();
        });
    }

    isRemoved(inviteId: string): boolean {
        return this.removedInvites.includes(inviteId);
    }

    isListEmpty(invites: InviteModel[]): boolean {
        return invites.length === 0 || invites.length === this.removedInvites.length;
    }

    private removeInvite(inviteId: string): void {
        this.removedInvites.push(inviteId);
    }
}
