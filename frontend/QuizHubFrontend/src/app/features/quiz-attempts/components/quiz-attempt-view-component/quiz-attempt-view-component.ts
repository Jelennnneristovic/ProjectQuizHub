import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { UserQuizAttemptDto } from '../../model/UserQuizAttemptDto';
import { AttemptAnswerService } from '../../services/attempt-answer.service';
import { QuizAttemptService } from '../../services/quiz-attempts.service';
import { ToastService } from '../../../../shared/services/toast.service';
import { UserQuizAttemptQuestionDto } from '../../model/UserQuizAttemptQuestionDto';
import { Router, RouterModule } from '@angular/router';

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

    private attemptAnswerService = inject(AttemptAnswerService);
    private quizAttemptService = inject(QuizAttemptService);
    private toastService = inject(ToastService);
    private router = inject(Router);

    userQuizAttempt?: UserQuizAttemptDto;

    selectedOptions: { [questionId: number]: number[] } = {};

    ngOnInit(): void {
        this.userQuizAttempt = this.data;
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
        const dto = {
            quizId: this.userQuizAttempt?.quizId ?? -1,
            quizAttemptId: this.userQuizAttempt?.quizAttemptId ?? -1,
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
        if (!this.userQuizAttempt) return;

        this.quizAttemptService.finishQuizAttempt(this.userQuizAttempt.quizAttemptId).subscribe({
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
