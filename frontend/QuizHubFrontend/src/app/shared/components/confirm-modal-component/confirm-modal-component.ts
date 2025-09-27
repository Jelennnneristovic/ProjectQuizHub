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
    @Input() title: string = 'Potvrda';
    @Input() message: string = 'Da li ste sigurni?';
    @Input() confirmText: string = 'Obriši';
    @Input() cancelText: string = 'Odustani';

    @Output() confirm = new EventEmitter<void>();
    @Output() cancel = new EventEmitter<void>();

    onConfirm() {
        this.confirm.emit();
    }

    onCancel() {
        this.cancel.emit();
    }
}
