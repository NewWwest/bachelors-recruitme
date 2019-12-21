<template>
  <RadSideDrawer ref="drawer" drawerLocation="Left" :gesturesEnabled="areGesturesEnabled"
    :drawerTransition="transition">
    <StackLayout ~drawerContent backgroundColor="#ffffff">
      <slot name="drawerContent"></slot>
    </StackLayout>
    <Frame ~mainContent ref="drawerMainContent">
      <slot name="mainContent"></slot>
    </Frame>
  </RadSideDrawer>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { SlideInOnTopTransition } from "nativescript-ui-sidedrawer";
import { UserService } from '../services/userService/userService';

@Component
export default class App extends Vue {
  transition = new SlideInOnTopTransition();

  get areGesturesEnabled() {
    return UserService.isLoggedIn();
  }
}
</script>
