import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestsListDialogComponent } from './requests-list-dialog.component';

describe('RequestsListDialogComponent', () => {
  let component: RequestsListDialogComponent;
  let fixture: ComponentFixture<RequestsListDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequestsListDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RequestsListDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
