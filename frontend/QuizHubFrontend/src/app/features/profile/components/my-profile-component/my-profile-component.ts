import { Component, inject, OnInit } from '@angular/core';
import { UserDto } from '../../models/UserDto';
import { AuthService } from '../../../auth/services/auth.services';
import { ProfileService } from '../../service/profile.service';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-my-profile-component',
    imports: [CommonModule],
    standalone: true,
    templateUrl: './my-profile-component.html',
    styleUrl: './my-profile-component.scss',
})
export class MyProfileComponent implements OnInit {
    private profilService = inject(ProfileService);
    private authService = inject(AuthService);
    user?: UserDto;
    ngOnInit(): void {
        const userContext = this.authService.GetCurrentUser();
        if (userContext) {
            this.profilService.getUserById(Number(userContext.id)).subscribe({
                next: (data) => (this.user = data),
            });
        }
    }
}
