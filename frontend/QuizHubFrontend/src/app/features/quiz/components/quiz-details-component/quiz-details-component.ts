import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { QuizService } from '../../services/quiz.service';
import { QuestionService } from '../../services/question.service';
import { QuizDetailsDto } from '../../models/QuizDetailsDto';
import { CreateQuestionDto } from '../../models/CreateQuestionDto ';
import { QuestionDto } from '../../models/QuestionDto';
import { FormsModule } from '@angular/forms';
import { CreateAnswerOptionDto } from '../../models/CreateAnswerOptionDto ';
import { DeleteQuestionDto } from '../../models/DeleteQuestionDto ';
import { UpdateQuestionDto } from '../../models/UpdateQuestionDto ';
import { ConfirmModalComponent } from '../../../../shared/components/confirm-modal-component/confirm-modal-component';

@Component({
    selector: 'app-quiz-details-component',
    imports: [CommonModule, RouterModule, FormsModule, ConfirmModalComponent],
    standalone: true,
    templateUrl: './quiz-details-component.html',
    styleUrl: './quiz-details-component.scss',
})
export class QuizDetailsComponent implements OnInit {
    private route = inject(ActivatedRoute);

    private quizService = inject(QuizService);
    private questionService = inject(QuestionService);

    quizDetails?: QuizDetailsDto;

    questionToDelete?: QuestionDto;

    // novi question forma
    newQuestion: CreateQuestionDto = {
        text: '',
        points: 1,
        questionType: 'SingleChoice',
        quizId: 0,
        correctFillInAnswer: undefined,
        answerOptions: [],
    };

    editingQuestion?: QuestionDto;
    currentQuestion: any = this.resetNewQuestion();

    ngOnInit(): void {
        const id = Number(this.route.snapshot.paramMap.get('id'));
        this.quizService.getQuiz(id).subscribe({
            next: (data) => {
                this.quizDetails = data;
                this.newQuestion.quizId = data.id;
            },
        });
    }

    private resetNewQuestion() {
        return {
            text: '',
            points: 1,
            questionType: 'SingleChoice',
            quizId: this.quizDetails?.id,
            correctFillInAnswer: '',
            answerOptions: [] as CreateAnswerOptionDto[],
        };
    }

    // ---------------- Question CRUD ----------------

    addQuestion() {
        if (!this.currentQuestion.text.trim()) {
            alert('Question text is required!');
            return;
        }
        if (this.currentQuestion.questionType !== 'FillIn' && this.currentQuestion.answerOptions.length === 0) {
            alert('Add at least one answer option!');
            return;
        }

        const dto: CreateQuestionDto = {
            quizId: this.quizDetails!.id,
            text: this.currentQuestion.text,
            points: this.currentQuestion.points,
            questionType: this.currentQuestion.questionType,
            correctFillInAnswer:
                this.currentQuestion.questionType === 'FillIn' ? this.currentQuestion.correctFillInAnswer : undefined,
            answerOptions: this.currentQuestion.answerOptions,
        };

        this.questionService.createQuestion(dto).subscribe({
            next: (updatedQuiz) => {
                this.quizDetails = updatedQuiz;
                this.currentQuestion = this.resetNewQuestion();
            },
            error: (err) => {
                console.error(err);
                alert('Failed to add question');
            },
        });
    }

    editQuestion(q: QuestionDto) {
        this.editingQuestion = { ...q };
        this.currentQuestion = {
            text: q.text,
            points: q.points,
            questionType: q.questionType,
            correctFillInAnswer: q.correctFillInAnswer,
            answerOptions: q.answerOptions.map((a) => ({ ...a })),
        };
    }

    updateQuestion() {
        if (!this.editingQuestion) return;

        if (!this.currentQuestion.text.trim()) {
            alert('Question text is required!');
            return;
        }

        const dto: UpdateQuestionDto = {
            quizId: this.quizDetails!.id,
            questionId: this.editingQuestion.id,
            text: this.currentQuestion.text,
            points: this.currentQuestion.points,
            updateAnswerOptionDtos: this.currentQuestion.answerOptions.map((a: any) => ({
                AnswerOptionId: a.id,
                Text: a.text,
                IsCorrect: a.isCorrect,
            })),
        };

        this.questionService.updateQuestion(dto).subscribe({
            next: (updatedQuiz) => {
                this.quizDetails = updatedQuiz;
                this.editingQuestion = undefined;
                this.currentQuestion = this.resetNewQuestion();
            },
            error: (err) => {
                console.error(err);
                alert('Failed to update question');
            },
        });
    }

    openDeleteQuestionConfirm(question: QuestionDto) {
        this.questionToDelete = question;
    }

    cancelDelete() {
        this.questionToDelete = undefined;
    }

    deleteQuestion() {
        if (!this.questionToDelete) return;

        const dto: DeleteQuestionDto = {
            quizId: this.quizDetails!.id,
            questionId: this.questionToDelete.id,
        };

        this.questionService.removeQuestion(dto).subscribe({
            next: (updatedQuiz) => {
                this.quizDetails = updatedQuiz;
                this.questionToDelete = undefined;
            },
            error: (err) => {
                console.error(err);
                alert('Failed to delete question');
            },
        });
    }

    cancelEditing() {
        this.editingQuestion = undefined;
        this.currentQuestion = this.resetNewQuestion();
    }

    // ---------------- Answer Option CRUD ----------------

    addAnswerOption() {
        this.currentQuestion.answerOptions.push({ text: '', isCorrect: false } as CreateAnswerOptionDto);
    }

    removeAnswerOption(index: number) {
        this.currentQuestion.answerOptions.splice(index, 1);
    }

    onQuestionTypeChange() {
        if (this.currentQuestion.questionType === 'FillIn') {
            this.currentQuestion.answerOptions = [];
            this.currentQuestion.correctFillInAnswer = '';
        }
    }
}
