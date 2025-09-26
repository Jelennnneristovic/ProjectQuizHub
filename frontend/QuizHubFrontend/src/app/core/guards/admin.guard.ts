//ulogovan+admin stiti putanju
import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../../features/auth/services/auth.services';

export const adminGuard: CanActivateFn = () => {
    const authService = inject(AuthService);
    const router = inject(Router);

    const user= authService.GetCurrentUser();

    if (user && user.role=== "Admin") {
        return true;
    }

    return router.createUrlTree(['/']);
};