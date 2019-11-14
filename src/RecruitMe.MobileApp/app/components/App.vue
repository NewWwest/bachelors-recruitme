<template>
  <RadSideDrawer ref="drawer" drawerLocation="Left" gesturesEnabled="true"
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

@Component
export default class App extends Vue {
  transition = new SlideInOnTopTransition();
}

</script>

<style scoped>

</style>
