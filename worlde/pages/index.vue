<template>
  <v-row justify="center" align="center">
    <v-col cols="12" sm="8" md="6">
      <v-card>
        <v-card-title class="headline">
          Welcome to Generic Word Game!
        </v-card-title>
        <v-card-text>
          <p>
            This game is an original word game that is fun for the whole family!
            This game will be the highlight at all your parties.
          </p>
          
        </v-card-text>
        <v-card-actions>
          <v-spacer />
          <v-btn color="primary" nuxt to="/Game"> {{buttonText}} </v-btn>
          <v-spacer />
        </v-card-actions>
      </v-card>
    </v-col>
  </v-row>
</template>

<script lang="ts">
import Vue from "vue"
import Component from "vue-class-component"
@Component
export default class IndexPage extends Vue{
    name: string = 'IndexPage';
    buttonText: string = 'Play the game';
    isLoading: boolean = false;

    wordsService() {
      
    }
}
</script>
