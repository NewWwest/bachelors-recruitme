import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { LocalStorageService } from '../../services/localStorage.service';

// @ts-ignore
@Component
export default class Navmenu extends Vue {
    name: string = "";

    constructor() {
        super();
    }

    mounted() {
        this.name = (LocalStorageService.getEmail() != null ? LocalStorageService.getEmail() : "") as string;
    }
}
