import { ProgressDto } from './ProgressDto';
import { ResultDetailsQuizAttemptAnswerDto } from './ResultDetailsQuizAttemptAnswerDto';

export interface ResultDetailsDto {
    id: number;
    quizAttemptId: number;
    quizTitle: string;
    totalQuestions: number;
    correctAnswers: number;
    score: number;
    percentage: number;
    maximumScore: number;
    timeTakenMin: number;
    createdAt: string;
    unanswerdQuestion: number;
    attemptAnswerDtos: ResultDetailsQuizAttemptAnswerDto[];
    progress: ProgressDto[];
}
