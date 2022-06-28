import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'qtt-chat-input',
    templateUrl: './chat-input.component.html',
    styleUrls: ['./chat-input.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatInputComponent {
    constructor() {}
}
