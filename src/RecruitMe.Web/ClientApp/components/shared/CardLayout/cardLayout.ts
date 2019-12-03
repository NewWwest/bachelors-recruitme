import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';

@Component({
    name:"card-layout"
})
export default class CardLayout extends Vue {
    @Prop()
    maxWidth: string | undefined;
    @Prop()
    minWidth: string | undefined;

    defaultMaxWidth: string = "344px";
    defaultMinWidth: string = "344px";
}
