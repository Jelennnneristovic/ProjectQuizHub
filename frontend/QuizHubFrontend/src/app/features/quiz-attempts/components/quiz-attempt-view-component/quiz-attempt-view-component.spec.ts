import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizAttemptViewComponent } from './quiz-attempt-view-component';

describe('QuizAttemptViewComponent', () => {
  let component: QuizAttemptViewComponent;
  let fixture: ComponentFixture<QuizAttemptViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuizAttemptViewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuizAttemptViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
