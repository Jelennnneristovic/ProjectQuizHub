import { inject, Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable, tap } from "rxjs";
import { LoginUserDto } from "../models/LoginUserDto";
import { TokenResponseDto } from "../models/TokenResponseDto";
import { CreateUserDto } from "../models/CreateUserDto";
import { UserDto } from "../models/UserDto";
import { UserContext } from "../../../core/models/UserContext";
import { jwtDecode } from "jwt-decode";

export interface RawJwtPayload{
    [key: string]: any;

}

//auth servis sluzi za logovanje i logout.
@Injectable({ providedIn: 'root' })

export class AuthService {

    private http = inject(HttpClient)
    private apiUrl = environment.apiUrl + '/users';

    login(dto: LoginUserDto): Observable<TokenResponseDto> {
        return this.http.post<TokenResponseDto>(this.apiUrl + "/login", dto).pipe(
        tap((res) => {
            console.log(res)
            localStorage.setItem('jwt', res.token);
        })
        );
    }

    logout() {
        localStorage.removeItem('jwt');
    }

    isLoggedIn(): boolean {
        return !!localStorage.getItem('jwt');

    }

  
    register(dto: CreateUserDto): Observable<any> {
            const formData = new FormData();
            formData.append('username', dto.username);
            formData.append('email', dto.email);
            formData.append('password', dto.password);

            if (dto.profileImage) {
                formData.append('profileImage', dto.profileImage);
            }

            console.log(formData);
            console.log(dto);
            return this.http.post<any>(`${this.apiUrl}/register`, formData).pipe(tap((res) => {}));
        }

    GetCurrentUser(): UserContext | null {
            const token = localStorage.getItem('jwt');

            if(!token) return null;

            try {

                const payload= jwtDecode<RawJwtPayload>(token);

                return {
                    id: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
                    username: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
                    email: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
                    role: payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
                };

            } catch{

                return null;
            }

    }
    
}


