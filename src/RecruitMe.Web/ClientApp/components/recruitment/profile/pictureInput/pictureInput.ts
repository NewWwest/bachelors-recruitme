import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import { PictureConfirmedEvent } from './pictureConfirmed.event';
import { ApiGateway } from '../../../../api/api.gateway';
import { getErrorMessage } from '../../../../helpers/error.helper';

@Component({})
export default class PictureInput extends Vue {
    apiGateway: ApiGateway = new ApiGateway();

    @Prop()
    fileId: number | undefined;
    @Prop()
    fileName: string | undefined

    originaPictureLoaded: boolean = false;
    originaPictureSrc: string = "/defaultProfilePicture.jpg";

    newPictureLoaded: boolean = false;
    newPictureData: string = "";
    newPictureFile: any = null;

    processing: boolean = false;
    cameraMode: boolean = false;
    snackbar: boolean = false;
    errorMessage: string = "";

    video: any;
    canvas: any;
    stream: any;

    mounted() {
        this.video = this.$refs.video;
    }

    updated() {
        if (this.fileId != null && this.fileId > 0 && !this.originaPictureLoaded) {
            this.apiGateway.getImage(this.fileId).then(d => {
                this.originaPictureLoaded = true;
                this.originaPictureSrc = `data:${d.contentType};${d.contentEncoding},` + d.file;
                this.$forceUpdate();
            }, err => {
                this.snackbar = true;
                this.errorMessage = getErrorMessage(err);
            });
        }
    }

    getdisplayedName(): string {
        return this.newPictureLoaded ? this.newPictureFile.name : (this.fileName != null ? this.fileName : "");
    }

    getdisplayedImage() {
        let src: string;
        if (this.newPictureLoaded) {
            src = this.newPictureData;
        }
        else {
            src = this.originaPictureSrc;
        }
        return { background: `url(${src})` };
    }

    pictureSelected(evt: any) {
        if (this.processing)
            return;

        this.processing = true;
        this.newPictureFile = evt.target.files[0];
        if (this.newPictureFile) {
            var reader = new FileReader();
            reader.onload = this.loadImage.bind(this);
            reader.onerror = this.handleError.bind(this);
            reader.readAsDataURL(this.newPictureFile);
        }
    }

    capture() {
        this.canvas = this.$refs.canvas;
        this.canvas.getContext("2d").drawImage(this.video, 0, 0, 288, 288);
        this.newPictureData = this.canvas.toDataURL("image/png");
        this.newPictureFile = this.dataURLtoFile(this.newPictureData, "ProfilePicture.png");

        this.switchCameraMode(false);
        this.savePicture();
    }

    savePicture() {
        if (this.processing || this.newPictureFile == null) {
            return;
        }

        this.newPictureLoaded = true;
        let evtData = new PictureConfirmedEvent(this.newPictureFile, this.newPictureFile.name)
        this.$emit("picture-confirmed", evtData);
        this.$forceUpdate();
    }

    handleError(e: any) {
        this.processing = false;

        this.snackbar = true;
        this.errorMessage = getErrorMessage(e);
    }

    loadImage(e: any) {
        this.processing = false;
        if (e && e.target) {
            this.newPictureData = e.target.result;
            this.savePicture();
        }
    }

    switchCameraMode(camMode: boolean) {
        this.cameraMode = camMode;
        if (this.cameraMode) {
            if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
                navigator.mediaDevices.getUserMedia({ video: true, audio: false }).then((stream: any) => {
                    this.stream = stream;
                    this.video.srcObject = stream;
                    this.video.play();
                }, (err: any) => {
                    this.snackbar = true;
                    this.errorMessage = err.name == "NotFoundError" ? "Nie wykryto podłączonej kamerki." : getErrorMessage(err);
                });
            }
        }
        else {
            this.video.pause();
            this.video.src = "";
            this.video.srcObject = null;
            this.stream.getTracks()[0].stop();
        }
    }


    dataURLtoFile(dataurl: any, filename: string) {
        let arr = dataurl.split(',');
        let mime = arr[0].match(/:(.*?);/)[1];
        let bstr = atob(arr[1]);
        let n = bstr.length;
        let u8arr = new Uint8Array(n);

        while (n--) {
            u8arr[n] = bstr.charCodeAt(n);
        }
        return new File([u8arr], filename, { type: mime });
    }

}
