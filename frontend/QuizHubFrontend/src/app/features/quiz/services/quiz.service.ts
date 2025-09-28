import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { QuizDto } from '../models/QuizDto';
import { CreateQuizDto } from '../models/CreateQuizDto';
import { UpdateQuizDto } from '../models/UpdateQuizDto';

@Injectable({ providedIn: 'root' })
export class QuizService {
    private http = inject(HttpClient);
    private apiUrl = environment.apiUrl + '/quizzes';

    //constructor(private http: HttpClient) {}

    getQuizzes(): Observable<QuizDto[]> {
        return this.http.get<QuizDto[]>(this.apiUrl);
    }
    createQuiz(dto: CreateQuizDto): Observable<QuizDto> {
        return this.http.post<QuizDto>(`${this.apiUrl}`, dto);
    }

    updateQuiz(dto: UpdateQuizDto): Observable<QuizDto> {
        return this.http.put<QuizDto>(`${this.apiUrl}`, dto);
    }
    deleteQuiz(id: number): Observable<string> {
        return this.http.delete(`${this.apiUrl}/${id}`, { responseType: 'text' });
    }
}
