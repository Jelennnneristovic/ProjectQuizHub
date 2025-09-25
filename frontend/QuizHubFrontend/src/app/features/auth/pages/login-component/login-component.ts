import { Component } from '@angular/core';
import { Validators, FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.services';
import { LoginUserDto } from '../../models/LoginUserDto';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login-component',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login-component.html',
  styleUrl: './login-component.scss'
})
export class LoginComponent {

  errorMessage = '';
  loading = false;

  loginForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      userKey: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(3)]],
    });
  }

  onSubmit() {
    if (this.loginForm.invalid) return;

    this.loading = true;
    const dto: LoginUserDto = this.loginForm.value as LoginUserDto;
    
    this.authService.login(dto).subscribe({
      next: () => {
        this.loading = false;
        this.router.navigate(['']); 
      },
      error: (err) => {
        console.log(err);
        this.loading = false;
        this.errorMessage = 'Neispravno korisničko ime ili lozinka';
      },
    });
  }
}
