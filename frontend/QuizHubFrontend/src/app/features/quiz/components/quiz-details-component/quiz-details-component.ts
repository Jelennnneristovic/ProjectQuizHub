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
import { ToastService } from '../../../../shared/services/toast.service';
import { AnswerOptionDto } from '../../models/AnswerOptionDto';

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
    private toastService = inject(ToastService);

    quizDetails?: QuizDetailsDto;

    questionToDelete?: QuestionDto;

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

    validateQuestionText(questionText: string): boolean {
        if (!questionText.trim()) {
            this.toastService.error('Question text is required.', 3000);
            return true;
        }
        return false;
    }

    validatePoints(points: number): boolean {
        if (points <= 0) {
            this.toastService.error('Points must be positive number.', 3000);
            return true;
        }
        return false;
    }

    validateFillInAnswer(fillInAnswer: string, questionType: string): boolean {
        if (questionType === 'FillIn' && !fillInAnswer.trim()) {
            this.toastService.error('Fill in question must have answer.', 3000);
            return true;
        }
        return false;
    }

    validateQuestionType(questionType: string, answerOptions: CreateAnswerOptionDto[]): boolean {
        if (questionType === 'MultipleChoice') {
            if (answerOptions.length < 2) {
                this.toastService.error('Multiple Choice must have at least 2 options.', 3000);
                return true;
            }

            const correctCount = answerOptions.filter((opt) => opt.isCorrect).length;
            if (correctCount < 2) {
                this.toastService.error('Multiple Choice must have at least 2 correct answers.', 3000);
                return true;
            }
        }

        if (questionType === 'SingleChoice') {
            if (answerOptions.length < 2) {
                this.toastService.error('Single Choice must have at least 2 options.', 3000);
                return true;
            }

            const correctCount = answerOptions.filter((opt) => opt.isCorrect).length;
            if (correctCount !== 1) {
                this.toastService.error('Single Choice must have exactly 1 correct answer.', 3000);
                return true;
            }
        }

        if (questionType === 'TrueFalse') {
            if (answerOptions.length !== 2) {
                this.toastService.error('True/False must have exactly 2 options.', 3000);
                return true;
            }

            const correctCount = answerOptions.filter((opt) => opt.isCorrect).length;
            if (correctCount !== 1) {
                this.toastService.error('True/False must have exactly 1 correct answer.', 3000);
                return true;
            }
        }
        return false;
    }

    validateOptions(questionType: string, answerOptions: CreateAnswerOptionDto[]): boolean {
        if (questionType !== 'FillIn' && answerOptions.some((opt) => !opt.text || opt.text.trim() === '')) {
            this.toastService.error('All answer options must have text.', 3000);
            return true;
        }
        return false;
    }

    addQuestion() {
        if (
            this.validateQuestionText(this.currentQuestion.text.trim()) ||
            this.validatePoints(this.currentQuestion.points) ||
            this.validateFillInAnswer(this.currentQuestion.correctFillInAnswer, this.currentQuestion.questionType) ||
            this.validateQuestionType(this.currentQuestion.questionType, this.currentQuestion.answerOptions) ||
            this.validateOptions(this.currentQuestion.questionType, this.currentQuestion.answerOptions)
        ) {
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

    validateQuestionTypeForEdit(questionType: string, answerOptions: AnswerOptionDto[]): boolean {
        if (questionType === 'MultipleChoice') {
            if (answerOptions.length < 2) {
                this.toastService.error('Multiple Choice must have at least 2 options.', 3000);
                return true;
            }

            const correctCount = answerOptions.filter((opt) => opt.isCorrect).length;
            if (correctCount < 2) {
                this.toastService.error('Multiple Choice must have at least 2 correct answers.', 3000);
                return true;
            }
        }

        if (questionType === 'SingleChoice') {
            if (answerOptions.length < 2) {
                this.toastService.error('Single Choice must have at least 2 options.', 3000);
                return true;
            }

            const correctCount = answerOptions.filter((opt) => opt.isCorrect).length;
            if (correctCount !== 1) {
                this.toastService.error('Single Choice must have exactly 1 correct answer.', 3000);
                return true;
            }
        }

        if (questionType === 'TrueFalse') {
            if (answerOptions.length !== 2) {
                this.toastService.error('True/False must have exactly 2 options.', 3000);
                return true;
            }

            const correctCount = answerOptions.filter((opt) => opt.isCorrect).length;
            if (correctCount !== 1) {
                this.toastService.error('True/False must have exactly 1 correct answer.', 3000);
                return true;
            }
        }
        return false;
    }

    validateOptionsForEdit(questionType: string, answerOptions: AnswerOptionDto[]): boolean {
        if (questionType !== 'FillIn' && answerOptions.some((opt) => !opt.text || opt.text.trim() === '')) {
            this.toastService.error('All answer options must have text.', 3000);
            return true;
        }
        return false;
    }

    updateQuestion() {
        if (
            !this.editingQuestion ||
            this.validateQuestionText(this.editingQuestion.text) ||
            this.validatePoints(this.editingQuestion.points) ||
            this.validateQuestionTypeForEdit(this.editingQuestion.questionType, this.editingQuestion.answerOptions) ||
            this.validateOptionsForEdit(this.editingQuestion.questionType, this.editingQuestion.answerOptions)
        ) {
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
