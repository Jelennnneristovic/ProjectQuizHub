import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from '../../core/guards/auth.guards';
import { QuizTableComponent } from '../leaderBoard/components/quiz-table-component/quiz-table-component';
import { MyProfileComponent } from '../profile/components/my-profile-component/my-profile-component';
import { QuizAttemptsListComponent } from '../quiz-attempts/components/quiz-attempts-list-component/quiz-attempts-list-component';
import { QuizListComponent } from '../quiz/components/quiz-list-component/quiz-list-component';
import { ResultDetailsComponents } from '../result/components/result-details-components/result-details-components';
import { ResultListComponent } from '../result/components/result-list-component/result-list-component';
import { UserHomepageComponent } from './pages/user-homepage-component/user-homepage-component';

const routes: Routes = [
    {
        path: 'homepage',
        component: UserHomepageComponent,
        canActivate: [authGuard],
        children: [
            { path: '', redirectTo: 'quizzes', pathMatch: 'full' },
            { path: 'quizzes', component: QuizListComponent },
            { path: 'attempts', component: QuizAttemptsListComponent },
            { path: 'profile', component: MyProfileComponent },
            { path: 'leaderboard', component: QuizTableComponent },
            { path: 'results', component: ResultListComponent },
            { path: 'results/:id', component: ResultDetailsComponents },
        ],
    },
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UserRoutingModule {}
