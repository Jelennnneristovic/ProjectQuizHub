import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { QuizService } from '../../services/quiz.service';
import { QuizDto } from '../../models/QuizDto';

import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CreateQuizDto } from '../../models/CreateQuizDto';
import { UpdateQuizDto } from '../../models/UpdateQuizDto';
import { QuizModalComponent } from '../quiz-modal-component/quiz-modal-component';
import { ConfirmModalComponent } from '../../../../shared/components/confirm-modal-component/confirm-modal-component';
import { ToastService } from '../../../../shared/services/toast.service';
import { AuthService } from '../../../auth/services/auth.services';
import { FormsModule } from '@angular/forms';
import { DifficultyLevel } from '../../models/DifficultyLevel';
import { RouterModule } from '@angular/router';
import { QuizAttemptService } from '../../../quiz-attempts/services/quiz-attempts.service';
import { QuizAttemptViewComponent } from '../../../quiz-attempts/components/quiz-attempt-view-component/quiz-attempt-view-component';

@Component({
    selector: 'app-quiz-list-component',
    imports: [CommonModule, ConfirmModalComponent, FormsModule, RouterModule, MatDialogModule],

    standalone: true,
    templateUrl: './quiz-list-component.html',
    styleUrl: './quiz-list-component.scss',
})
export class QuizListComponent implements OnInit {
    private quizService = inject(QuizService);
    private authService = inject(AuthService);
    private dialog = inject(MatDialog);
    private toastServise = inject(ToastService);
    private quizAttemptService = inject(QuizAttemptService);
    role: string = 'User';

    quizzes: QuizDto[] = [];
    quizToDelete?: QuizDto;

    keyword = '';
    categoryName = '';
    difficultyLevel: DifficultyLevel | null = null;

    ngOnInit(): void {
        const context = this.authService.GetCurrentUser();
        if (!context) return;

        this.role = context.role;
        this.loadQuizzes();
    }

    loadQuizzes() {
        this.quizService.getQuizzes().subscribe({
            next: (data) => {
                this.quizzes = data;
            },
        });
    }
    openEditQuizModal(quiz: QuizDto) {
        const dialogRef = this.dialog.open(QuizModalComponent, {
            width: '500px',
            height: '600px',
            data: { quiz, isEdit: true }, // Prosleđujemo podatke ovdje
        });

        // subscribe na save EventEmitter
        dialogRef.componentInstance.save.subscribe((dto: CreateQuizDto | UpdateQuizDto) => {
            this.quizService.updateQuiz(dto as UpdateQuizDto).subscribe({
                next: () => {
                    this.loadQuizzes();
                },
                error: (err) => {
                    this.toastServise.error(err.error, 3000);
                },
            });
        });
    }

    openAddQuizModal() {
        const dialogRef = this.dialog.open(QuizModalComponent, {
            width: '500px',
            height: '600px',
            data: { isEdit: false },
        });

        dialogRef.componentInstance.save.subscribe((dto: CreateQuizDto | UpdateQuizDto) => {
            this.quizService.createQuiz(dto as CreateQuizDto).subscribe({
                next: () => {
                    this.loadQuizzes();
                },
                error: (err) => {
                    this.toastServise.error(err.error, 3000);
                },
            });
        });
    }

    openDeleteQuizConfirm(quiz: QuizDto) {
        this.quizToDelete = quiz;
    }
    cancelDelete() {
        this.quizToDelete = undefined;
    }
    deleteQuiz() {
        if (!this.quizToDelete) return;

        this.quizService.deleteQuiz(this.quizToDelete.id).subscribe({
            next: (data) => {
                this.toastServise.success(data);

                this.loadQuizzes();
                this.quizToDelete = undefined;
            },
        });
    }

    startQuiz(quiz: QuizDto) {
        this.quizAttemptService.createQuizAttempt({ quizId: quiz.id }).subscribe({
            next: (attempt) => {
                const dialogRef = this.dialog.open(QuizAttemptViewComponent, {
                    width: '800px',
                    data: attempt,
                    disableClose: true,
                });

                dialogRef.beforeClosed().subscribe((result) => {
                    if (!result?.finished) {
                        alert('Kviz nije završen! Ako zatvorite modal, napredak neće biti sačuvan.');
                    }
                });
            },
            error: (err) => console.error(err),
        });
    }

    onSearchByKeyword() {
        if (!this.keyword.trim()) {
            this.loadQuizzes();
            return;
        }
        this.quizService.searchByKeyword(this.keyword).subscribe({
            next: (data) => {
                this.quizzes = data;
            },
        });
    }
    onSearchFilters() {
        const diff = this.difficultyLevel || undefined;
        this.quizService.searchQuizzes(diff as any, this.categoryName || undefined).subscribe({
            next: (data) => {
                this.quizzes = data;
            },
        });
    }
}
