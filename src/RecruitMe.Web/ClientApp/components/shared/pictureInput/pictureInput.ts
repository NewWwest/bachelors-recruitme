import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import { PictureConfirmedEvent } from './PictureConfirmedEvent';
import { ApiGateway } from '../../../api/api.gateway';

@Component({})
export default class PictureInput extends Vue {
    defaultProfilePic: string = "/defaultProfilePicture.jpg";
    apiGateway: ApiGateway = new ApiGateway();

    @Prop()
    fileId: number | undefined;
    @Prop()
    fileName: string | undefined
    filesrc: string | undefined;

    displayedName: string = "";
    displayedImage: string = "";
    touched: boolean = false;

    file: any = null;
    fileBase64: string = "";
    processing: boolean = false;


    constructor() {
        super();
    }

    updated() {
        if (this.fileId != null && this.fileId > 0 && this.filesrc == null) {
            this.apiGateway.getImage(this.fileId).then(d => {
                this.filesrc = `data:${d.contentType};${d.contentEncoding},` + d.file;
                this.$forceUpdate();
            });
        }
    }

    getdisplayedName(): string {
        if (this.touched) {
            return this.file.name;
        }
        else {
            return this.fileName != null ? this.fileName : "";
        }
    }

    getdisplayedImage() {
        let src: string;
        if (this.touched) {
            src = this.fileBase64;
        }
        else {
            src = this.filesrc != null ? this.filesrc : this.defaultProfilePic;
        }
        return { background: `url(${src})` };
    }

    PictureSelected(evt: any) {
        if (this.processing)
            return;

        this.processing = true;
        this.file = evt.target.files[0];
        if (this.file) {
            var reader = new FileReader();
            reader.onload = this.loadImage.bind(this);
            reader.onerror = this.handleError.bind(this);
            reader.readAsDataURL(this.file);
        }
        this.$emit("picture-selected", this.file);
    }

    PictureConfirmed(evt: any) {
        if (this.processing || this.file == null) {
            return;
        }

        let evtData = new PictureConfirmedEvent(this.file, this.file.name)
        this.$emit("picture-confirmed", evtData);
    }

    handleError(e: any) {
        this.processing = false;
        console.error(e);
    }

    loadImage(e: any) {
        this.processing = false;
        if (e && e.target) {
            this.fileBase64 = e.target.result;
            this.touched = true;
            this.$forceUpdate();
        }
    }

    TakePhoto() {
        alert("integrate webcam");
    }

    doNothing() { }
}
