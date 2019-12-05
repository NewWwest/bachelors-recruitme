import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';

@Component({
    name: "exam-details"
})
export default class DetailsComponent extends Vue {
    @Prop()
    id: number | undefined;

    mounted() {
        //TODO fetch
        console.log(this.id);
    }
    updated() {

        console.log(this.id);
    }
    
    handleSubmit() {
    }
}
