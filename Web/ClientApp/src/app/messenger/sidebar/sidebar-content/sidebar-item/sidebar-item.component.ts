import { TranslocoService } from '@ngneat/transloco';
import { ChatItem } from './../../../../shared/models/chat-item.model';
import { Component, Input } from '@angular/core';
import { dateCalculator } from 'src/app/shared/utils/date-calculator';

@Component({
    selector: 'qtt-sidebar-item',
    templateUrl: './sidebar-item.component.html',
    styleUrls: ['./sidebar-item.component.scss'],
})
export class SidebarItemComponent {
    @Input() active = false;
    @Input() data!: ChatItem;

    get title(): string {
        return this.data?.title ?? '??';
    }

    get icon(): string {
        switch (this.data?.type) {
            case 'dialog':
                return 'tuiIconUser';
            case 'group':
                return 'tuiIconUsers';
            case 'channel':
                return 'tuiIconComment';
            default:
                return 'tuiIconUser';
        }
    }

    get dateTime(): string {
        if (this.data) {
            return dateCalculator(this.data?.lastMessageDate, this.translocoSerivce);
        } else {
            return 'none';
        }
    }

    constructor(private translocoSerivce: TranslocoService) {}
}
