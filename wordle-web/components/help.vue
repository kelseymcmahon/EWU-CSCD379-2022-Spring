<template>
  <div>
    <v-btn icon @click="dialog = true">
      <v-icon>mdi-help-circle-outline</v-icon>
    </v-btn>

    <v-dialog v-model="dialog" width="450">
      <v-card>
        <v-card-title>How To Play Generic Word Game</v-card-title>
        <v-card-text>
          There is a secret 5 letter word choosen that you guess in less than 6 tries.
          After you submit your guess, the colors on the guess word changes to show you how close your guess was.
          <v-list>
                <v-list-item>
                    <v-list-item-icon>
                        <v-icon large color="success">mdi-file-word-box</v-icon>
                    </v-list-item-icon>
                    <v-list-item-content>
                        <v-list-item-title><b>Green:</b> Correct letter in the correct spot</v-list-item-title>
                    </v-list-item-content>
                </v-list-item>

                <v-list-item>
                    <v-list-item-icon>
                        <v-icon large color="warning">mdi-file-word-box</v-icon>
                    </v-list-item-icon>
                    <v-list-item-content>
                        <v-list-item-title><b>Yellow:</b> Correct letter in the wrong spot</v-list-item-title>
                    </v-list-item-content>
                </v-list-item>

                <v-list-item>
                    <v-list-item-icon>
                        <v-icon large color="info">mdi-file-word-box</v-icon>
                    </v-list-item-icon>
                    <v-list-item-content>
                        <v-list-item-title><b>Gray:</b> Wrong letter and wrong spot</v-list-item-title>
                    </v-list-item-content>
                </v-list-item>
          </v-list>
        </v-card-text>
      </v-card>
    </v-dialog>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'

@Component
export default class HelpDialog extends Vue {
  dialog = false
}
</script>
