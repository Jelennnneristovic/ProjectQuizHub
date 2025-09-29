export interface CreateAttemptAnswerDto {
    quizId: number;
    quizAttemptId: number;
    questionId: number;
    fillInAnswer?: string;
    attemptAnswerOptions: number[];
}
