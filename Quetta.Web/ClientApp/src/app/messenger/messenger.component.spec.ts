import { SidebarComponent } from './sidebar/sidebar.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MessengerComponent } from './messenger.component';
import { MockComponent } from 'ng-mocks';

describe('MessengerComponent', () => {
    let component: MessengerComponent;
    let fixture: ComponentFixture<MessengerComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [MessengerComponent, MockComponent(SidebarComponent)],
            imports: [RouterTestingModule],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(MessengerComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
