import { SelectedNotes } from 'src/app/state-manager/actions/selected-notes.actions';
import { TestBed } from '@angular/core/testing';
import { NgxsModule, Store } from '@ngxs/store';
import { SelectedNotesState } from './selected-notes.state';

describe('SelectedNotes', () => {
    let store: Store;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [NgxsModule.forRoot([SelectedNotesState])],
        });

        store = TestBed.inject(Store);
    });

    it('should add new id', () => {
        store.dispatch(new SelectedNotes.Select('some-id'));

        const ids: string[] = store.selectSnapshot(state => state.selectedNotes.ids);

        expect(ids.length).toBe(1);
    });

    it('should add several ids', () => {
        store.dispatch(new SelectedNotes.Select('some-id1'));
        store.dispatch(new SelectedNotes.Select('some-id2'));
        store.dispatch(new SelectedNotes.Select('some-id3'));

        const ids: string[] = store.selectSnapshot(state => state.selectedNotes.ids);

        expect(ids.length).toBe(3);
    });

    it('should clear ids', () => {
        store.dispatch(new SelectedNotes.Select('some-id1'));
        store.dispatch(new SelectedNotes.Select('some-id2'));
        store.dispatch(new SelectedNotes.Select('some-id3'));

        let ids: string[] = store.selectSnapshot(state => state.selectedNotes.ids);

        expect(ids.length).toBe(3);

        store.dispatch(new SelectedNotes.Clear());

        ids = store.selectSnapshot(state => state.selectedNotes.ids);

        expect(ids.length).toBe(0);
    });

    it('should remove id', () => {
        store.dispatch(new SelectedNotes.Select('some-id1'));
        store.dispatch(new SelectedNotes.Select('some-id2'));

        let ids: string[] = store.selectSnapshot(state => state.selectedNotes.ids);

        expect(ids.length).toBe(2);

        store.dispatch(new SelectedNotes.Remove('some-id2'));

        ids = store.selectSnapshot(state => state.selectedNotes.ids);

        expect(ids.length).toBe(1);
    });

    it('should select ids', () => {
        store.reset({
            ...store.snapshot(),
            selectedNotes: {
                ids: ['some-id1', 'some-id2'],
            },
        });

        let ids: string[] = store.selectSnapshot(state => state.selectedNotes.ids);

        expect(ids).toEqual(['some-id1', 'some-id2']);
    });
});
