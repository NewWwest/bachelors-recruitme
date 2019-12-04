import { ApiGateway } from '../common/apiGateway';
import PopupFactory from '../popupFactory';
import { ImageSource, fromUrl } from 'tns-core-modules/image-source/image-source'
import { LocalStorageService } from '../localStorage/localStorageService';

export class ImageService {
    private _userImage : ImageSource = new ImageSource();
    private _apiGateway: ApiGateway = new ApiGateway();

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
}