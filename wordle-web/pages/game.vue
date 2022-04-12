<template>
  <v-row justify="center" align="center">
    <v-col cols="12" sm="8" md="6">
      <v-card>
        <v-card-title class="headline">
          Welcome to Generic Word Game!
        </v-card-title>
        <v-card-text>
          <p>
            How to play: 
          </p>
          <ol>
            <li>Enter in a word guess that is 5 letters long.</li>
            <li>Click submit to see your how your guess did.</li>
          </ol>
          <br>
          <v-text-field v-model="inputMessage" placeholder="Enter a guess" outlined>
          </v-text-field>

          <v-alert dense border="left" type="warning">
            {{errorMessage}}
          </v-alert>

          <v-btn color="secondary" @click="checkLength"> Submit </v-btn>

        </v-card-text>
        <v-card-actions>
          <v-spacer />
          <v-btn color="primary" nuxt to="/"> Return Home </v-btn>
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
export default class wordService extends Vue{
    name: string = 'IndexPage';
    inputMessage: string = "";
    errorMessage: string = "Please enter a word with 5 letters";
    checkLength() {
      if (this.inputMessage.length > 5) {
        return false;
      }

      else {
        return true;
      }
      // this.inputMessage;
      // console.log(this.inputMessage);
    }
}
</script>