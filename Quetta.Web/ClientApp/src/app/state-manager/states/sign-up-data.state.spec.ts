import { SignUpDataStateModel } from './../models/sign-up-data.model';
import { SignUpDataState } from './sign-up-data.state';
import { TestBed } from '@angular/core/testing';
import { NgxsModule, Store } from '@ngxs/store';
import { SignUpData } from '../actions/sign-up-data.actions';

describe('SignUpDataState', () => {
    let store: Store;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [NgxsModule.forRoot([SignUpDataState])],
        });

        store = TestBed.inject(Store);
    });

    it('should set sign-up data', () => {
        const expectedData = {
            firstName: 'TestFirstName',
            lastName: 'TestLastName',
            username: 'testUserName',
            idToken: 'testidToken',
        };
        store.dispatch(new SignUpData.Set(expectedData));

        const model: SignUpDataStateModel | null = store.selectSnapshot(state => state.signUpData);

        expect(model).not.toBeNull();
        expect(model).toEqual(expectedData);
    });

    it('should delete sign-up data', () => {
        store.dispatch(new SignUpData.Delete());

        const model: SignUpDataStateModel | null = store.selectSnapshot(state => state.signUpData);

        expect(model).toBeNull();
    });

    it('should select sign-up data', () => {
        store.reset({
            ...store.snapshot(),
            signUpData: {
                firstName: 'TestFirstName',
                lastName: 'TestLastName',
                username: 'testUserName',
                idToken: 'testidToken',
            },
        });

        let model: SignUpDataStateModel | null = store.selectSnapshot(state => state.signUpData);

        expect(model).toEqual({
            firstName: 'TestFirstName',
            lastName: 'TestLastName',
            username: 'testUserName',
            idToken: 'testidToken',
        });
    });
});
