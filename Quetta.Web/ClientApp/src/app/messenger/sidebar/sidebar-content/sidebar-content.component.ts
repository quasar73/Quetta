import { Observable } from 'rxjs';
import { SelectedChatService } from '@services/selected-chat/selected-chat.service';
import { Router } from '@angular/router';
import { ChatItemModel } from '@api-models/chat-item.model';
import { ChatApiService } from '@api-services/chat/chat.service';
import { ChangeDetectionStrategy, Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { SidebarWebsocketService } from '@services/websocket/sidebar-websocket/sidebar-websocket.service';
import { ChatUnreadService } from '@services/chat-unread/chat-unread.service';

@Component({
    selector: 'qtt-sidebar-content',
    templateUrl: './sidebar-content.component.html',
    styleUrls: ['./sidebar-content.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SidebarContentComponent implements OnInit {
    selectedId!: string | null;
    chats$!: Observable<ChatItemModel[] | null>;

    constructor(
        private readonly chatService: ChatApiService,
        private readonly router: Router,
        private readonly selectedChatService: SelectedChatService,
        private readonly sidebarWebsocketService: SidebarWebsocketService,
        private readonly chatUnreadService: ChatUnreadService,
        private readonly cdr: ChangeDetectorRef
    ) {}

    ngOnInit(): void {
        this.chats$ = this.chatService.getChats();

        this.selectedChatService.getSelectedChat().subscribe(id => {
            this.selectedId = id;
            this.cdr.markForCheck();
        });

        this.sidebarWebsocketService.startConnection().subscribe(() => {
            this.sidebarWebsocketService.addNotificationsListner();
        });

        this.sidebarWebsocketService.getSidebarObservable().subscribe(res => {
            this.chatUnreadService.updateChat(res.chatId, res.amount, res.text);
            this.cdr.markForCheck();
        });
    }

    changeSelection(id: string): void {
        this.router.navigate([this.router.url, id]);
    }

    isActive(id: string): boolean {
        return id === this.selectedId;
    }
}
