import { inject, Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { QuizAttemptDto } from "../model/QuizAttemptDto";

@Injectable({ providedIn: 'root' })

export class QuizAttemptService{

    private http = inject(HttpClient)
    private apiUrl = environment.apiUrl + '/quizAttempts';

    getQuizAttempts() : Observable<QuizAttemptDto[]>{

            return this.http.get<QuizAttemptDto[]>(this.apiUrl)
    }


}