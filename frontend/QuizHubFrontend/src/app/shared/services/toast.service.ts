import { Injectable, inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({ providedIn: 'root' })
export class ToastService {
    private snackBar = inject(MatSnackBar);

    success(msg: string, duration = 3000) {
        this.snackBar.open(msg, 'Close', { duration, panelClass: ['toast-success'] });
    }

    error(msg: string, duration = 3000) {
        this.snackBar.open(msg, 'Close', { duration, panelClass: ['toast-error'] });
    }

    info(msg: string, duration = 3000) {
        this.snackBar.open(msg, 'Close', { duration, panelClass: ['toast-info'] });
    }

    warning(msg: string, duration = 3000) {
        this.snackBar.open(msg, 'Close', { duration, panelClass: ['toast-warning'] });
    }
}
