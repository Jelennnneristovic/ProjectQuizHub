import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ResultService } from '../../services/result.service';
import { AuthService } from '../../../auth/services/auth.services';
import { ResultDto } from '../../models/ResultDto';
import { Observable } from 'rxjs';
import { ResultDetailsDto } from '../../models/ResultDetailsDto';

@Component({
    selector: 'app-result-list-component',
    imports: [CommonModule],
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
    viewDetails(result: ResultDto): void {
        // Za sada samo log u konzoli, možeš kasnije staviti modal ili navigaciju

        this.resultService.getResultDetailsById(result.id).subscribe({
            next: (data) => {
                console.log(data);
            },
        });
    }
}
