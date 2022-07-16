import { InviteModel } from './../../../../shared/api-models/invite.model';
import { InviteService } from './../../../../shared/services/api/invite/invite.service';
import { ChangeDetectionStrategy, Component, OnInit, ChangeDetectorRef } from '@angular/core';

@Component({
    selector: 'qtt-invites-list-dialog',
    templateUrl: './invites-list-dialog.component.html',
    styleUrls: ['./invites-list-dialog.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class InvitesListDialogComponent implements OnInit {
    readonly invites: InviteModel[] = [];

    constructor(public readonly inviteService: InviteService, private readonly cdr: ChangeDetectorRef) {}

    ngOnInit(): void {
        this.inviteService.getInvites().subscribe(invites => {
            this.invites.push(...(invites ?? []));
            console.log(invites);
            this.cdr.detectChanges();
        });
    }
}
