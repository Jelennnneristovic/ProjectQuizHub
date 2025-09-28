import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'app-confirm-modal-component',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './confirm-modal-component.html',
    styleUrl: './confirm-modal-component.scss',
})
export class ConfirmModalComponent {
    @Input() title: string = 'Confirm';
    @Input() message: string = 'Are you sure?';
    @Input() confirmText: string = 'Delete';
    @Input() cancelText: string = 'Cancel';

    @Output() confirm = new EventEmitter<void>();
    @Output() cancel = new EventEmitter<void>();

    onConfirm() {
        this.confirm.emit();
    }

    onCancel() {
        this.cancel.emit();
    }
}
