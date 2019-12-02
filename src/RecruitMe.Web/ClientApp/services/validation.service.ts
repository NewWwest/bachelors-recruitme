export class ValidationService {
    public static notEmptyRule() {
        return [(v: string) => v != "" || "Musisz podać to pole"];
    }
    public static peselRules() {
        return [
            (v: string) => v.length == 11 || "Pesel musi mieć 11 cyfr",
            (v: string) => /^\d+$/.test(v) || "Pesel może zawierać tylko cyfry",
        ];
    }
}