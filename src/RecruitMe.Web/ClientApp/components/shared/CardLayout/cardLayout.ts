import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';

@Component({
    name:"card-layout"
})
export default class CardLayout extends Vue {
    @Prop()
    className: string | undefined;

    defaultClassName: string = "col-lg-5 col-md-6 col-sm-8 col-xs-10";
}
