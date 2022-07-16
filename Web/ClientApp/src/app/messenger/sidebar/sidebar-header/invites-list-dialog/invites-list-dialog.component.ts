import { Observable } from 'rxjs';
import { POLYMORPHEUS_CONTEXT } from '@tinkoff/ng-polymorpheus';
import { TuiAlertService, TuiDialogContext, TuiNotification } from '@taiga-ui/core';
import { InviteModel } from './../../../../shared/api-models/invite.model';
import { InviteService } from './../../../../shared/services/api/invite/invite.service';
import { Component, OnInit, ChangeDetectionStrategy, DoCheck, ChangeDetectorRef, Inject } from '@angular/core';

@Component({
    selector: 'qtt-invites-list-dialog',
    templateUrl: './invites-list-dialog.component.html',
    styleUrls: ['./invites-list-dialog.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class InvitesListDialogComponent implements OnInit, DoCheck {
    readonly removedInvites: string[] = [];
    invites$!: Observable<InviteModel[] | null>;

    constructor(
        private readonly inviteService: InviteService,
        private readonly alertService: TuiAlertService,
        private readonly cdr: ChangeDetectorRef,
        @Inject(POLYMORPHEUS_CONTEXT) private readonly context: TuiDialogContext
    ) {}

    ngOnInit(): void {
        this.invites$ = this.inviteService.getInvites();
    }

    ngDoCheck(): void {
        console.log('check!');
    }

    onInviteAccepted(inviteId: string): void {
        this.inviteService.acceptInvite(inviteId).subscribe(() => {
            this.alertService.open('Invite successfully accepted', { status: TuiNotification.Success }).subscribe();
            this.removeInvite(inviteId);
        });
    }

    onInviteDeclined(inviteId: string): void {
        // this.inviteService.declineInvite(inviteId).subscribe(() => {
        //     this.alertService.open('Invite declined', { status: TuiNotification.Info }).subscribe();
        //     this.removeInvite(inviteId);
        // });
        this.removeInvite(inviteId);
    }

    isRemoved(inviteId: string): boolean {
        return this.removedInvites.includes(inviteId);
    }

    private removeInvite(inviteId: string): void {
        this.removedInvites.push(inviteId);
    }
}
