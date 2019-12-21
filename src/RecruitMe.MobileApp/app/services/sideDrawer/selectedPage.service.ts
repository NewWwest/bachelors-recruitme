import { BehaviorSubject } from "rxjs"

export default class SelectedPageService {
    static _instance = new SelectedPageService();

    _selectedPageSource = new BehaviorSubject("Home"); // observable selectedPage source
    selectedPage$ = this._selectedPageSource.asObservable();

    constructor() {
        if (SelectedPageService._instance) {
            throw new Error("Use SelectedPageService.getInstance() instead of new.");
        }
    }

    public updateSelectedPage(selectedPage: string) {
        this._selectedPageSource.next(selectedPage);
    }

    public static getInstance() {
        return SelectedPageService._instance;
    }
}