export interface ResultDto {
    id: number;
    quizAttemptId: number;
    quizTitle: string;
    totalQuestions: number;
    correctAnswers: number;
    score: number;
    percentage: number;
    timeTakenMin: number;
    createdAt: string; // može se kastovati u Date ako želiš
}
