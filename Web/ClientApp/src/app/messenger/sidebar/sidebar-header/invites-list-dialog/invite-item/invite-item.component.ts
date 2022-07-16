import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { InviteModel } from 'src/app/shared/api-models/invite.model';

@Component({
    selector: 'qtt-invite-item',
    templateUrl: './invite-item.component.html',
    styleUrls: ['./invite-item.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class InviteItemComponent {
    @Input() invite!: InviteModel;

    @Output() inviteAccepted = new EventEmitter();

    constructor() {}
}
