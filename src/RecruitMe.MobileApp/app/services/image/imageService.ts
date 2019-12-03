import { ApiGateway } from '../common/apiGateway';
import PopupFactory from '../popupFactory';
import { ImageSource, fromUrl } from 'tns-core-modules/image-source/image-source'
import { LocalStorageService } from '../localStorage/localStorageService';

export class ImageService {
    private _userImage : ImageSource;
    private _apiGateway: ApiGateway = new ApiGateway();

    public getUserPicture() : Promise<ImageSource> {
        const fileId: number | undefined = LocalStorageService.getProfileData().profilePictureFileId;
        const url: any = this._apiGateway.getImageRequest(fileId);
        
        fromUrl(url).then(r => {
            this._userImage = r;
            return this._userImage;
        }, err => {
            console.log(err);
            PopupFactory.GenericErrorPopup("" + err);

            return null;
        })
    }
}