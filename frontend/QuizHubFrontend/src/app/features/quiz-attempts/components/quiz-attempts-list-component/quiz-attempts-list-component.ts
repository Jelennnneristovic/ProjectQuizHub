import { Component, inject } from '@angular/core';
import { QuizAttemptDto } from '../../model/QuizAttemptDto';
import { QuizAttemptService } from '../../services/quiz-attempts.service';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-quiz-attempts-list-component',
    imports: [CommonModule],
    standalone: true,
    templateUrl: './quiz-attempts-list-component.html',
    styleUrl: './quiz-attempts-list-component.scss',
})
export class QuizAttemptsListComponent {
    private quizAttemptService = inject(QuizAttemptService);
    quizAttempts: QuizAttemptDto[] = [];

    ngOnInit(): void {
        //kad god se kreira komponenta, zovemo back, da napunimo listu
        this.quizAttemptService.getQuizAttempts().subscribe({
            next: (data) => {
                this.quizAttempts = data;
            },
        });
    }
}
