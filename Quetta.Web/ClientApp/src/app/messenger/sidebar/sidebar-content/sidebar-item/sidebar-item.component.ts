import { ChatItemModel } from '@api-models/chat-item.model';
import { TranslocoService } from '@ngneat/transloco';
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { ChatType } from '@enums/chat-type.enum';

@Component({
    selector: 'qtt-sidebar-item',
    templateUrl: './sidebar-item.component.html',
    styleUrls: ['./sidebar-item.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SidebarItemComponent {
    @Input() active = false;
    @Input() data!: ChatItemModel;

    get title(): string {
        return this.data?.title ?? '??';
    }

    get icon(): string {
        switch (this.data?.chatType) {
            case ChatType.PersonalChat:
                return 'tuiIconUser';
            case ChatType.GroupChat:
                return 'tuiIconUsers';
            case ChatType.Channel:
                return 'tuiIconComment';
            default:
                return 'tuiIconUser';
        }
    }

    get dateTime(): string {
        // if (this.data) {
        //     return dateCalculator(this.data?.lastMessageDate, this.translocoSerivce);
        // } else {
        //     return 'none';
        // }
        return '';
    }

    constructor(private readonly translocoSerivce: TranslocoService) {}
}
