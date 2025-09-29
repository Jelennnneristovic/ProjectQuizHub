import { UserQuizAttemptQuestionDto } from './UserQuizAttemptQuestionDto';

export interface UserQuizAttemptDto {
    quizAttemptId: number;
    quizId: number;
    title: string;
    timeLimit: number;
    questions: UserQuizAttemptQuestionDto[];
}
