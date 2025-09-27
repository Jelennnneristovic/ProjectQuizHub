import { Component, inject, OnInit } from '@angular/core';
import { CategoryDto } from '../../models/categoryDto';
import { CategoryService } from '../../services/category.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-category-list-component',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './category-list-component.html',
  styleUrl: './category-list-component.scss'
})
export class CategoryListComponent implements OnInit{

    private categoryService = inject(CategoryService);
   categories: CategoryDto[]=[];

    ngOnInit(): void {
    


    //kad god se kreira komponenta, zovemo back, da napunimo listu
    this.categoryService.getCategories().subscribe({
      next: (data) => { this.categories = data;},

    });
  }

}
