import { SelectedChatService } from './../../shared/services/selected-chat/selected-chat.service';
import { ActivatedRoute } from '@angular/router';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
    selector: 'qtt-chat',
    templateUrl: './chat.component.html',
    styleUrls: ['./chat.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatComponent implements OnInit {
    constructor(private readonly activatedRoute: ActivatedRoute, private readonly selectedChatService: SelectedChatService) {}

    ngOnInit(): void {
        this.selectedChatService.setId(this.activatedRoute.snapshot.paramMap.get('id'));
    }
}
