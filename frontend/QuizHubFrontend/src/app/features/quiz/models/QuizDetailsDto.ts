import { QuestionDto } from './QuestionDto';

export interface QuizDetailsDto {
    id: number;
    title: string;
    description?: string; // ? jer može biti null
    timeLimit: number;
    difficultyLevel: string;
    categoryName: string;
    categoryDescription?: string; // ? jer može biti null
    questions: QuestionDto[];
}
