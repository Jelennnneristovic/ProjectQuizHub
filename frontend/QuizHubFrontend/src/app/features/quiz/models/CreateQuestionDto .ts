import { CreateAnswerOptionDto } from './CreateAnswerOptionDto ';

export interface CreateQuestionDto {
    text: string;
    points: number;
    quizId: number;
    questionType: string; // ili enum ako želiš
    correctFillInAnswer?: string;
    answerOptions: CreateAnswerOptionDto[];
}
