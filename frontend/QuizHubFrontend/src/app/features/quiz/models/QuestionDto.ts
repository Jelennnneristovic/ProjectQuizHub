import { AnswerOptionDto } from './AnswerOptionDto';

export interface QuestionDto {
    id: number;
    text: string;
    points: number;
    questionType: string;
    correctFillInAnswer?: string; // ? jer može biti null
    answerOptions: AnswerOptionDto[];
}
