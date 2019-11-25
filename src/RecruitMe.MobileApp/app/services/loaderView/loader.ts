import * as application from 'tns-core-modules/application';
import { isIOS, isAndroid } from 'tns-core-modules/platform';

declare var android;



export default class LoaderService {
    private static loaderView;

    public static showLoader() {
        if (LoaderService.loaderView) {
            return;
        }
    
        // if Android
        LoaderService.loaderView = android.app.ProgressDialog.show(application.android.foregroundActivity, '', 'Operacja w trakcie');
    }

    public static hideLoader() {
        if (!LoaderService.loaderView) {
            return;
        }

        // if Android
        LoaderService.loaderView.dismiss();
        LoaderService.loaderView = null;
    }
}