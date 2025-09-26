export interface QuizDto
{
    id: number,
    title: string,
    quizDescription: string|null,
    categoryName: string,
    categoryDescription: string|null,
    timeLimit: number,
    difficultyLevel: string,
    questionsCount: number


}