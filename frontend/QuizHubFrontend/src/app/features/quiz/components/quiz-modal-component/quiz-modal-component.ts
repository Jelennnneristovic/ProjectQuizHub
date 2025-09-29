import { Component, EventEmitter, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { QuizDto } from '../../models/QuizDto';
import { CreateQuizDto } from '../../models/CreateQuizDto';
import { UpdateQuizDto } from '../../models/UpdateQuizDto';
import { CategoryService } from '../../../category/services/category.service';
import { MatSelectModule } from '@angular/material/select';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
    selector: 'app-quiz-modal-component',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, MatSelectModule, MatDialogModule, MatFormFieldModule, MatInputModule],
    templateUrl: './quiz-modal-component.html',
    styleUrl: './quiz-modal-component.scss',
})
export class QuizModalComponent implements OnInit {
    private fb = inject(FormBuilder);
    public dialogRef = inject(MatDialogRef<QuizModalComponent>);
    public data = inject(MAT_DIALOG_DATA) as { quiz?: QuizDto; isEdit: boolean };
    private categoryService = inject(CategoryService);

    form = this.fb.group({
        title: ['', Validators.required],
        quizDescription: [''],
        timeLimit: [10, Validators.required],
        difficultyLevel: ['Easy', Validators.required],
        categoryId: [0, Validators.required],
    });

    save = new EventEmitter<CreateQuizDto | UpdateQuizDto>();

    ngOnInit() {
        if (this.data.isEdit && this.data.quiz) {
            const quiz = this.data.quiz;
            this.categoryService.getCategory(quiz.categoryName).subscribe({
                next: (cat) => {
                    this.form.patchValue({
                        title: quiz.title,
                        quizDescription: quiz.quizDescription,
                        timeLimit: quiz.timeLimit,
                        difficultyLevel: quiz.difficultyLevel,
                        categoryId: cat.id, // npr. kad dobiješ pravi id
                    });
                },
                error: () => {
                    this.form.patchValue({
                        title: quiz.title,
                        quizDescription: quiz.quizDescription,
                        timeLimit: quiz.timeLimit,
                        difficultyLevel: quiz.difficultyLevel,
                        categoryId: -1,
                    });
                },
            });
        }
    }

    onSave() {
        if (!this.form.valid) return;

        if (this.data.isEdit && this.data.quiz) {
            const dto: UpdateQuizDto = {
                id: this.data.quiz.id,
                newTitle: this.form.value.title!,
                description: this.form.value.quizDescription!,
                timeLimit: this.form.value.timeLimit!,
                difficultyLevel: this.form.value.difficultyLevel!,
                idCategory: this.form.value.categoryId!,
            };
            this.save.emit(dto);
        } else {
            const dto: CreateQuizDto = {
                title: this.form.value.title!,
                description: this.form.value.quizDescription!,
                timeLimit: this.form.value.timeLimit!,
                difficultyLevel: this.form.value.difficultyLevel!,
                categoryId: this.form.value.categoryId!,
            };
            this.save.emit(dto);
        }

        this.dialogRef.close();
    }

    onCancel() {
        this.dialogRef.close();
    }
}
