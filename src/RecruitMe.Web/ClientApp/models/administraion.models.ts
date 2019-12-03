export enum SystemEntity {
    Teacher = "teacher",
    Exam = "exam",
    Candidate = "candidate",
    ExamCategory = "examCategory"
}

export enum ExamType {
    Individual,
    Collective
}


export interface Teacher {
    id: number;
    name: number;
    surname: number;
}

export interface Exam {
    id: number;
    name: number;
    startDateTime: Date;
    type: ExamType;
    durationInMinutes: number;
    examCategory: ExamCategory;
}


export interface ExamCategory {
    id: number;
    name: number;
}