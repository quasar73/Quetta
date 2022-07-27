import { MessageModel } from './../../../shared/api-models/message.model';
import { animate, style, transition, trigger } from '@angular/animations';
import { ChangeDetectionStrategy, Component, ElementRef, Input, ViewChild } from '@angular/core';
import { TuiScrollbarComponent } from '@taiga-ui/core';

const SCROLL_DOWN_BTN_SHOWS = 256;

@Component({
    selector: 'qtt-chat-content',
    templateUrl: './chat-content.component.html',
    styleUrls: ['./chat-content.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: [
        trigger('scrollDownButtonTrigger', [
            transition(':enter', [
                style({ transform: 'translateY(100%)', opacity: 0 }),
                animate('200ms ease-out', style({ transform: 'translateY(0%)', opacity: 1 })),
            ]),
            transition(':leave', [
                style({ transform: 'translateY(0%)', opacity: 1 }),
                animate('200ms ease-in', style({ transform: 'translateY(100%)', opacity: 0 })),
            ]),
        ]),
    ],
})
export class ChatContentComponent {
    @ViewChild('notesList') private readonly notesList?: ElementRef<HTMLElement>;
    @ViewChild('wrap') private readonly wrap?: ElementRef<HTMLElement>;
    @ViewChild(TuiScrollbarComponent, { read: ElementRef }) private readonly scrollBar?: ElementRef<HTMLElement>;

    @Input() messages!: MessageModel[] | null;

    scrollDownButtonVisible = false;

    constructor() {}

    onScroll(): void {
        if (this.scrollBar && this.notesList && this.wrap) {
            const scrollTop = this.scrollBar.nativeElement.scrollTop;
            const scrollHeight = this.notesList.nativeElement.scrollHeight;
            const wrapHeight = this.wrap.nativeElement.clientHeight;

            this.scrollDownButtonVisible = scrollHeight - scrollTop - wrapHeight > SCROLL_DOWN_BTN_SHOWS;
        }
    }

    scrollDown(anchro: HTMLElement): void {
        anchro.scrollIntoView({ behavior: 'smooth', block: 'start', inline: 'nearest' });
    }
}
