import { ClientMessageModel } from '@models/client-message.model';
import { ClipboardService } from 'ngx-clipboard';
import { animate, style, transition, trigger } from '@angular/animations';
import { ChangeDetectionStrategy, Component, ElementRef, Input, ViewChild, ChangeDetectorRef, OnInit } from '@angular/core';
import { TuiScrollbarComponent, TuiAlertService, TuiNotification } from '@taiga-ui/core';
import { Actions, ofActionDispatched, Store } from '@ngxs/store';
import { SelectedNotes } from 'src/app/state-manager/actions/selected-notes.actions';

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
export class ChatContentComponent implements OnInit {
    @ViewChild('notesList') private readonly notesList?: ElementRef<HTMLElement>;
    @ViewChild('wrap') private readonly wrap?: ElementRef<HTMLElement>;
    @ViewChild('bottomAnchor') private readonly bottomAnchor?: ElementRef<HTMLElement>;
    @ViewChild(TuiScrollbarComponent, { read: ElementRef }) private readonly scrollBar?: ElementRef<HTMLElement>;

    @Input() messages!: ClientMessageModel[] | null;

    scrollDownButtonVisible = false;
    isSelectingMode = false;

    private selectedIds: string[] = [];

    constructor(
        private readonly clipboardService: ClipboardService,
        private readonly alertService: TuiAlertService,
        private readonly cdr: ChangeDetectorRef,
        private readonly store: Store,
        private readonly actions: Actions
    ) {}

    ngOnInit(): void {
        this.actions.pipe(ofActionDispatched(SelectedNotes.Clear)).subscribe(() => {
            this.clearSelecting();
        });
        this.actions.pipe(ofActionDispatched(SelectedNotes.Delete)).subscribe(() => {
            console.log(this.messages);
            this.messages = [...(this.messages?.filter(m => !this.selectedIds.includes(m.id ?? '')) ?? [])];
            console.log(this.messages);
            this.clearSelecting();
        });
        this.store.dispatch(new SelectedNotes.Clear());
    }

    onScroll(): void {
        if (this.scrollBar && this.notesList && this.wrap) {
            const scrollTop = this.scrollBar.nativeElement.scrollTop;
            const scrollHeight = this.notesList.nativeElement.scrollHeight;
            const wrapHeight = this.wrap.nativeElement.clientHeight;

            this.scrollDownButtonVisible = scrollHeight - scrollTop - wrapHeight > SCROLL_DOWN_BTN_SHOWS;
        }
    }

    scrollDown(): void {
        this.bottomAnchor?.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'start', inline: 'nearest' });
    }

    onMessageCopied(text: string): void {
        this.clipboardService.copy(text);
        this.alertService
            .open('Text successfully copied!', {
                status: TuiNotification.Info,
            })
            .subscribe();
    }

    onMessageSelected(isSelected: boolean, message: ClientMessageModel): void {
        if (this.messages) {
            const index = this.messages.indexOf(message);

            if (index > -1 && this.messages) {
                this.messages[index] = { ...this.messages[index], isSelected: isSelected };
                this.messages = [...this.messages];
                this.isSelectingMode = this.messages.some(m => m.isSelected);
                isSelected
                    ? this.store.dispatch(new SelectedNotes.Select(message.id))
                    : this.store.dispatch(new SelectedNotes.Remove(message.id));

                const {
                    selectedNotes: { ids },
                } = this.store.snapshot();
                this.selectedIds = [...ids];

                this.cdr.markForCheck();
            }
        }
    }

    isNextDay(index: number): boolean {
        if (this.messages) {
            if (index === this.messages.length - 1) {
                return this.messages.length > 1;
            }

            const currentDate = new Date(this.messages[index].date);
            const nextDate = new Date(this.messages[index + 1].date);

            return !(
                currentDate.getDate() === nextDate.getDate() &&
                currentDate.getMonth() === nextDate.getMonth() &&
                currentDate.getFullYear() === nextDate.getFullYear()
            );
        }

        return false;
    }

    private clearSelecting(): void {
        this.selectedIds = [];
        this.isSelectingMode = false;
        this.messages?.forEach(m => (m.isSelected = false));
        this.messages = [...(this.messages ?? [])];
        this.cdr.markForCheck();
    }
}
