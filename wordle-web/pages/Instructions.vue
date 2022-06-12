<template>
  <v-container fluid="true">
    <v-row justify="center">
      <!-- Instructions Title Cards -->
      <v-col :cols="cols">
        <v-card flat color="transparent">
          <v-card-title class="justify-center">
            <h1>How To Play</h1>
          </v-card-title>
        </v-card>
      </v-col>

      <!-- Instructions Details Cards -->
      <v-col :cols="cols">
          <div class="d-flex align-center">
            <v-container>
              <v-card class="pa-5">
                <v-icon class="pa-1 my-2">mdi-check</v-icon>You have to guess
                the Generic Word in six guesses or less. <v-spacer></v-spacer>
                <v-icon class="pa-1 my-2">mdi-check</v-icon> Letters can be used
                more than once. <v-spacer></v-spacer>
                <v-icon class="pa-1 my-2">mdi-check</v-icon> Answers are never
                plurals.<v-spacer></v-spacer>
                <v-icon class="pa-1 my-2">mdi-lightbulb-on-outline</v-icon>
                There are a list of "available words" for you to choose from
                <v-spacer></v-spacer>

                <v-icon class="pa-1 my-2">mdi-lightbulb-on-outline</v-icon>
                You can place
                <v-btn color="primary" small min-width="30px" height="30px" class="min-button text-h6 font-weight-bold pa-0 my-4">?</v-btn>
                anywhere and choose from the list.<v-spacer></v-spacer>
                <h-4 class="pa-1 my-2">(color varies by theme)</h-4>

                <v-divider></v-divider>
                <h2 class="pa-1 mt-2">Example:</h2>

                <!-- BASIC word -->
                <v-btn color="info" tile small min-width="40px" height="40px" class="min-button text-h5 font-weight-bold pa-0 mt-4 mb-2">B</v-btn>
                <v-btn color="warning" tile small min-width="40px" height="40px" class="min-button text-h5 font-weight-bold pa-0 mt-4 mb-2">A</v-btn>
                <v-btn color="info" tile small min-width="40px" height="40px" class="min-button text-h5 font-weight-bold pa-0 mt-4 mb-2">S</v-btn>
                <v-btn color="info" tile small min-width="40px" height="40px" class="min-button text-h5 font-weight-bold pa-0 mt-4 mb-2">I</v-btn>
                <v-btn color="info" tile small min-width="40px" height="40px" class="min-button text-h5 font-weight-bold pa-0 mt-4 mb-2">C</v-btn>
                <v-spacer></v-spacer>
                The A is IN the word but in the WRONG spot, the rest are not in
                the word. <v-spacer></v-spacer>

                <!-- APPLE word -->
                <v-btn color="success" tile small min-width="40px" height="40px" class="min-button text-h5 font-weight-bold pa-0 mt-4 mb-2">A</v-btn>
                <v-btn color="info" tile small min-width="40px" height="40px" class="min-button text-h5 font-weight-bold pa-0 mt-4 mb-2">P</v-btn>
                <v-btn color="info" tile small min-width="40px" height="40px" class="min-button text-h5 font-weight-bold pa-0 mt-4 mb-2">P</v-btn>
                <v-btn color="warning" tile small min-width="40px" height="40px" class="min-button text-h5 font-weight-bold pa-0 mt-4 mb-2">L</v-btn>
                <v-btn color="warning" tile small min-width="40px" height="40px" class="min-button text-h5 font-weight-bold pa-0 mt-4 mb-2">E</v-btn>
                <v-spacer></v-spacer>
                The A is IN the word and in the CORRECT spot.<v-spacer
                ></v-spacer>
                The L, E is IN the word but in the WRONG spot, the rest are not
                in the word.
                <v-spacer></v-spacer>

                <!-- ANGEL word -->
                <h2 class="mt-4">Correct word:</h2>

                <v-spacer></v-spacer>
                <v-btn color="success" tile small min-width="40px" height="40px" class="min-button text-h5 font-weight-bold pa-0 mt-4 mb-2">A</v-btn>
                <v-btn color="success" tile small min-width="40px" height="40px" class="min-button text-h5 font-weight-bold pa-0 mt-4 mb-2">N</v-btn>
                <v-btn color="success" tile small min-width="40px" height="40px" class="min-button text-h5 font-weight-bold pa-0 mt-4 mb-2">G</v-btn>
                <v-btn color="success" tile small min-width="40px" height="40px" class="min-button text-h5 font-weight-bold pa-0 mt-4 mb-2">E</v-btn>
                <v-btn color="success" tile small min-width="40px" height="40px" class="min-button text-h5 font-weight-bold pa-0 mt-4 mb-2">L</v-btn>
                <v-spacer></v-spacer>
              </v-card>
            </v-container>
          </div>
      </v-col>
      <!--END Instructions Details Cards -->
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from 'vue'
import Component from 'vue-class-component'
@Component
export default class InstructionsPage extends Vue {
  get cols(): string {
    switch (this.$vuetify.breakpoint.name) {
      case 'xs':
      case 'sm':
        return '12'
      default:
        return '7'
    }
  }
}
</script>

<style>
.min-button::before {
  display: none;
}
</style>
