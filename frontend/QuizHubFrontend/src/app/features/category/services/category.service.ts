import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { environment } from "../../../../environments/environment";
import { CategoryDto } from "../models/categoryDto";
import { Observable } from "rxjs";

@Injectable({ providedIn: 'root' })

export class CategoryService{

    private http = inject(HttpClient)
    private apiUrl = environment.apiUrl + '/categories';

    getCategories() : Observable<CategoryDto[]>{

        return this.http.get<CategoryDto[]>(this.apiUrl)
    }


}