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


export interface ITeacher {
    id: number;
    name: number;
    surname: number;
}

export interface IExam {
    id: number;
    name: number;
    startDateTime: Date;
    type: ExamType;
    durationInMinutes: number;
    examCategory: IExamCategory;
}


export interface IExamCategory {
    id: number;
    name: number;
}