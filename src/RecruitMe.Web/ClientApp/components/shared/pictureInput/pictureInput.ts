import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({})
export default class PictureInput extends Vue {
    filesrc: string ="/defaultProfilePicture.jpg";

    //TODO: HELP MEEE INJECT THIS
    //PictureConfirmed:any = () => console.log("XD");
    //PictureSelected:any =() => console.log("XD2");

    constructor() {
        super();
    }

    mounted() {
    }

    PictureSelected(evt:any){
        let file = evt.target.files[0];
        if (file) {
            var reader = new FileReader();
            reader.onload = this.loadImage.bind(this);
            reader.readAsDataURL(file);
        }
    }
    
    loadImage(e:any){
        if(e && e.target)
            this.filesrc=e.target.result;
    }

    PictureConfirmed(evt: any){
        console.log(evt);
    }
}
