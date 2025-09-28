import { Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../auth/services/auth.services';

@Component({
    selector: 'app-user-homepage-component',
    imports: [RouterModule],
    standalone: true,
    templateUrl: './user-homepage-component.html',
    styleUrl: './user-homepage-component.scss',
})
export class UserHomepageComponent {
    private router = inject(Router);
    private authService = inject(AuthService);

    logout() {
        this.authService.logout();
        this.router.navigate(['/auth/login']);
    }
}
