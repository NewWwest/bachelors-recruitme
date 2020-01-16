export interface IPersonalData {
    adress: string;
    fatherName: string;
    motherName: string;
    primarySchool: string;
}
export interface IProfileData {
    adress: string;
    fatherName: string;
    motherName: string;
    primarySchool: string;
    profilePictureName: string;
    profilePictureFileId: number | undefined;
    status?: number;
}