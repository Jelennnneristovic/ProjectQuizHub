export interface LeaderBoardEntryDto {
    position: number;
    username: string;
    score: number;
    timeElapsedMin: number;
    completedAt: string; // može ostati string jer se obično vraća ISO datum
}
