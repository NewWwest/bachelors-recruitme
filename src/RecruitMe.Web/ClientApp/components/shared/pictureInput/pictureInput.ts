import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { PictureConfirmedEvent } from './PictureConfirmedEvent';

@Component({})
export default class PictureInput extends Vue {
    filesrc: string = "/defaultProfilePicture.jpg";
    filename: string | null = null;
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
        let file = evt.target.files[0];
        this.filename = file.name
        if (file) {
            var reader = new FileReader();
            reader.onload = this.loadImage.bind(this);
            reader.onerror = this.handleError.bind(this);
            reader.readAsDataURL(file);
        }
        this.$emit("picture-selected", file);
    }

    PictureConfirmed(evt: any) {
        if (this.processing || this.filename == null) {
            return;
        }

        let evtData = new PictureConfirmedEvent(this.filesrc, this.filename)
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
