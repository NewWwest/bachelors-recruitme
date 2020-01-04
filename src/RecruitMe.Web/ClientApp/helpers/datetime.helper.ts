export function toLocalTime(date: any):Date {
    let timezoneOffsetInMinutes = new Date().getTimezoneOffset();
    return new Date(new Date(date).getTime() - (timezoneOffsetInMinutes * 60 * 1000));
}
export function isToday(date: Date): boolean {
    const today: Date = new Date();
    
    return date.getDay() == today.getDay() &&
            date.getMonth() == today.getMonth() &&
            date.getFullYear() == today.getFullYear();
}
export function toLocaleDateTimeString(date: Date): string {
    const today: Date = new Date();

    if (date.getFullYear() == today.getFullYear()) {
        return getString(date);
    }
    else return getString(date, true);
}

function getString(date: Date, writeOutYear?: boolean): string {
    let month: string = "";

    switch (date.getMonth()) {
        case 0:
            month = 'stycznia';
            break;
        case 1:
            month = 'lutego';
            break;
        case 2:
            month = 'marca';
            break;
        case 3:
            month = 'kwietnia';
            break;
        case 4:
            month = 'maja';
            break;
        case 5:
            month = 'czerwca';
            break;
        case 6:
            month = 'lipca';
            break;
        case 7:
            month = 'sierpnia';
            break;
        case 8:
            month = 'września';
            break;
        case 9:
            month = 'października';
            break;
        case 10:
            month = 'listopada';
            break;
        case 11:
            month = 'grudnia';
            break;       
        default:
            throw new Error("Invalid month");
    }

    if (writeOutYear) {
        return `${date.getDate()} ${month} ${date.getFullYear()}, ${date.toLocaleTimeString()}`;
    }
    else return `${date.getDate()} ${month}, ${date.toLocaleTimeString()}`;
}