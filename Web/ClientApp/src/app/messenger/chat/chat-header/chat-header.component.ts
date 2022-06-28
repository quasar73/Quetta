import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'qtt-chat-header',
    templateUrl: './chat-header.component.html',
    styleUrls: ['./chat-header.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatHeaderComponent {
    constructor() {}
}
