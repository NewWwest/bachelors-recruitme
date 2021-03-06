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

export interface ITeacher {
    id: number;
    name: string;
    surname: string;
    email: string; 
}


export interface IExamDetails {
    exam: IExam;
    examTakers: IExamTaker[];
}

export interface IExam {
    id: number;
    seatCount: number;
    startDateTime: Date;
    durationInMinutes: number;
    examCategoryId: number;
    examCategoryName: string;
}

export interface IExamTaker {
    id: number;
    examId: number;
    examCategoryName: string;
    candidateId: string;
    userId: number;
    teacherId: number | undefined;
    startDate: Date;
    score: number | undefined;
    userDisplayName: string;
    teacherDisplayName: string;
}

export interface IExamCategory {
    id: number;
    name: string;
    examType: ExamType;
}