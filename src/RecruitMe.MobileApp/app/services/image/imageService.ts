import { ApiGateway } from '../common/apiGateway';
import PopupFactory from '../popupFactory';
import { LocalStorageService } from '../localStorage/localStorageService';
import { requestPermissions, takePicture, CameraOptions } from 'nativescript-camera';
import * as imagepicker from 'nativescript-imagepicker';
import LoaderService from '../loaderView/loader';
import { ImageSource, fromUrl } from 'tns-core-modules/image-source/image-source'
import * as fs from 'tns-core-modules/file-system'
import { ImageAsset } from 'tns-core-modules/image-asset/image-asset';

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
                return this.saveImageAsset(imageAsset);
            })
        })
    }

    public choosePicture() {
        let context = imagepicker.create({
            mode: 'single',
            mediaType: imagepicker.ImagePickerMediaType.Image
        });

        return context.authorize().then(_ => context.present())
            .then(selection => {
                return this.saveImageAsset(selection[0]);
            })
    }

    saveImageAsset(imageAsset: ImageAsset): any {
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
                    let task = this._apiGateway.setProfilePicture(path, fileName);

                    task.on('error', (e) => {
                        return resolve(false);
                    });
                    task.on('complete', (e) => {
                        return resolve(imageSource);
                    });
                })
            }
            else {
                LoaderService.hideLoader();
                PopupFactory.GenericErrorPopup('Nie udał się zapis do pamięci telefonu');

                return new Promise((resolve, reject) => reject(null));
            }
        })
    }
}