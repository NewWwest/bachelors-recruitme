<template>
  <Frame>
      <StackLayout class="layout" horizontalAlignment="center" verticalAligment="center">
          <ActivityIndicator busy="true" color="green"></ActivityIndicator>
      </StackLayout>
  </Frame>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class ActivityLoader extends Vue {

}
</script>

<style scoped lang="scss">
    @import "../../app-common.scss";

    .layout {
        background-color: #cccccc33;
    }
</style>