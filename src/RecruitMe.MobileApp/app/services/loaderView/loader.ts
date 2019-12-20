import * as application from 'tns-core-modules/application';
import { isIOS, isAndroid } from 'tns-core-modules/platform';

declare var android: any;

export default class LoaderService {
    private static loaderView: any;

    public static showLoader() {
        if (LoaderService.loaderView) {
            return;
        }
    
        if (isAndroid) {
            LoaderService.loaderView = android.app.ProgressDialog.show(application.android.foregroundActivity, '', 'Operacja w trakcie');
        }
    }

    public static hideLoader() {
        if (!LoaderService.loaderView) {
            return;
        }

        if (isAndroid) {
            LoaderService.loaderView.dismiss();
        }

        LoaderService.loaderView = null;
    }
}