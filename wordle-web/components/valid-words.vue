<template>
  <div>
    <v-btn @click="toggleDialog" class="ma-1"> Valid Words {{ validWordCount }} </v-btn>

    <v-dialog v-model="dialog" width="450">
      <v-card>
        <v-container>
          <v-btn
            v-for="word in validWords()"
            :key="word"
            color="secondary"
            class="mx-1 my-1"
            @click="addValidWord(word)">
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
    for (let i = 0; i < 5; i++) {
      this.wordleGame.currentWord.letters[i] = new Letter(word.charAt(i))
    }
    this.dialog = !this.dialog
  }
}
</script>
