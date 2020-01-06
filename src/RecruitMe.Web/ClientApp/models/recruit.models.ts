import { ExamType } from "./administraion.models";

export interface IPersonalData {
    adress: string;
    fatherName: string;
    motherName: string;
    primarySchool: string;
}

export interface IProfileData {
    id: number; //same as userId
    userId: number;

    email: string;
    name: string;
    surname: string;
    pesel: string | undefined;
    candidateId: string;
    birthDate: Date;

    adress: string;
    fatherName: string;
    motherName: string;
    primarySchool: string;
    profilePictureName: string;
    profilePictureFileId: number | undefined;

    documents: IDocument[];
}

export interface IDocument {
    id: number;
    fileUrl: string;
    name: string;
    contentType: string;
}

export interface IMyExamDto {
    durationInMinutes: number;
    startTime: Date;
    categoryName: string;
    examType: ExamType;
}

export interface IMyExamsDto {
    exams: IMyExamDto[];
}