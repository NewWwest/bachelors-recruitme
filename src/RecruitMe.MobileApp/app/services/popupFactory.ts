import { Feedback, FeedbackType } from "nativescript-feedback"

export default class PopupFactory {
    
    public static ConnectionError() {
        return this.privCreatePopup(FeedbackType.Error, "Brak połączenia z internetem!",
            "Proszę połączyć się z siecią WiFi lub włączyć transfer danych")
    }
    
    public static createPopup(type: string, title: string, message: string) {
        switch(type) {
            case "error":
                return this.privCreatePopup(FeedbackType.Error, title, message)
            case "info":
                return this.privCreatePopup(FeedbackType.Info, title, message)
            case "success": 
                return this.privCreatePopup(FeedbackType.Success, title, message)
            case "warning": 
                return this.privCreatePopup(FeedbackType.Warning, title, message)
            default:
                throw new Error('not supported type')
        }
    }

    private static privCreatePopup(type: FeedbackType, title: string, message: string) {
        return new Feedback().show({
            type: type,
            title: title,
            message: message,
            duration: 3000,
            android: {
                iconPulseEnabled: false
            }
        })
    }
}