<template>
  <div>
    <v-btn
      v-if="isMobile()"
      :disabled="wordleGame.gameOver"
      color="primary"
      @click="toggleDialog"
    >
      {{ validWordCount }}
      Hints
    </v-btn>
    <v-btn
      v-if="!isMobile()"
      class="absolute-bottom-right"
      :disabled="wordleGame.gameOver"
      color="primary"
      @click="toggleDialog"
    >
      <v-icon>mdi-cat</v-icon>
      {{ validWordCount }}
      Hints
    </v-btn>
    <v-dialog v-model="dialog" width="450">
      <v-card>
        <v-container>
          <v-btn
            v-for="word in validWords()"
            :key="word"
            color="secondary"
            class="mx-1 my-1"
            @click="addValidWord(word)"
          >
            {{ word }}
          </v-btn>
        </v-container>
      </v-card>
    </v-dialog>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'
import { WordleGame } from '~/scripts/wordleGame'

@Component
export default class ValidWords extends Vue {
  @Prop({ required: true })
  wordleGame!: WordleGame

  validWordCount: number = 0
  dialog = false

  isMobile() {
    return this.$vuetify.breakpoint.xsOnly
  }

  toggleDialog() {
    this.dialog = !this.dialog
  }

  validWords() {
    const wordList = this.wordleGame.getWildcardWords()
    this.validWordCount = wordList.length
    return wordList
  }

  addValidWord(word: string) {
    this.wordleGame.changeCurrentWord(word)
    this.dialog = !this.dialog
  }
}
</script>

<style>
.absolute-bottom-right {
  position: absolute;
  bottom: 20px;
  right: 20px;
}
</style>
