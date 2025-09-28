import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { AuthService } from '../../../auth/services/auth.services';
import { ResultDto } from '../../models/ResultDto';
import { ResultService } from '../../services/result.service';
import { ResultDetailsDto } from '../../models/ResultDetailsDto';

@Component({
    selector: 'app-result-details-components',
    imports: [CommonModule, RouterModule],
    standalone: true,
    templateUrl: './result-details-components.html',
    styleUrl: './result-details-components.scss',
})
export class ResultDetailsComponents {
    private authService = inject(AuthService);
    private resultService = inject(ResultService);
    private route = inject(ActivatedRoute);
    result?: ResultDetailsDto;

    ngOnInit(): void {
        const id = Number(this.route.snapshot.paramMap.get('id'));
        this.resultService.getResultDetailsById(id).subscribe({
            next: (data) => {
                this.result = data;
                console.log(data);
            },
        });
    }
}
