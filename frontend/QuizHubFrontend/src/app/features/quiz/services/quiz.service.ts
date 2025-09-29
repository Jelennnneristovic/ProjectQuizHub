import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
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
    // filter po težini i kategoriji
    searchQuizzes(difficultyLevel?: string, categoryName?: string): Observable<QuizDto[]> {
        let params = new HttpParams();
        if (difficultyLevel) params = params.set('difficultyLevel', difficultyLevel);
        if (categoryName) params = params.set('categoryName', categoryName);

        return this.http.get<QuizDto[]>(`${this.apiUrl}/search`, { params });
    }

    // pretraga po ključnoj reči
    searchByKeyword(keyword: string): Observable<QuizDto[]> {
        return this.http.get<QuizDto[]>(`${this.apiUrl}/search/${keyword}`);
    }
}
