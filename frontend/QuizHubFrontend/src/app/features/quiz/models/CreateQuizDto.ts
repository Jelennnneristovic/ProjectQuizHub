export interface CreateQuizDto {
    title: string;
    description?: string;
    timeLimit: number;
    difficultyLevel: string;
    categoryId: number;
}
