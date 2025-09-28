import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { ResultDto } from '../../result/models/ResultDto';
import { LeaderBoardEntriesDto } from '../models/LeaderBoardEntriesDto';

@Injectable({ providedIn: 'root' })
export class LeaderBoardService {
    private http = inject(HttpClient);
    private apiUrl = environment.apiUrl + '/results';

    getLeaderBoard(quizId: number, period?: string): Observable<LeaderBoardEntriesDto> {
        return this.http.get<LeaderBoardEntriesDto>(this.apiUrl + '/leaderboard', {
            params: {
                quizId: quizId.toString(),
                ...(period ? { period } : {}),
            },
        });
    }
}
