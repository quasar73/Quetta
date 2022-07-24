import { BehaviorSubject, Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class SelectedChatService {
    private readonly selectedChat$ = new BehaviorSubject<string | null>(null);

    constructor() {}

    setId(id: string | null): void {
        this.selectedChat$.next(id);
    }

    getSelectedChat(): Observable<string | null> {
        return this.selectedChat$.asObservable();
    }
}
