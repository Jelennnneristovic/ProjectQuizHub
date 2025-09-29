import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { QuizService } from '../../services/quiz.service';
import { QuestionService } from '../../services/question.service';
import { QuizDetailsDto } from '../../models/QuizDetailsDto';
import { CreateQuestionDto } from '../../models/CreateQuestionDto ';
import { QuestionDto } from '../../models/QuestionDto';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-quiz-details-component',
    imports: [CommonModule, RouterModule, FormsModule],
    standalone: true,
    templateUrl: './quiz-details-component.html',
    styleUrl: './quiz-details-component.scss',
})
export class QuizDetailsComponent implements OnInit {
    private route = inject(ActivatedRoute);

    private quizService = inject(QuizService);
    private questionService = inject(QuestionService);

    quizDetails?: QuizDetailsDto;

    // novi question forma
    newQuestion: CreateQuestionDto = {
        text: '',
        points: 1,
        questionType: 'SingleChoice',
        quizId: 0,
        correctFillInAnswer: undefined,
        answerOptions: [],
    };

    // za edit
    editingQuestion: QuestionDto | null = null;
    toastr: any;

    ngOnInit(): void {
        const id = Number(this.route.snapshot.paramMap.get('id'));
        this.quizService.getQuiz(id).subscribe({
            next: (data) => {
                this.quizDetails = data;
                this.newQuestion.quizId = data.id;
            },
        });
    }

    addQuestion() {
        // ovdje dodati jos koju zastitu

        if (!this.newQuestion.text.trim()) {
            this.toastr.error('Question text is required.', 'Validation');
            return;
        }
        if (this.newQuestion.points <= 0) {
            this.toastr.error('Points must be greater than 0.', 'Validation');
            return;
        }

        if (
            (!this.newQuestion.answerOptions || this.newQuestion.answerOptions.length === 0) &&
            this.newQuestion.questionType !== 'FillIn'
        ) {
            this.toastr.error('At least one answer option is required.', 'Validation');
            return;
        }

        if (!this.newQuestion.correctFillInAnswer && this.newQuestion.questionType === 'FillIn') {
            this.toastr.error('Fill in question must have fill in answer.', 'Validation');
            return;
        }

        if (this.newQuestion.answerOptions.length > 0 && this.newQuestion.questionType === 'FillIn') {
            this.toastr.error('Fill in question must have 0 answer options.', 'Validation');
            return;
        }

        const emptyOption = this.newQuestion.answerOptions.find((opt) => !opt.text.trim());
        if (emptyOption) {
            this.toastr.error('All answer options must have text.', 'Validation');
            return;
        }

        if (this.newQuestion.questionType === 'MultipleChoice') {
            const hasCorrect = this.newQuestion.answerOptions.some((opt) => opt.isCorrect);
            if (!hasCorrect) {
                this.toastr.error('At least one option must be marked as correct.', 'Validation');
                return;
            }
        }

        this.questionService.createQuestion(this.newQuestion).subscribe({
            next: (data) => {
                this.quizDetails = data;
                // reset forme
                this.newQuestion = {
                    text: '',
                    points: 1,
                    questionType: 'SingleChoice',
                    quizId: this.quizDetails.id,
                    correctFillInAnswer: undefined,
                    answerOptions: [],
                };
            },
        });
    }

    addAnswerOptionToNew() {
        this.newQuestion.answerOptions.push({ text: '', isCorrect: false });
    }

    removeAnswerOptionFromNew(index: number) {
        this.newQuestion.answerOptions.splice(index, 1);
    }
}
