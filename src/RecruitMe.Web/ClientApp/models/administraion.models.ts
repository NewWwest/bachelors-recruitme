export enum SystemEntity {
    Teacher = "teacher",
    Exam = "exam",
    Candidate = "candidate",
    ExamCategory = "examCategory"
}

export enum ExamType {
    Individual = 1,
    Collective = 2
}

export function ExamTypeDisplayName(type: ExamType) {
    switch (type) {
        case ExamType.Individual:
            return "Indywidualny";
        case ExamType.Collective:
            return "Pisemny";
        default:
            return type;
    }
}


export interface ITeacher {
    id: number;
    name: number;
    surname: number;
    email: string;
}

export interface IExam {
    id: number;
    name: number;
    startDateTime: Date;
    durationInMinutes: number;
    examCategory: IExamCategory;
}


export interface IExamCategory {
    id: number;
    name: string;
    examType: ExamType;
}