import { Goto } from "./services/router";

declare public module 'vue/types/vue' {
    interface Vue {
      $goto: Goto;
      $navigate(args, options);
    }
  }