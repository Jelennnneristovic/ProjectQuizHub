import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { AuthService } from '../../../auth/services/auth.services';
import { ResultService } from '../../services/result.service';
import { ResultDetailsDto } from '../../models/ResultDetailsDto';
import { BaseChartDirective } from 'ng2-charts';
import { ProgressDto } from '../../models/ProgressDto';

@Component({
    selector: 'app-result-details-components',
    imports: [CommonModule, RouterModule, BaseChartDirective],
    standalone: true,
    templateUrl: './result-details-components.html',
    styleUrl: './result-details-components.scss',
})
export class ResultDetailsComponents implements OnInit {
    private route = inject(ActivatedRoute);
    private resultService = inject(ResultService);

    @ViewChild(BaseChartDirective) chart?: BaseChartDirective;

    private labels: string[] = [];
    private data: number[] = [];

    public lineChartData = {
        labels: ['1', '2', '3'],
        datasets: [{ data: [10, 20, 15], label: 'Score' }],
    };

    public lineChartOptions = { responsive: true };

    result?: ResultDetailsDto;

    ngOnInit(): void {
        const id = Number(this.route.snapshot.paramMap.get('id'));
        this.resultService.getResultDetailsById(id).subscribe({
            next: (data) => {
                this.result = data;

                this.populateChartData(this.result);
                this.lineChartData = {
                    labels: this.labels,
                    datasets: [{ data: this.data, label: 'Score' }],
                };
            },
        });
    }

    populateChartData(result: ResultDetailsDto) {
        // Resetuj prethodne podatke
        this.labels = [];
        this.data = [];

        // Prođi kroz sve progress
        result.progress.forEach((p: ProgressDto) => {
            this.labels.push(p.attemptNumber.toString()); // X osa
            this.data.push(p.score); // Y osa
        });
    }
}
