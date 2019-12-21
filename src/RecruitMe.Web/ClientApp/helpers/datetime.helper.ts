export function toLocalTime(date: any):Date {
    let timezoneOffsetInMinutes = new Date().getTimezoneOffset();
    return new Date(new Date(date).getTime() - (timezoneOffsetInMinutes * 60 * 1000));
}