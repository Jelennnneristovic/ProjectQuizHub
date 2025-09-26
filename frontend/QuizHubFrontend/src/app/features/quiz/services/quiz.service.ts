import { inject, Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { QuizDto } from "../models/QuizDto";

@Injectable({ providedIn: 'root' })

export class QuizService{

    private http = inject(HttpClient)
    private apiUrl = environment.apiUrl + '/quizzes';

    getQuizzes() : Observable<QuizDto[]>{

            return this.http.get<QuizDto[]>(this.apiUrl)
    }


}