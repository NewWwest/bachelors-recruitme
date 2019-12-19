import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';

@Component({
    name:"card-layout"
})
export default class CardLayout extends Vue {
    @Prop()
    className: string | undefined;

    defaultClassName: string = "col-sm-3 col-xs-6";
}
