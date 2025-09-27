import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizAttemptsListComponent } from './quiz-attempts-list-component';

describe('QuizAttemptsListComponent', () => {
  let component: QuizAttemptsListComponent;
  let fixture: ComponentFixture<QuizAttemptsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuizAttemptsListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuizAttemptsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
