import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { CreateQuestionDto } from '../models/CreateQuestionDto ';
import { DeleteQuestionDto } from '../models/DeleteQuestionDto ';
import { QuizDetailsDto } from '../models/QuizDetailsDto';
import { UpdateQuestionDto } from '../models/UpdateQuestionDto ';

@Injectable({ providedIn: 'root' })
export class QuestionService {
    private http = inject(HttpClient);
    private apiUrl = environment.apiUrl + '/questions';

    createQuestion(dto: CreateQuestionDto): Observable<QuizDetailsDto> {
        return this.http.post<QuizDetailsDto>(this.apiUrl, dto);
    }

    removeQuestion(dto: DeleteQuestionDto): Observable<QuizDetailsDto> {
        return this.http.request<QuizDetailsDto>('delete', this.apiUrl, { body: dto });
    }

    updateQuestion(dto: UpdateQuestionDto): Observable<QuizDetailsDto> {
        return this.http.put<QuizDetailsDto>(this.apiUrl, dto);
    }
}
