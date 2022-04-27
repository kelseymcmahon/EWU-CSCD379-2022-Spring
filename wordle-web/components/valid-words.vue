<template>
  <div>
    <v-btn
      :disabled="wordleGame.gameOver"
      style="
        background: linear-gradient(
          302deg,
          rgba(0, 0, 0, 0.2),
          rgba(255, 255, 255, 0.2)
        );
      "
      color="primary"
      class="ms-1"
      @click="toggleDialog"
    >
      Valid Words {{ validWordCount }}
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
import { Letter } from '~/scripts/letter'

@Component
export default class ValidWords extends Vue {
  @Prop({ required: true })
  wordleGame!: WordleGame

  validWordCount: number = 0
  dialog = false

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
