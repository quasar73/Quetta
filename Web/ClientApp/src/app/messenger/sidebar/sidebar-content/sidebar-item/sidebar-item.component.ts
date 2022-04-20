import { ChatItem } from './../../../../shared/models/chat-item.model';
import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: 'qtt-sidebar-item',
    templateUrl: './sidebar-item.component.html',
    styleUrls: ['./sidebar-item.component.scss'],
})
export class SidebarItemComponent implements OnInit {
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

    constructor() {}

    ngOnInit(): void {}
}
