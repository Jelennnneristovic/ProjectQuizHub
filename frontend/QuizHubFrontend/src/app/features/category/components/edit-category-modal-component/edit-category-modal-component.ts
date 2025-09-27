import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UpdateCategoryDto } from '../../models/updateCategoryDto';
import { FormsModule } from '@angular/forms';
import { CreateCategoryDto } from '../../models/createCategoryDto';

@Component({
    selector: 'app-edit-category-modal-component',
    imports: [CommonModule, FormsModule],
    standalone: true,
    templateUrl: './edit-category-modal-component.html',
    styleUrl: './edit-category-modal-component.scss',
})
export class EditCategoryModalComponent implements OnInit {
    @Input() name: string = '';
    @Input() description: string = '';
    @Input() isEdit: boolean = false;

    @Output() edit = new EventEmitter<UpdateCategoryDto>();
    @Output() create = new EventEmitter<CreateCategoryDto>();
    @Output() cancel = new EventEmitter<void>();

    newName: string = '';
    newDescription: string = '';

    ngOnInit(): void {
        this.newName = this.name;
        this.newDescription = this.description || '';
    }

    close() {
        this.cancel.emit();
    }

    onCreate() {
        this.create.emit({
            name: this.newName.trim() ?? '',
            description: this.newDescription.trim() ?? '',
        });
    }

    onEdit() {
        this.edit.emit({
            oldName: this.name,
            newName: this.newName.trim() ?? '',
            description: this.newDescription.trim() ?? '',
        });
    }
}
