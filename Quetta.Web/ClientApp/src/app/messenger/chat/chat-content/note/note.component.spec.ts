import { ReactiveFormsModule } from '@angular/forms';
import { TuiSvgModule } from '@taiga-ui/core';
import { TokenStorage } from './../../../../shared/services/auth/token-storage.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AuthenticationService } from 'src/app/shared/services/auth/authentication.service';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoteComponent } from './note.component';
import { MessageStatus } from 'src/app/shared/enums/message-status.enum';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { TuiCheckboxModule, TuiDropdownContextModule } from '@taiga-ui/kit';

describe('NoteComponent', () => {
    let component: NoteComponent;
    let fixture: ComponentFixture<NoteComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                HttpClientTestingModule,
                NoopAnimationsModule,
                TuiCheckboxModule,
                TuiSvgModule,
                TuiDropdownContextModule,
                ReactiveFormsModule,
            ],
            declarations: [NoteComponent],
            providers: [AuthenticationService, TokenStorage],
        }).compileComponents();

        fixture = TestBed.createComponent(NoteComponent);
        component = fixture.componentInstance;
        component.message = {
            code: '',
            isSelected: false,
            text: '',
            username: '',
            date: '',
            status: MessageStatus.Unreaded,
        };
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
