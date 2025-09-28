import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { QuizService } from '../../services/quiz.service';
import { QuizDto } from '../../models/QuizDto';

import { MatDialog } from '@angular/material/dialog';
import { CreateQuizDto } from '../../models/CreateQuizDto';
import { UpdateQuizDto } from '../../models/UpdateQuizDto';
import { QuizModalComponent } from '../quiz-modal-component/quiz-modal-component';
import { ConfirmModalComponent } from '../../../../shared/components/confirm-modal-component/confirm-modal-component';

@Component({
    selector: 'app-quiz-list-component',
    imports: [CommonModule, ConfirmModalComponent],

    standalone: true,
    templateUrl: './quiz-list-component.html',
    styleUrl: './quiz-list-component.scss',
})
export class QuizListComponent implements OnInit {
    private quizService = inject(QuizService);
    private dialog = inject(MatDialog);

    quizzes: QuizDto[] = [];
    quizToDelete?: QuizDto;

    ngOnInit(): void {
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
            console.log('Saved:', dto);
            this.quizService.updateQuiz(dto as UpdateQuizDto).subscribe({
                next: () => {
                    this.loadQuizzes();
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
            console.log('Created:', dto);
            this.quizService.createQuiz(dto as CreateQuizDto).subscribe({
                next: () => {
                    this.loadQuizzes();
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
            next: () => {
                this.loadQuizzes();
                this.quizToDelete = undefined;
            },
        });
    }

    onDetails(quiz: QuizDto) {}
}
