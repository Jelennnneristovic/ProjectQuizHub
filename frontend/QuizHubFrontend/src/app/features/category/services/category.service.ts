import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { CategoryDto } from '../models/categoryDto';
import { Observable } from 'rxjs';
import { UpdateCategoryDto } from '../models/updateCategoryDto';
import { CreateCategoryDto } from '../models/createCategoryDto';

@Injectable({ providedIn: 'root' })
export class CategoryService {
    private http = inject(HttpClient);
    private apiUrl = environment.apiUrl + '/categories';

    getCategories(): Observable<CategoryDto[]> {
        return this.http.get<CategoryDto[]>(this.apiUrl);
    }

    deleteCategory(name: string): Observable<CategoryDto> {
        return this.http.delete<CategoryDto>(this.apiUrl + '/' + name);
    }

    updateCategory(updateCategoryDto: UpdateCategoryDto): Observable<CategoryDto> {
        return this.http.put<CategoryDto>(this.apiUrl, updateCategoryDto);
    }
    createCategory(createCategoryDto: CreateCategoryDto): Observable<CategoryDto> {
        return this.http.post<CategoryDto>(this.apiUrl, createCategoryDto);
    }
    getCategory(name: string): Observable<CategoryDto> {
        return this.http.get<CategoryDto>(this.apiUrl + '/' + name);
    }
}
