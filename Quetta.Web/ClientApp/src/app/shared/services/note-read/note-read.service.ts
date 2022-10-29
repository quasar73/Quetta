import { Injectable } from '@angular/core';
import { Subject, Observable, buffer, debounceTime, map } from 'rxjs';

export interface ReadNoteModel {
    noteId: string;
    index: number;
}

@Injectable({ providedIn: 'root' })
export class NoteReadService {
    private readonly noteId$ = new Subject<ReadNoteModel>();
    private readonly noteIdEmmited$ = new Subject<void>();

    readMessage(noteId: string, index: number): void {
        this.noteId$.next({ noteId, index });
        this.noteIdEmmited$.next();
    }

    getMessageIdAsObservable(): Observable<ReadNoteModel | null> {
        return this.noteId$.asObservable().pipe(
            buffer(this.noteIdEmmited$.pipe(debounceTime(300))),
            map(models => {
                const maxInedx = Math.max(...models.map(m => m.index));
                return models.find(m => m.index === maxInedx) ?? null;
            })
        );
    }
}
