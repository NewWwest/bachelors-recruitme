import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { PictureConfirmedEvent } from './PictureConfirmedEvent';

@Component({})
export default class PictureInput extends Vue {
    filesrc: string = "/defaultProfilePicture.jpg";
    file: any = null;
    processing: boolean = false;

    constructor() {
        super();
    }

    mounted() {
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
        if (e && e.target)
            this.filesrc = e.target.result;
    }
    TakePhoto() {
        alert("integrate webcam");
    }
    doNothing() { }
}
