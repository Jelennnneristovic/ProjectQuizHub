import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.services';
import { CreateUserDto } from '../../models/CreateUserDto';

@Component({
  selector: 'app-register-component',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  standalone: true,
  templateUrl: './register-component.html',
  styleUrl: './register-component.scss',
})
export class RegisterComponent {

  private fb = inject(FormBuilder);
  private authService = inject (AuthService);
  private router = inject(Router);

  loading = false;
  errorMsg = '';

    registerForm: FormGroup = this.fb.group({
            username:['',[Validators.required, Validators.minLength(3)]],
            email: ['',[Validators.required, Validators.email]],
            password: ['',[Validators.required, Validators.minLength(3)]],

    });

    IsInvalid(controlName : string) : boolean{
      const control = this.registerForm.get(controlName);
      return !!(control && control.invalid && (control.dirty || control.touched));
    
    }
    onSubmit(): void {
        if (this.registerForm.invalid) return;

        this.loading = true;
        this.errorMsg = '';

        const dto: CreateUserDto = this.registerForm.value as CreateUserDto;

        this.authService.register(dto).subscribe({
            next: () => {
                this.loading = false;
                this.router.navigate(['/auth/login']);
            },
            error: (err) => {
                this.loading = false;
                this.errorMsg = err.error?.message || 'Registration failed';
            },
        });
    }
}
