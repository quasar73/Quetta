import { ChatItemModel } from './../../../shared/api-models/chat-item.model';
import { Observable } from 'rxjs';
import { ChatService } from './../../../shared/services/api/chat/chat.service';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
    selector: 'qtt-sidebar-content',
    templateUrl: './sidebar-content.component.html',
    styleUrls: ['./sidebar-content.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SidebarContentComponent implements OnInit {
    selectedId!: string | null;
    chats$!: Observable<ChatItemModel[] | null>;

    constructor(private readonly chatService: ChatService) {}

    ngOnInit(): void {
        this.chats$ = this.chatService.getChats();
    }

    changeSelection(id: string): void {
        this.selectedId = this.selectedId === id ? null : id;
    }

    isActive(id: string): boolean {
        return id === this.selectedId;
    }
}
