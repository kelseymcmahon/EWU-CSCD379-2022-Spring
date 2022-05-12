<template>
<div>

  <v-row justify="center" align="center">
    <v-col cols="12" sm="8" md="6" justify="center" align="center">

        <v-spacer />
        <game-logo-text />
        <v-spacer />

    </v-col>
  </v-row>

  <v-row justify="center" align="center">
    <v-col cols="12" sm="8" md="6">
      <v-card class="pa-10">
        <v-card-text>
          <p>
            This game is an original word game that is fun for the whole family!
            This game will be the highlight at all your parties.
          </p>
        </v-card-text>
        <v-card-actions>
          <v-spacer />
          <v-btn color="primary" nuxt to="/Game"> Play the game! </v-btn>
          <v-btn color="secondary" nuxt to="/PlayerScores">
            Player Scores
          </v-btn>
          <v-spacer />
        </v-card-actions>
      </v-card>
    </v-col>
  </v-row>
</div>
</template>

<script lang="ts">
import Vue from 'vue'
import Component from 'vue-class-component'

@Component
export default class IndexPage extends Vue {
  name: string = 'IndexPage'
}
</script>
