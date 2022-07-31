import { SelectedChatService } from './../../../shared/services/selected-chat/selected-chat.service';
import { Router } from '@angular/router';
import { ChatItemModel } from './../../../shared/api-models/chat-item.model';
import { Observable } from 'rxjs';
import { ChatService } from './../../../shared/services/api/chat/chat.service';
import { ChangeDetectionStrategy, Component, OnInit, ChangeDetectorRef } from '@angular/core';

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
        private readonly chatService: ChatService,
        private readonly router: Router,
        private readonly selectedChatService: SelectedChatService,
        private readonly cdr: ChangeDetectorRef
    ) {}

    ngOnInit(): void {
        this.chats$ = this.chatService.getChats();

        this.selectedChatService.getSelectedChat().subscribe(id => {
            this.selectedId = id;
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
