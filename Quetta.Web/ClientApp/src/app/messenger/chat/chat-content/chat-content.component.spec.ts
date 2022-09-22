import { TuiScrollbarModule } from '@taiga-ui/core';
import { NoteComponent } from './note/note.component';
import { MockComponent } from 'ng-mocks';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { Actions, NgxsModule, ofActionDispatched } from '@ngxs/store';
import { Observable } from 'rxjs';
import { ClientMessageModel } from 'src/app/shared/models/client-message.model';
import { SelectedNotes } from 'src/app/state-manager/actions/selected-notes.actions';
import { SelectedNotesState } from 'src/app/state-manager/states/selected-notes.state';
import { testMessages } from 'src/app/testing/data/test-messages';

import { ChatContentComponent } from './chat-content.component';

describe('ChatContentComponent', () => {
    let component: ChatContentComponent;
    let fixture: ComponentFixture<ChatContentComponent>;
    let actions$: Observable<any>;
    const messages: ClientMessageModel[] = testMessages;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [NgxsModule.forRoot([SelectedNotesState]), TuiScrollbarModule],
            declarations: [ChatContentComponent, MockComponent(NoteComponent)],
        }).compileComponents();

        actions$ = TestBed.inject(Actions);
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(ChatContentComponent);
        component = fixture.componentInstance;
        component.messages = [...messages];
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should shows message if empty', () => {
        fixture = TestBed.createComponent(ChatContentComponent);
        component = fixture.componentInstance;
        component.messages = [];
        fixture.detectChanges();

        const emptyMessage = fixture.debugElement.query(By.css('.no-messages-wrap'))?.nativeElement;

        expect(emptyMessage).not.toBeUndefined();
    });

    it('should shows notes if not empty', () => {
        const notes = fixture.debugElement.queryAll(By.css('qtt-note'));

        expect(notes?.length).toBeTruthy();
    });

    it('should dispatch Select action', done => {
        actions$.pipe(ofActionDispatched(SelectedNotes.Select)).subscribe(() => {
            expect(component.messages![0].isSelected).toBeTruthy();
            done();
        });

        component.onMessageSelected(true, messages[0]);
    });

    it('should dispatch Remove action', done => {
        actions$.pipe(ofActionDispatched(SelectedNotes.Remove)).subscribe(() => {
            expect(component.messages![0].isSelected).toBeFalsy();
            done();
        });

        component.onMessageSelected(false, messages[0]);
    });
});
