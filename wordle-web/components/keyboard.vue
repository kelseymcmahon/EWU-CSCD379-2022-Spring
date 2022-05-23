<template>
  <v-container fluid class="pa-0">
  <v-container fluid v-if="isMobile()" class="pa-0">
    <v-row v-for="(charRow, i) in chars" :key="i" dense flex>
      <v-spacer />
      <v-col v-for="char in charRow" :key="char" 
        cols="1"
        height="37"
        >
        <v-btn
          flex
          width="100%"
          min-width="0"
          color="primary"
          :disabled="wordleGame.gameOver"
          class="text-h7 font-weight-bold"
          v-if="char === 'enter'"
          @click="guessWord">
            <v-icon small>mdi-arrow-down</v-icon>
        </v-btn>
        <v-btn
          flex
          width="100%"
          min-width="0"
          :color="letterColor(char)"
          style="background-color: lightgray"
          :disabled="wordleGame.gameOver"
          @click="setLetter(char)"
          class="text-h7 font-weight-bold"
          v-if="char !== '?' && char !== 'enter' && char !== 'back'"
        >
            {{ char }}
        </v-btn>
        <v-btn
          flex
          width="100%"
          min-width="0"
          color="primary"
          :disabled="wordleGame.gameOver"
          class="text-h7 font-weight-bold"
          v-if="char === '?'"
          @click="setLetter('?')">
            ?
        </v-btn>
        <v-btn
          flex
          width="100%"
          min-width="0"
          color="primary"
          :disabled="wordleGame.gameOver"

          v-if="char === 'back'"
          @click="removeLetter">
            <v-icon small>mdi-backspace</v-icon>
        </v-btn>
      </v-col>
      <v-spacer />
    </v-row>
  </v-container>
  <v-container v-if="!isMobile()">
    <v-row v-for="(charRow, i) in chars" :key="i" class="keyboard">
      <v-spacer />
      <v-col v-for="char in charRow" :key="char" class="pa-1">
        <v-btn
          height="45"
          min-width="35"
          color="primary"
          :disabled="wordleGame.gameOver"
          class="font-weight-bold"
          v-if="char === 'enter'"
          @click="guessWord">
            Enter
        </v-btn>
        <v-btn
          height="45"
          min-width="35"
          :color="letterColor(char)"
          style="background-color: lightgray"
          :disabled="wordleGame.gameOver"
          @click="setLetter(char)"
          class="text-h6 font-weight-bold"
          v-if="char !== '?' && char !== 'enter' && char !== 'back'"
        >
            {{ char }}
        </v-btn>
        <v-btn
          height="45"
          min-width="35"
          color="primary"
          :disabled="wordleGame.gameOver"
          class="text-h6 font-weight-bold"
          v-if="char === '?'"
          @click="setLetter('?')">
            ?
        </v-btn>
        <v-btn
          height="45"
          min-width="35"
          color="primary"
          :disabled="wordleGame.gameOver"
          v-if="char === 'back'"
          @click="removeLetter">
            <v-icon>mdi-backspace</v-icon>
        </v-btn>
      </v-col>
      <v-spacer />
    </v-row>

    <v-dialog v-model="dialog" width="450">
      <v-card color="error" dark>
        <v-container>
          <v-card-title> <v-icon>mdi-chat-alert</v-icon> Ooops! </v-card-title>
          <v-card-text>
            You need to enter a valid word. Try again!
          </v-card-text>
        </v-container>
      </v-card>
    </v-dialog>

  </v-container>

  </v-container>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'
import { Letter, LetterStatus } from '~/scripts/letter'
import { WordleGame } from '~/scripts/wordleGame'

@Component
export default class KeyBoard extends Vue {
  @Prop({ required: true })
  wordleGame!: WordleGame

  dialog = false

  isMobile() {
    return this.$vuetify.breakpoint.smAndDown;
  }

  chars = [
    ['q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p'],
    ['a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l'],
    ['enter','z', 'x', 'c', 'v', 'b', 'n', 'm', '?', 'back'],
  ]

  setLetter(char: string) {
    this.wordleGame.currentWord.addLetter(char)
  }

  removeLetter() {
    this.wordleGame.currentWord.removeLetter()
  }

  guessWord() {
    if (this.wordleGame.checkIfWordExists(this.wordleGame.currentWord.text)) {
      if (
        this.wordleGame.currentWord.length ===
        this.wordleGame.currentWord.maxLetters
      ) {
        this.wordleGame.submitWord()
      }
    } else {
      this.dialog = true
    }
  }

  letterColor(char: string): string {
    if (this.wordleGame.correctChars.includes(char)) {
      return Letter.getColorCode(LetterStatus.Correct)
    }
    if (this.wordleGame.wrongPlaceChars.includes(char)) {
      return Letter.getColorCode(LetterStatus.WrongPlace)
    }
    if (this.wordleGame.wrongChars.includes(char)) {
      return Letter.getColorCode(LetterStatus.Wrong)
    }

    return Letter.getColorCode(LetterStatus.Unknown)
  }
}
</script>

<style>
.keyboard .col {
  flex-grow: 0;
}

.letter-card .v-card__subtitle,
.v-card__text,
.v-card__title {
  padding: 10px;
}
</style>