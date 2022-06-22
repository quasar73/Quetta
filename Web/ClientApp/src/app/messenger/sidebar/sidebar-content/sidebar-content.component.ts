import { ChatItem } from './../../../shared/models/chat-item.model';
import { Component } from '@angular/core';

@Component({
    selector: 'qtt-sidebar-content',
    templateUrl: './sidebar-content.component.html',
    styleUrls: ['./sidebar-content.component.scss'],
})
export class SidebarContentComponent {
    testData: ChatItem[] = [
        {
            id: '1',
            title: 'max ship',
            lastMessage: 'This is last message',
            type: 'dialog',
            lastMessageDate: new Date('2020-01-01'),
        },
        {
            id: '2',
            title: 'cc xcox coc',
            lastMessage: 'This is last message',
            type: 'group',
            lastMessageDate: new Date('2022-05-08 7:57'),
        },
        {
            id: '3',
            title: 'jack kek',
            lastMessage: 'This is last message',
            type: 'channel',
            lastMessageDate: new Date(),
        },
        {
            id: '4',
            title: 'This is very very very very looooooong text to test how title will be cut',
            lastMessage: 'And this is very very veeryyy looooong message to test how last message will be cut',
            type: 'dialog',
            lastMessageDate: new Date('2022-04-20'),
        },
    ];
    selectedId!: string | null;

    constructor() {}

    changeSelection(id: string): void {
        this.selectedId = this.selectedId === id ? null : id;
    }

    isActive(id: string): boolean {
        return id === this.selectedId;
    }
}
