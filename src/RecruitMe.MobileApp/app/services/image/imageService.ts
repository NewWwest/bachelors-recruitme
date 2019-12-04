import { ApiGateway } from '../common/apiGateway';
import PopupFactory from '../popupFactory';
import { LocalStorageService } from '../localStorage/localStorageService';
import { requestPermissions, takePicture, CameraOptions } from 'nativescript-camera';
import LoaderService from '../loaderView/loader';
import { ImageSource, fromUrl } from 'tns-core-modules/image-source/image-source'
import * as fs from 'tns-core-modules/file-system'

export class ImageService {
    private _userImage : ImageSource = new ImageSource();
    private _apiGateway: ApiGateway = new ApiGateway();
    private static _cameraOptions: CameraOptions = {
        width: 500,
        height: 500,
        keepAspectRatio: false,
        saveToGallery: false,
        cameraFacing: "front"
    }

    public loadUserPicture() : Promise<ImageSource> {
        let fileId: number | undefined = LocalStorageService.getProfileData().profilePictureFileId;
        
        if (fileId) {
            const url: any = this._apiGateway.getImageRequest(fileId);
        
            return fromUrl(url).then(r => {
                this._userImage = r;
                return r;
            }, err => {
                console.log(err);
                PopupFactory.GenericErrorPopup("" + err);

                return new ImageSource();
            })
        }

        return new Promise (() => new ImageSource());
    }

    public takePicture() : Promise<void> {
        return requestPermissions().then(permissionGranted => {
            takePicture(ImageService._cameraOptions).then(imageAsset => {
                
                LoaderService.showLoader();
                let is: ImageSource = new ImageSource();
                is.fromAsset(imageAsset).then(imageSource => {
                    //const fileContent = imageSource.toBase64String("jpg");
                    // save file
                    const folder = fs.knownFolders.documents().path;
                    const fileName: string = '' + LocalStorageService.getUserId() + '_'
                        + new Date().getTime() + '_mobile.png';
                    const path = fs.path.join(folder, fileName);

                    console.log(path);
                    const saved = imageSource.saveToFile(path, "png");
                    
                    if (saved) {

                        console.log('before call');
                        let task = this._apiGateway.setProfilePicture(path, fileName);
                        console.log('after call');

                        task.on('error', (e) => {
                            console.log(e);
                            // LoaderService.hideLoader();
                            // PopupFactory.GenericErrorPopup('' + e);
                        });
                        task.on('complete', (e) => {
                            console.log(e);
                            // LoaderService.hideLoader();
                            // PopupFactory.GenericSuccessPopup('Pomyślnie zapisano obrazek');
                        });
                    }
                    else {
                        LoaderService.hideLoader();
                        PopupFactory.GenericErrorPopup('Nie udał się zapis do pamięci telefonu');
                    }

                    // this._apiGateway.setProfilePicture(fileContent, fileName).then(r => {
                    //     console.log("siadło");
                    //     LoaderService.hideLoader();
                    //     PopupFactory.GenericSuccessPopup('Pomyślnie zapisano obrazek');
                    // }, e => {
                    //     console.log(e);
                    //     LoaderService.hideLoader();
                    //     PopupFactory.GenericErrorPopup('' + e);
                    // });
                })
                // imageAsset.getImageAsync((image, error) => {
                //     if (!error) {
                //         // only for android
                //         let bitmapImage: android.graphics.Bitmap = image;
                //         let stream = new java.io.ByteArrayOutputStream();
                //         bitmapImage.compress(android.graphics.Bitmap.CompressFormat.PNG, 100, stream);
                //         let byteArray = stream.toByteArray();
                //         bitmapImage.recycle();

                //         console.log(bitmapImage);
                //         console.log(byteArray);

                //         const str = new java.lang.String(byteArray, java.nio.charset.StandardCharsets.UTF_8);
                //         const fileContent = '' + str;

                //         console.log(fileContent);

                //         const fileName: string = '' + LocalStorageService.getUserId() + '_'
                //          + new Date().getTime() + '_mobile.png';

                //         this._apiGateway.setProfilePicture(fileContent, fileName).then(r => {
                //             console.log("mhm");
                //             LoaderService.hideLoader();
                //         }, e => {
                //             console.log(e);
                //             LoaderService.hideLoader();
                //         })
                //     }
                //     else {
                //         console.log(error);
                //     }
                // })

                var folder = fs.knownFolders.documents();
                var path = fs.path.join(folder.path, "Test.png");
                var saved = imageAsset.saveToFile(path, "png");
            })
        })
    }

    private savePicture(image: ImageSource) {

    }
}