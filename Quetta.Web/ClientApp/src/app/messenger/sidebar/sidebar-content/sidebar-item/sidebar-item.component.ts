import { ChatItemModel } from '@api-models/chat-item.model';
import { TranslocoService } from '@ngneat/transloco';
import { ChangeDetectionStrategy, Component, Input, OnInit, ChangeDetectorRef } from '@angular/core';
import { ChatType } from '@enums/chat-type.enum';
import { ChatUnreadModel, ChatUnreadService } from '@services/chat-unread/chat-unread.service';

@Component({
    selector: 'qtt-sidebar-item',
    templateUrl: './sidebar-item.component.html',
    styleUrls: ['./sidebar-item.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SidebarItemComponent implements OnInit {
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

    constructor(
        private readonly translocoSerivce: TranslocoService,
        private readonly chatUnreadService: ChatUnreadService,
        private readonly cdr: ChangeDetectorRef
    ) {}

    ngOnInit(): void {
        this.chatUnreadService.addChat(this.data.id);
        this.chatUnreadService.getChatAsObservable(this.data.id)?.subscribe((model: ChatUnreadModel) => {
            this.data = { ...this.data, amountOfUnread: model.amount, lastMessage: model.text ?? this.data.lastMessage };
            this.cdr.markForCheck();
        });
    }
}
