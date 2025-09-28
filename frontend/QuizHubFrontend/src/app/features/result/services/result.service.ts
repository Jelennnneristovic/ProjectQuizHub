import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { ResultDto } from '../models/ResultDto';
import { Observable } from 'rxjs';
import { ResultDetailsDto } from '../models/ResultDetailsDto';

@Injectable({ providedIn: 'root' })
export class ResultService {
    private http = inject(HttpClient);
    private apiUrl = environment.apiUrl + '/results';

    getResults(): Observable<ResultDto[]> {
        return this.http.get<ResultDto[]>(this.apiUrl + '/admin');
    }
    getResultsByUser(): Observable<ResultDto[]> {
        return this.http.get<ResultDto[]>(this.apiUrl);
    }
    getResultDetailsById(id: number): Observable<ResultDetailsDto> {
        return this.http.get<ResultDetailsDto>(this.apiUrl + '/details/' + id);
    }
}
