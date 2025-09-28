import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaderBoardDialogComponent } from './leader-board-dialog-component';

describe('LeaderBoardDialogComponent', () => {
  let component: LeaderBoardDialogComponent;
  let fixture: ComponentFixture<LeaderBoardDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeaderBoardDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeaderBoardDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
