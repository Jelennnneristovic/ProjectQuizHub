import { Component, EventEmitter, inject, Output } from '@angular/core';
import { QuizDto } from '../../../quiz/models/QuizDto';
import { QuizService } from '../../../quiz/services/quiz.service';
import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { LeaderBoardDialogComponent } from '../leader-board-dialog-component/leader-board-dialog-component';

@Component({
    selector: 'app-quiz-table-component',
    imports: [CommonModule],
    standalone: true,
    templateUrl: './quiz-table-component.html',
    styleUrl: './quiz-table-component.scss',
})
export class QuizTableComponent {
    private quizService = inject(QuizService);
    private dialog = inject(MatDialog);
    quizzes: QuizDto[] = [];

    ngOnInit(): void {
        //kad god se kreira komponenta, zovemo back, da napunimo listu
        this.quizService.getQuizzes().subscribe({
            next: (data) => {
                this.quizzes = data;
            },
        });
    }
    openLeaderboard(quiz: QuizDto) {
        this.dialog.open(LeaderBoardDialogComponent, {
            width: '600px',
            data: quiz,
        });
    }
}
