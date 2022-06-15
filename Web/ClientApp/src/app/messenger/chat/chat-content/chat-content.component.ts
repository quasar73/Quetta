import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'qtt-chat-content',
    templateUrl: './chat-content.component.html',
    styleUrls: ['./chat-content.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatContentComponent {
    constructor() {}
}
