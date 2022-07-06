import { InviteModel } from './../../../../shared/api-models/invite.model';
import { InviteService } from './../../../../shared/services/api/invite/invite.service';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';

@Component({
    selector: 'qtt-invites-list-dialog',
    templateUrl: './invites-list-dialog.component.html',
    styleUrls: ['./invites-list-dialog.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class InvitesListDialogComponent implements OnInit {
    invites: InviteModel[] = [];

    constructor(public readonly inviteService: InviteService, private readonly cdr: ChangeDetectorRef) {}

    ngOnInit(): void {
        this.inviteService.getInvites().subscribe(invites => {
            this.invites = invites ?? [];
            this.cdr.detectChanges();
        });
    }
}
