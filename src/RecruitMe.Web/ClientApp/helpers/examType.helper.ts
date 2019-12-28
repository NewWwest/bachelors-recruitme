import { ExamType } from "../models/administraion.models";

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