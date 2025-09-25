import { Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable, tap } from "rxjs";
import { LoginUserDto } from "../models/LoginUserDto";

//auth servis sluzi za logovanje i logout.
@Injectable({ providedIn: 'root' })
export class AuthService {

private apiUrl = environment.apiUrl + '/users';

    constructor(private http: HttpClient) {}

    login(dto: LoginUserDto): Observable<string> {
        return this.http.post(`${this.apiUrl}/login`, dto, { responseType: 'text' }).pipe(
        tap((res) => {
            localStorage.setItem('jwt', res);
        })
        );
    }

    logout() {
        localStorage.removeItem('jwt');
    }

    isLoggedIn(): boolean {
        return !!localStorage.getItem('jwt');

    }

}