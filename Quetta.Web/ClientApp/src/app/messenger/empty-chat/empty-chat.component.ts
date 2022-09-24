import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { SelectedChatService } from '@services/selected-chat/selected-chat.service';

@Component({
    selector: 'qtt-empty-chat',
    templateUrl: './empty-chat.component.html',
    styleUrls: ['./empty-chat.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EmptyChatComponent implements OnInit {
    constructor(private readonly selectedChatService: SelectedChatService) {}

    ngOnInit(): void {
        this.selectedChatService.setId(null);
    }
}
