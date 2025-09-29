import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { UserQuizAttemptDto } from '../../model/UserQuizAttemptDto';
import { AttemptAnswerService } from '../../services/attempt-answer.service';
import { QuizAttemptService } from '../../services/quiz-attempts.service';
import { ToastService } from '../../../../shared/services/toast.service';
import { UserQuizAttemptQuestionDto } from '../../model/UserQuizAttemptQuestionDto';
import { Router, RouterModule } from '@angular/router';
import { interval } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
    selector: 'app-quiz-attempt-view-component',
    standalone: true,
    imports: [CommonModule, MatDialogModule, FormsModule, RouterModule],
    templateUrl: './quiz-attempt-view-component.html',
    styleUrl: './quiz-attempt-view-component.scss',
})
export class QuizAttemptViewComponent implements OnInit {
    public dialogRef = inject(MatDialogRef<QuizAttemptViewComponent>);
    public data = inject(MAT_DIALOG_DATA) as UserQuizAttemptDto;
    private destroyRef = inject(DestroyRef);

    private attemptAnswerService = inject(AttemptAnswerService);
    private quizAttemptService = inject(QuizAttemptService);
    private toastService = inject(ToastService);
    private router = inject(Router);

    userQuizAttempt = signal<UserQuizAttemptDto | null>(null);

    selectedOptions: { [questionId: number]: number[] } = {};

    timer = signal<number>(0); // sekunde preostale

    ngOnInit(): void {
        this.userQuizAttempt.set(this.data);

        // Postavi timer u sekundama
        if (this.userQuizAttempt()?.timeLimit) {
            this.timer.set(this.userQuizAttempt()!.timeLimit * 60);

            // Pokreni interval tajmer
            interval(1000)
                .pipe(takeUntilDestroyed(this.destroyRef))
                .subscribe(() => {
                    const current = this.timer();
                    if (current > 0) {
                        this.timer.set(current - 1);
                    } else {
                        this.finishQuiz(); // automatski završi kada istekne vreme
                    }
                });
        }
    }

    get formattedTime(): string {
        const total = this.timer();
        const minutes = Math.floor(total / 60);
        const seconds = total % 60;
        return `${minutes}:${seconds < 10 ? '0' : ''}${seconds}`;
    }

    // Handler za checkbox promene
    onOptionChange(question: UserQuizAttemptQuestionDto, optionId: number, checked: boolean) {
        const current = this.selectedOptions[question.id] || [];

        if (checked) {
            // Dodaj opciju ako je čekirana
            this.selectedOptions[question.id] = [...current, optionId];
        } else {
            // Izbaci opciju ako je odčekirana
            this.selectedOptions[question.id] = current.filter((id) => id !== optionId);
        }
    }

    submitAnswer(question: UserQuizAttemptQuestionDto, selectedOptionIds: number[], fillInAnswer?: string) {
        const attempt = this.userQuizAttempt();
        if (!attempt) return;

        const dto = {
            quizId: attempt.quizId ?? -1,
            quizAttemptId: attempt.quizAttemptId ?? -1,
            questionId: question.id,
            fillInAnswer: fillInAnswer,
            attemptAnswerOptions: selectedOptionIds,
        };

        this.attemptAnswerService.createAttemptAnswer(dto).subscribe({
            next: (data) => {
                this.toastService.info(data, 3000);
                (question as any).answered = true;
            },
            error: (err) => console.error(err),
        });
    }

    finishQuiz() {
        const attempt = this.userQuizAttempt();
        if (!attempt) return;

        this.quizAttemptService.finishQuizAttempt(attempt.quizAttemptId).subscribe({
            next: (result) => {
                this.toastService.info('Quiz is finished with score ' + result.score, 3000);
                this.dialogRef.close({ finished: true });

                this.router.navigate([`/user/homepage/results/${result.id}`]);
            },
            error: (err) => {
                console.error(err);
            },
        });
    }

    closeModal() {
        const confirmClose = confirm('Da li ste sigurni da želite da izađete? Napredak neće biti sačuvan.');
        if (confirmClose) {
            this.dialogRef.close({ finished: false });
        }
    }
}
