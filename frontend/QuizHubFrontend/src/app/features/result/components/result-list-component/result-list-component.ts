import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ResultService } from '../../services/result.service';
import { AuthService } from '../../../auth/services/auth.services';
import { ResultDto } from '../../models/ResultDto';
import { RouterModule } from '@angular/router';

@Component({
    selector: 'app-result-list-component',
    imports: [CommonModule, RouterModule],
    standalone: true,
    templateUrl: './result-list-component.html',
    styleUrl: './result-list-component.scss',
})
export class ResultListComponent {
    results: ResultDto[] = [];
    role: string = 'User';

    private authService = inject(AuthService);
    private resultService = inject(ResultService);

    ngOnInit(): void {
        const context = this.authService.GetCurrentUser();
        if (!context) return;
        this.role = context.role;
        this.loadResults();
    }
    loadResults() {
        if (this.role == 'User') {
            this.resultService.getResultsByUser().subscribe({
                next: (data) => {
                    this.results = data;
                },
            });
        } else {
            this.resultService.getResults().subscribe({
                next: (data) => {
                    this.results = data;
                },
            });
        }
    }
}
