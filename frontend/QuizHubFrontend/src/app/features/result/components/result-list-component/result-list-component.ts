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
    private authService = inject(AuthService);
    private resultService = inject(ResultService);

    ngOnInit(): void {
        this.loadResults();
    }
    loadResults() {
        this.resultService.getResults().subscribe({
            next: (data) => {
                this.results = data;
            },
        });
    }
}
