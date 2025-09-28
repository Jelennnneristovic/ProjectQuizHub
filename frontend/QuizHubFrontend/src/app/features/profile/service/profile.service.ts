import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { UserDto } from '../models/UserDto';

@Injectable({ providedIn: 'root' })
export class ProfileService {
    private http = inject(HttpClient);
    private apiUrl = environment.apiUrl + '/users';

    getUserById(id: number): Observable<UserDto> {
        return this.http.get<UserDto>(this.apiUrl + '/' + id);
    }
}
