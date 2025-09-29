import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { QuizAttemptDto } from '../model/QuizAttemptDto';
import { CreateQuizAttemptDto } from '../model/CreateQuizAttemptDto';
import { ResultDetailsDto } from '../../result/models/ResultDetailsDto';
import { UserQuizAttemptDto } from '../model/UserQuizAttemptDto';

@Injectable({ providedIn: 'root' })
export class QuizAttemptService {
    private http = inject(HttpClient);
    private apiUrl = environment.apiUrl + '/quizAttempts';

    getQuizAttempts(): Observable<QuizAttemptDto[]> {
        return this.http.get<QuizAttemptDto[]>(this.apiUrl);
    }

    getQuizAttemptsFromUser(): Observable<QuizAttemptDto[]> {
        return this.http.get<QuizAttemptDto[]>(this.apiUrl + '/user');
    }

    createQuizAttempt(dto: CreateQuizAttemptDto): Observable<UserQuizAttemptDto> {
        return this.http.post<UserQuizAttemptDto>(this.apiUrl, dto);
    }

    finishQuizAttempt(id: number): Observable<ResultDetailsDto> {
        return this.http.put<ResultDetailsDto>(`${this.apiUrl}/${id}`, null);
    }
}
