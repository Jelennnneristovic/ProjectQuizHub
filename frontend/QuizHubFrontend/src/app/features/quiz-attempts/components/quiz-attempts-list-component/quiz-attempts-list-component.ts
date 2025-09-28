import { Component, inject } from '@angular/core';
import { QuizAttemptDto } from '../../model/QuizAttemptDto';
import { QuizAttemptService } from '../../services/quiz-attempts.service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../auth/services/auth.services';

@Component({
    selector: 'app-quiz-attempts-list-component',
    imports: [CommonModule],
    standalone: true,
    templateUrl: './quiz-attempts-list-component.html',
    styleUrl: './quiz-attempts-list-component.scss',
})
export class QuizAttemptsListComponent {
    private quizAttemptService = inject(QuizAttemptService);
    private authService = inject(AuthService);
    quizAttempts: QuizAttemptDto[] = [];

    ngOnInit(): void {
        this.loadQuizzesAttempts();
    }
    //kad god se kreira komponenta, zovemo back, da napunimo listu
    loadQuizzesAttempts() {
        const context = this.authService.GetCurrentUser();

        if (!context) return;

        if (context.role === 'Admin') {
            this.quizAttemptService.getQuizAttempts().subscribe({
                next: (data) => {
                    this.quizAttempts = data;
                },
            });
        } else {
            // ovo je case u slucaju obicnog usera
            this.quizAttemptService.getQuizAttemptsFromUser().subscribe({
                next: (data) => {
                    this.quizAttempts = data;
                },
            });
        }
    }
}
