<template>
  <v-container :="false">
    <v-row v-for="(charRow, i) in chars" :key="i" class="keyboard">
      <v-spacer />
      <v-col v-for="char in charRow" :key="char" class="pa-1" lg="auto" md="auto" xs="1">
        <v-card
          height="50"
          width="35"
          style="background: linear-gradient(302deg, rgba(0, 0, 0, 0.2), rgba(255, 255, 255, 0.2));"
          :color="letterColor(char)"
          :disabled="wordleGame.gameOver"
          @click="setLetter(char)"
        >
          <v-card-text class="letter-card text-center text-h6 font-weight-bold">
            {{ char }}
          </v-card-text>
        </v-card>
      </v-col>
      <v-spacer />
    </v-row>

    <v-row align="center" class="pa-2">
      <v-spacer />
      <v-btn
        :disabled="wordleGame.gameOver"
        style="background: linear-gradient(302deg, rgba(0, 0, 0, 0.2), rgba(255, 255, 255, 0.2));"
        color="primary"
        class="ms-1 text-h6"
        @click="setLetter('?')"
      >
        ?
      </v-btn>

      <valid-words :wordle-game="wordleGame" />

      <v-btn
        :disabled="wordleGame.gameOver"
        style="background: linear-gradient(302deg, rgba(0, 0, 0, 0.2), rgba(255, 255, 255, 0.2));"
        color="primary"
        class="ms-1"
        @click="guessWord"
      >
        Guess
      </v-btn>

      <v-btn
        :disabled="wordleGame.gameOver"
        style="background: linear-gradient(302deg, rgba(0, 0, 0, 0.2), rgba(255, 255, 255, 0.2));"
        color="primary"
        class="ms-1"
        @click="removeLetter"
      >
        <v-icon>mdi-backspace</v-icon>
      </v-btn>
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

  chars = [
    ['q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p'],
    ['a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l'],
    ['z', 'x', 'c', 'v', 'b', 'n', 'm'],
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

