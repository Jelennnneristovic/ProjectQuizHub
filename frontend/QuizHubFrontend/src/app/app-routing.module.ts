import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [
    {
        path: '',
        redirectTo: 'auth/login',
        pathMatch: 'full',
    },
    {
        path: 'auth',
        loadChildren: () => import('./features/auth/auth-routing.module').then((m) => m.AuthRoutingModule),
    },
    {
        path: 'admin',
        loadChildren: () => import('./features/admin/admin-routing.module').then((m) => m.AdminRoutingModule),
    },
    {
        path: '**',
        redirectTo: 'auth/login',
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule{}
