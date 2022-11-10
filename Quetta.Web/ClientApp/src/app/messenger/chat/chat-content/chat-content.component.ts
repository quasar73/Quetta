import { MessageApiService } from '@api-services/message/message.service';
import { MessageUpdaterService } from '@services/message-updater/message-updater.service';
import { ClientMessageModel } from '@models/client-message.model';
import { ClipboardService } from 'ngx-clipboard';
import { animate, style, transition, trigger } from '@angular/animations';
import {
    ChangeDetectionStrategy,
    Component,
    ElementRef,
    Input,
    ViewChild,
    ChangeDetectorRef,
    OnInit,
    OnChanges,
    AfterViewInit,
    AfterViewChecked,
    OnDestroy,
} from '@angular/core';
import { TuiScrollbarComponent, TuiAlertService, TuiNotification } from '@taiga-ui/core';
import { Actions, ofActionDispatched, Store } from '@ngxs/store';
import { SelectedNotes } from 'src/app/state-manager/actions/selected-notes.actions';
import { MessageStatus } from '@enums/message-status.enum';
import { delay, of, tap, EMPTY, Subscription } from 'rxjs';
import { NoteReadService } from '@services/note-read/note-read.service';
import { getStatus } from '@utils/status-calculator';
import { ReadWebsocketService } from '@services/websocket/read-websocket/read-websocket.service';

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
export class ChatContentComponent implements OnInit, OnChanges, AfterViewInit, AfterViewChecked, OnDestroy {
    @ViewChild('notesScroll') private readonly notesScroll?: TuiScrollbarComponent;
    @ViewChild('wrap') private readonly wrap?: ElementRef<HTMLElement>;
    @ViewChild('bottomAnchor') private readonly bottomAnchor?: ElementRef<HTMLElement>;

    @Input() incomingMessages!: ClientMessageModel[] | null;
    @Input() chatId!: string | null;

    messages!: ClientMessageModel[] | null;
    isScrollDownButtonVisible = false;
    isSelectingMode = false;
    isMessagesLoading = false;
    hasMoreMessages = true;
    isCanBeScrolled = false;

    private selectedIds: string[] = [];
    private readonly subscriptions = new Subscription();

    constructor(
        private readonly clipboardService: ClipboardService,
        private readonly alertService: TuiAlertService,
        private readonly cdr: ChangeDetectorRef,
        private readonly store: Store,
        private readonly actions: Actions,
        private readonly messageApiService: MessageApiService,
        private readonly messageUpdaterService: MessageUpdaterService,
        private readonly noteReadService: NoteReadService,
        private readonly readWebsocketService: ReadWebsocketService
    ) {}

    ngAfterViewChecked(): void {
        if (this.isCanBeScrolled) {
            if (this.notesScroll?.browserScrollRef.nativeElement) {
                this.notesScroll.browserScrollRef.nativeElement.scrollTop = this.notesScroll.browserScrollRef.nativeElement.scrollHeight;
            }
            this.isCanBeScrolled = false;
        }
    }

    ngOnChanges(): void {
        if (this.incomingMessages) {
            this.messages = [...this.incomingMessages];
        }
    }

    ngOnInit(): void {
        this.initializeSelectingSubscribtions();
        this.initializeMessagesUpdater();
        this.initializeReadService();

        this.subscriptions.add(
            this.readWebsocketService.getReadObservable().subscribe(readModel => {
                readModel.messageIds.forEach(mId => {
                    const index = this.messages?.findIndex(m => m.id === mId);

                    if (index !== undefined && index > -1) {
                        this.messages![index] = {
                            ...this.messages![index],
                            readers: [readModel.reader, ...this.messages![index].readers],
                            status: MessageStatus.Read,
                        };
                    }
                });

                this.cdr.markForCheck();
            })
        );
    }

    ngAfterViewInit(): void {
        of(EMPTY)
            .pipe(
                delay(0),
                tap(() => {
                    if (this.messages?.some(m => m.status == MessageStatus.Unread)) {
                        const index = this.messages.findIndex(m => m.status == MessageStatus.Unread);
                        if (index > -1) {
                            const element = document.getElementById(index.toString());
                            element!.scrollIntoView();
                        }
                    } else {
                        if (this.notesScroll?.browserScrollRef.nativeElement) {
                            this.notesScroll.browserScrollRef.nativeElement.scrollTop =
                                this.notesScroll.browserScrollRef.nativeElement.scrollHeight;
                        }
                    }
                })
            )
            .subscribe();
    }

    ngOnDestroy(): void {
        this.subscriptions.unsubscribe();
    }

    onScroll(): void {
        if (!this.notesScroll || !this.wrap) {
            return;
        }

        const { nativeElement } = this.notesScroll.browserScrollRef;
        const scrollTop = nativeElement.scrollTop;
        const scrollHeight = nativeElement.scrollHeight;
        const wrapHeight = this.wrap.nativeElement.clientHeight;

        this.isScrollDownButtonVisible = scrollHeight - scrollTop - wrapHeight > SCROLL_DOWN_BTN_SHOWS;
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

    onMessageDeleted(message: ClientMessageModel): void {
        this.messageApiService.deleteMessages([message.id ?? '']).subscribe(() => {
            const index = this.messages?.indexOf(message);
            if (index !== undefined && index > -1) {
                this.messages?.splice(index, 1);
                this.cdr.markForCheck();
            }
        });
    }

    onVisibilityChange(event: string, message: ClientMessageModel, index: number): void {
        if (event == 'VISIBLE' && message.status == MessageStatus.Unread && !message.isOwner) {
            this.noteReadService.readMessage(message.id!, index);
        }
    }

    isNextDay(index: number): boolean {
        if (this.messages) {
            if (index === 0) {
                return true;
            }

            const currentDate = new Date(this.messages[index].date);
            const nextDate = new Date(this.messages[index - 1].date);

            return !(
                currentDate.getDate() === nextDate.getDate() &&
                currentDate.getMonth() === nextDate.getMonth() &&
                currentDate.getFullYear() === nextDate.getFullYear()
            );
        }

        return false;
    }

    loadMoreMessages(): void {
        if (this.messages) {
            this.isMessagesLoading = true;
            const lastMessageId = this.messages[0].id;

            this.messageApiService.getMessages(this.chatId, lastMessageId, 10).subscribe(messages => {
                this.messages = [
                    ...(messages?.map(message => {
                        return {
                            ...message,
                            isSelected: false,
                            code: undefined,
                            status: getStatus(message),
                        };
                    }) ?? []),
                    ...this.messages!,
                ];
                this.hasMoreMessages = messages?.length !== 0;
                this.isMessagesLoading = false;
                this.cdr.markForCheck();
            });
        }
    }

    private initializeSelectingSubscribtions(): void {
        this.actions.pipe(ofActionDispatched(SelectedNotes.Clear)).subscribe(() => {
            this.clearSelecting();
        });

        this.actions.pipe(ofActionDispatched(SelectedNotes.Delete)).subscribe(() => {
            this.messages = [...(this.messages?.filter(m => !this.selectedIds.includes(m.id ?? '')) ?? [])];
            this.clearSelecting();
        });

        this.store.dispatch(new SelectedNotes.Clear());
    }

    private initializeMessagesUpdater(): void {
        this.messageUpdaterService.getSentMessage().subscribe(message => {
            if (message) {
                this.messages = [...(this.messages ?? []), message];
                this.cdr.markForCheck();
            }
        });

        this.messageUpdaterService.getAddedMessage().subscribe(model => {
            if (model && this.messages) {
                const index = this.messages.findIndex(m => m.code === model.code);
                if (index > -1) {
                    this.messages[index] = { ...this.messages[index], status: MessageStatus.Unread, id: model.messageId };
                    this.isCanBeScrolled = true;
                    this.cdr.markForCheck();
                }
            }
        });
    }

    private initializeReadService(): void {
        this.noteReadService.getMessageIdAsObservable().subscribe(model => {
            if (model) {
                const message = this.messages!.find(m => m.id == model.noteId);
                const unreadMessages = this.messages?.filter(m => m.date <= message!.date && m.status == MessageStatus.Unread);
                unreadMessages?.forEach(message => {
                    const index = this.messages!.findIndex(m => m.id === message.id);
                    if (index > -1) {
                        this.messages![index] = { ...this.messages![index], status: MessageStatus.Read };
                    }
                });
                this.messageApiService.readMessage(model.noteId).subscribe();
                this.cdr.markForCheck();
            }
        });
    }

    private clearSelecting(): void {
        this.selectedIds = [];
        this.isSelectingMode = false;
        this.messages?.forEach(m => (m.isSelected = false));
        this.messages = [...(this.messages ?? [])];
        this.cdr.markForCheck();
    }
}
