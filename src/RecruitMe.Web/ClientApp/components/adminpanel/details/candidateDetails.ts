import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';

@Component({
    name: "candidate-details"
})
export default class CandidateDetailsComponent extends Vue {
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
    handleDelete() {
    }
}
