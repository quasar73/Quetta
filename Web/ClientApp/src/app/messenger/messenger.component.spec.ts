import { RouterTestingModule } from '@angular/router/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MessengerComponent } from './messenger.component';
import { Component } from '@angular/core';

describe('MessengerComponent', () => {
    let component: MessengerComponent;
    let fixture: ComponentFixture<MessengerComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [MessengerComponent, MockSidebarComponent],
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

@Component({
    selector: 'qtt-sidebar',
    template: '',
})
class MockSidebarComponent {}
