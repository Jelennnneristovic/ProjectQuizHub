import { UserQuizAttemptOptionDto } from './UserQuizAttemptOptionDto';

export interface UserQuizAttemptQuestionDto {
    id: number;
    text: string;
    points: number;
    questionType: string;
    answered: boolean;
    optionDtos: UserQuizAttemptOptionDto[];
}
