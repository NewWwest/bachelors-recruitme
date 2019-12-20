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
        width: 200,
        height: 200,
        keepAspectRatio: true,
        saveToGallery: false,
        cameraFacing: 'front'
    }

    public loadUserPicture() : Promise<ImageSource> {
        let fileId: number | undefined = LocalStorageService.getProfileData().profilePictureFileId;
        
        if (fileId) {
            return this._apiGateway.getProfilePicture(fileId).then(r => {
                if (r.data) {
                    this._userImage.loadFromBase64(r.data.file);
                }

                return this._userImage;
            }, err => {
                console.log(err);
                return new ImageSource();
            })
        }

        return new Promise (() => new ImageSource());
    }

    public takePicture() : Promise<any> {
        return requestPermissions().then(permissionGranted => {
            return takePicture(ImageService._cameraOptions).then(imageAsset => {
                LoaderService.showLoader();

                let is: ImageSource = new ImageSource();
                return is.fromAsset(imageAsset).then(imageSource => {
                    const folder = fs.knownFolders.documents().path;
                    const fileName: string = '' + LocalStorageService.getUserId() + '_'
                        + new Date().getTime() + '_mobile.png';
                    const path = fs.path.join(folder, fileName);

                    // save file
                    const saved = imageSource.saveToFile(path, "png");
                    
                    if (saved) {
                        return new Promise( (resolve, reject) => {
                            console.log("setProfilePicture");
                            let task = this._apiGateway.setProfilePicture(path, fileName);
                            console.log("after setProfilePicture");

                            task.on('error', (e) => {
                                console.log(e);
                                console.log("error, ale done");
                                return resolve(false);
                            });
                            task.on('complete', (e) => {
                                console.log(e);
                                console.log("complete i done");
                                return resolve(true);
                            });
                        })
                    }
                    else {
                        LoaderService.hideLoader();
                        PopupFactory.GenericErrorPopup('Nie udał się zapis do pamięci telefonu');

                        return new Promise((resolve, reject) => reject(null));
                    }
                })
            })
        })
    }
}