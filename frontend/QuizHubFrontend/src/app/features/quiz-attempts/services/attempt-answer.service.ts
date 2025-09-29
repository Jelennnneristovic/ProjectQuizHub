import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { CreateAttemptAnswerDto } from '../model/CreateAttemptAnswerDto';

@Injectable({ providedIn: 'root' })
export class AttemptAnswerService {
    private http = inject(HttpClient);
    private apiUrl = environment.apiUrl + '/attemptAnswers';

    createAttemptAnswer(dto: CreateAttemptAnswerDto): Observable<string> {
        return this.http.post(`${this.apiUrl}`, dto, { responseType: 'text' });
    }
}
