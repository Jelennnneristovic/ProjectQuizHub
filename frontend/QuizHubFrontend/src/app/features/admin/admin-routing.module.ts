import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AdminHomepageComponent } from "./pages/admin-homepage-component/admin-homepage-component";
import { adminGuard } from "../../core/guards/admin.guard";
import { QuizListComponent } from "../quiz/components/quiz-list-component/quiz-list-component";

const routes: Routes=[

    {
        path: 'homepage', component: AdminHomepageComponent,
        canActivate:[adminGuard],
        children: [
            {path: '',redirectTo: 'quizzes', pathMatch: 'full'},
            {path: 'quizzes', component: QuizListComponent},

        ]

    },

];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}



