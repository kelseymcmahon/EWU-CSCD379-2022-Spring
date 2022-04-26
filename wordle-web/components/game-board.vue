<template>
  <v-container fluid="false">
    <v-row
      v-for="row in wordleGame.maxGuesses"
      :key="row"
      dense
      class="game-board">
      <v-spacer />
      <v-col v-for="index in wordleGame.currentWord.maxLetters" :key="index">
        <transition appear>
        <v-card
          height="50"
          width="50"
          :color="letterColor(getLetter(row, index))">
          <v-card-text class="letter-card text-center text-h5 font-weight-bold">
            {{ getChar(getLetter(row, index)) }}
          </v-card-text>
        </v-card>
        </transition>
      </v-col>
      <v-spacer />
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'
import { WordleGame } from '~/scripts/wordleGame'
import { Word } from '~/scripts/word'
import { Letter } from '~/scripts/letter'

@Component({ components: {} })
export default class GameBoard extends Vue {
  @Prop({ required: true })
  wordleGame!: WordleGame

  // value: string ="";

  // @Watch(this.value)
  // onPropertyChanged(value: string, oldValue: string) {
  //   // Do stuff with the watcher here.
  // }

  getLetter(row: number, index: number): Letter | null {
    const word: Word = this.wordleGame.words[row - 1]
    if (word !== undefined) {
      return word.letters[index - 1] ?? null
    }
    return null
  }

  getChar(letter: Letter | null) {
    if (letter === null) return ''
    return letter.char.toUpperCase()
  }

  letterColor(letter: Letter | null): string {
    if (letter === null) return ''
    return letter.letterColor
  }
}
</script>

<style>
.game-board .col {
  flex-grow: 0;
}

.letter-card .v-card__subtitle,
.v-card__text,
.v-card__title {
  padding: 10px;
}
</style>
