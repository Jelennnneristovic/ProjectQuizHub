export interface QuizAttemptDto {
  quizAttemptId: number;
  userId: number;
  username: string;
  quizId: number;
  quizTitle: string;
  startedAt: string;
  finisedAt?: string | null;
  timeLimit: number;
  timeTakenMin?: number | null;
  score: number;
}
