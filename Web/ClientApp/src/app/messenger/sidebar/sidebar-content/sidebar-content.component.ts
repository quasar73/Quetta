import { ChatItem } from './../../../shared/models/chat-item.model';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'qtt-sidebar-content',
    templateUrl: './sidebar-content.component.html',
    styleUrls: ['./sidebar-content.component.scss'],
})
export class SidebarContentComponent implements OnInit {
    testData: ChatItem[] = [
        {
            id: '1',
            title: 'max ship',
            lastMessage: 'This is last message',
            type: 'dialog',
        },
        {
            id: '2',
            title: 'cc xcox coc',
            lastMessage: 'This is last message',
            type: 'group',
        },
        {
            id: '3',
            title: 'jack kek',
            lastMessage: 'This is last message',
            type: 'channel',
        },
        {
            id: '4',
            title: 'This is very very very very looooooong text to test how title will be cut',
            lastMessage: 'And this is very very veeryyy looooong message to test how last message will be cut',
            type: 'dialog',
        },
    ];
    selectedId!: string | null;

    constructor() {}

    ngOnInit(): void {}

    changeSelection(id: string): void {
        this.selectedId = this.selectedId === id ? null : id;
    }

    isActive(id: string): boolean {
        return id === this.selectedId;
    }
}
