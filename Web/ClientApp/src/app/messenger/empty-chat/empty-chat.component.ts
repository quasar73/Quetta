import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
    selector: 'qtt-empty-chat',
    templateUrl: './empty-chat.component.html',
    styleUrls: ['./empty-chat.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EmptyChatComponent {
    constructor() {}
}
