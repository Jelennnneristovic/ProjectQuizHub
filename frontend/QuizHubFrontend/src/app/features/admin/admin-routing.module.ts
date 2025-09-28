import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminHomepageComponent } from './pages/admin-homepage-component/admin-homepage-component';
import { adminGuard } from '../../core/guards/admin.guard';
import { QuizListComponent } from '../quiz/components/quiz-list-component/quiz-list-component';
import { CategoryListComponent } from '../category/components/category-list-component/category-list-component';
import { QuizAttemptsListComponent } from '../quiz-attempts/components/quiz-attempts-list-component/quiz-attempts-list-component';
import { MyProfileComponent } from '../profile/components/my-profile-component/my-profile-component';
import { ResultListComponent } from '../result/components/result-list-component/result-list-component';

const routes: Routes = [
    {
        path: 'homepage',
        component: AdminHomepageComponent,
        canActivate: [adminGuard],
        children: [
            { path: '', redirectTo: 'quizzes', pathMatch: 'full' },
            { path: 'quizzes', component: QuizListComponent },
            { path: 'categories', component: CategoryListComponent },
            { path: 'attempts', component: QuizAttemptsListComponent },
            { path: 'profile', component: MyProfileComponent },
            { path: 'results', component: ResultListComponent },
        ],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AdminRoutingModule {}
