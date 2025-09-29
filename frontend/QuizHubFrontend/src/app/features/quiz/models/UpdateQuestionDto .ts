import { UpdateAnswerOptionDto } from './UpdateAnswerOptionDto ';

export interface UpdateQuestionDto {
    quizId: number;
    questionId: number;
    text: string;
    points: number;
    updateAnswerOptionDtos: UpdateAnswerOptionDto[];
}
