import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { QuizService } from '../../services/quiz.service';
import { QuizDto } from '../../models/QuizDto';

@Component({
  selector: 'app-quiz-list-component',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './quiz-list-component.html',
  styleUrl: './quiz-list-component.scss'
})
export class QuizListComponent implements OnInit {
  private quizService = inject(QuizService);
  quizzes: QuizDto[]=[];

  ngOnInit(): void {
    
    //kad god se kreira komponenta, zovemo back, da napunimo listu
    this.quizService.getQuizzes().subscribe({
      next: (data) => { this.quizzes = data;},



  });

  }

}
