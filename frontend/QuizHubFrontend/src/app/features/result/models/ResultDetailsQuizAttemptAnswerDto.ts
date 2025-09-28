import { ResultDetailsAttemptAnswerOptionDto } from './ResultDetailsAttemptAnswerOptionDto';

export interface ResultDetailsQuizAttemptAnswerDto {
    questionText: string;
    isCorrect: boolean;
    correctFillInAnswer?: string;
    allAnswers: ResultDetailsAttemptAnswerOptionDto[];
    fillInAnswer?: string;
    userAnswers: ResultDetailsAttemptAnswerOptionDto[];
    qusetionType: string;
    questionPoints: number;
    awardedPoints: number;
}
