import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.services';
import { CreateUserDto } from '../../models/CreateUserDto';
import { ToastService } from '../../../../shared/services/toast.service';

@Component({
    selector: 'app-register-component',
    imports: [CommonModule, ReactiveFormsModule, RouterLink],
    standalone: true,
    templateUrl: './register-component.html',
    styleUrl: './register-component.scss',
})
export class RegisterComponent {
    private fb = inject(FormBuilder);
    private authService = inject(AuthService);
    private router = inject(Router);
    private toastService = inject(ToastService);

    loading = false;
    errorMsg = '';
    selectedFile?: File;

    registerForm: FormGroup = this.fb.group({
        username: ['', [Validators.required, Validators.minLength(3)]],
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(3)]],
    });

    IsInvalid(controlName: string): boolean {
        const control = this.registerForm.get(controlName);
        return !!(control && control.invalid && (control.dirty || control.touched));
    }

    onFileSelected(event: Event) {
        const input = event.target as HTMLInputElement;
        if (input.files && input.files.length > 0) {
            this.selectedFile = input.files[0];
        }
    }

    onSubmit(): void {
        if (this.registerForm.invalid) return;

        this.loading = true;
        this.errorMsg = '';

        const dto: CreateUserDto = {
            username: this.registerForm.value.username,
            email: this.registerForm.value.email,
            password: this.registerForm.value.password,
            profileImage: this.selectedFile,
        };

        this.authService.register(dto).subscribe({
            next: () => {
                this.loading = false;
                this.router.navigate(['/auth/login']);
            },
            error: (err) => {
                this.toastService.error(err.error, 3000);
                this.loading = false;
                this.errorMsg = err.error?.message || 'Registration failed';
            },
        });
    }
}
