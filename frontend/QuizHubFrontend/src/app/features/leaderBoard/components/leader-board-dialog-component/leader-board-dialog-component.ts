import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatSelectModule } from '@angular/material/select';
import { LeaderBoardService } from '../../services/leaderBoard.service';
import { LeaderBoardEntriesDto } from '../../models/LeaderBoardEntriesDto';
import { QuizDto } from '../../../quiz/models/QuizDto';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-leader-board-dialog-component',
    standalone: true,
    imports: [CommonModule, MatDialogModule, MatButtonModule, MatTableModule, MatSelectModule, FormsModule],
    templateUrl: './leader-board-dialog-component.html',
    styleUrl: './leader-board-dialog-component.scss',
})
export class LeaderBoardDialogComponent {
    leaderboard?: LeaderBoardEntriesDto;
    displayedColumns: string[] = ['position', 'username', 'score', 'timeElapsedMin', 'completedAt'];

    private leaderboardService = inject(LeaderBoardService);
    public dialogRef = inject(MatDialogRef<LeaderBoardDialogComponent>);
    public data = inject(MAT_DIALOG_DATA) as QuizDto;
    selectedPeriod: '' | 'weekly' | 'monthly' = '';

    ngOnInit(): void {
        this.leaderboardService.getLeaderBoard(this.data.id).subscribe({
            next: (res) => {
                this.leaderboard = res;
            },
        });
    }
    loadLeaderboard() {
        this.leaderboardService
            .getLeaderBoard(this.data.id, this.selectedPeriod !== '' ? this.selectedPeriod : undefined)
            .subscribe({
                next: (res) => {
                    this.leaderboard = res;
                },
                error: () => {},
            });
    }
    close() {
        this.dialogRef.close();
    }
}
