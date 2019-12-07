export interface IPersonalData {
    adress: string;
    fatherName: string;
    motherName: string;
    primarySchool: string;
}

export interface IProfileData {
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
