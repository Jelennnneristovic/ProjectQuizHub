import { Component, EventEmitter, inject, OnInit, Output, ViewChild } from '@angular/core';
import { CategoryDto } from '../../models/categoryDto';
import { CategoryService } from '../../services/category.service';
import { CommonModule } from '@angular/common';
import { ConfirmModalComponent } from '../../../../shared/components/confirm-modal-component/confirm-modal-component';
import { EditCategoryModalComponent } from '../edit-category-modal-component/edit-category-modal-component';
import { UpdateCategoryDto } from '../../models/updateCategoryDto';
import { CreateCategoryDto } from '../../models/createCategoryDto';

@Component({
    selector: 'app-category-list-component',
    imports: [CommonModule, ConfirmModalComponent, EditCategoryModalComponent],
    standalone: true,
    templateUrl: './category-list-component.html',
    styleUrl: './category-list-component.scss',
})
export class CategoryListComponent implements OnInit {
    private categoryService = inject(CategoryService);
    categories: CategoryDto[] = [];

    categoryToEdit?: CategoryDto;
    categoryToDelete?: CategoryDto;
    addCategoryMode: boolean = false;

    ngOnInit(): void {
        this.loadCategories();
    }

    loadCategories() {
        this.categoryService.getCategories().subscribe({
            next: (data) => {
                this.categories = data;
            },
        });
    }
    // add category
    askToAdd() {
        this.addCategoryMode = true;
    }
    confirmAdd(createCategoryDto: CreateCategoryDto) {
        this.categoryService.createCategory(createCategoryDto).subscribe({
            next: () => {
                this.loadCategories();
                this.addCategoryMode = false;
            },
        });
        this.addCategoryMode = false;
    }
    cancelAdd() {
        this.addCategoryMode = false;
    }

    // edit category
    askToEdit(c: CategoryDto) {
        this.categoryToEdit = c;
    }
    cancelEdit() {
        this.categoryToEdit = undefined;
    }
    confirmEdit(updateCategoryDto: UpdateCategoryDto) {
        this.categoryService.updateCategory(updateCategoryDto).subscribe({
            next: () => {
                this.loadCategories();
                this.categoryToEdit = undefined;
            },
        });
    }
    // delete category
    askToDelete(c: CategoryDto) {
        this.categoryToDelete = c;
    }
    cancelDelete() {
        this.categoryToDelete = undefined;
    }
    //brisemo kategoriju, posle uspesnog brisanja pozivamo load
    //dobavljamo sve aktivne
    confirmDelete() {
        if (!this.categoryToDelete) {
            return; // ako nekim slucajem nije nista izabrano ne moze brisati
        }

        this.categoryService.deleteCategory(this.categoryToDelete.name).subscribe({
            next: () => {
                this.loadCategories();
                // resetuj selected category jer smo je obrisali
                this.categoryToDelete = undefined;
            },
        });
    }
}
