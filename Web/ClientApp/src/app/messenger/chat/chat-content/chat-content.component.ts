import { ChangeDetectionStrategy, Component, ElementRef, ViewChild } from '@angular/core';

@Component({
    selector: 'qtt-chat-content',
    templateUrl: './chat-content.component.html',
    styleUrls: ['./chat-content.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChatContentComponent {
    @ViewChild('notesList') private readonly notesList?: ElementRef<HTMLElement>;

    constructor() {}
}
