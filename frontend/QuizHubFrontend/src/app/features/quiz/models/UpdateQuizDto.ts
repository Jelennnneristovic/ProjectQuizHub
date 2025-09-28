export interface UpdateQuizDto {
    id: number;
    newTitle?: string;
    description?: string;
    timeLimit: number;
    difficultyLevel?: string;
    idCategory: number;
}
