<template>
  <v-container fluid class="pa-0">
    <v-container v-if="isMobile()" fluid class="pa-0">
      <v-row v-for="(charRow, i) in chars" :key="i" dense width="100%">
        <v-spacer />
        <v-col
          v-for="char in charRow"
          :key="char"
          cols="1"
          style="box-sizing: unset; flex-basis: 0; padding: 3px"
        >
          <v-btn
            v-if="char !== '?' && char !== 'enter' && char !== 'back'"
            height="25"
            min-width="18"
            light
            :color="letterColor(char)"
            style="background-color: lightgray"
            :disabled="wordleGame.gameOver"
            class="text-h6 font-weight-bold pa-1 ma-0"
            @click="setLetter(char)"
          >
            {{ char }}
          </v-btn>
          <v-btn
            v-if="char === '?'"
            height="25"
            min-width="18"
            color="primary"
            :disabled="wordleGame.gameOver"
            class="text-h6 font-weight-bold pa-1 ma-0"
            @click="setLetter('?')"
          >
            ?
          </v-btn>
        </v-col>
        <v-spacer />
      </v-row>
      <v-row class="ma-1">
        <v-col align="center" class="pa-2">
          <valid-words :wordle-game="wordleGame" />
        </v-col>
        <v-col align="center" class="pa-2">
          <v-btn
            color="primary"
            :disabled="wordleGame.gameOver"
            @click="guessWord"
          >
            enter
          </v-btn>
        </v-col>
        <v-col align="center" class="pa-2">
          <v-btn
            color="primary"
            :disabled="wordleGame.gameOver"
            @click="removeLetter"
          >
            <v-icon small color="white">mdi-backspace</v-icon>
          </v-btn>
        </v-col>
      </v-row>
    </v-container>
    <v-container v-if="!isMobile()">
      <valid-words :wordle-game="wordleGame" />
      <v-row v-for="(charRow, i) in chars" :key="i" class="keyboard">
        <v-spacer />
        <v-col v-for="char in charRow" :key="char" class="pa-1">
          <v-btn
            v-if="char === 'enter'"
            height="45"
            min-width="35"
            color="primary"
            :disabled="wordleGame.gameOver"
            class="font-weight-bold"
            @click="guessWord"
          >
            Enter
          </v-btn>
          <v-btn
            v-if="char !== '?' && char !== 'enter' && char !== 'back'"
            height="45"
            min-width="35"
            light
            :color="letterColor(char)"
            style="background-color: lightgray"
            :disabled="wordleGame.gameOver"
            class="text-h6 font-weight-bold"
            @click="setLetter(char)"
          >
            {{ char }}
          </v-btn>
          <v-btn
            v-if="char === '?'"
            height="45"
            min-width="35"
            color="primary"
            :disabled="wordleGame.gameOver"
            class="text-h6 font-weight-bold"
            @click="setLetter('?')"
          >
            ?
          </v-btn>
          <v-btn
            v-if="char === 'back'"
            height="45"
            min-width="35"
            color="primary"
            :disabled="wordleGame.gameOver"
            @click="removeLetter"
          >
            <v-icon>mdi-backspace</v-icon>
          </v-btn>
        </v-col>
        <v-spacer />
      </v-row>
    </v-container>
    <!-- 
    <v-dialog v-model="dialog" width="450">
      <v-card color="error" dark>
        <v-container>
          <v-card-title> <v-icon>mdi-chat-alert</v-icon> Ooops! </v-card-title>
          <v-card-text>
            You need to enter a valid word. Try again!
          </v-card-text>
        </v-container>
      </v-card>
    </v-dialog> -->
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
    return this.$vuetify.breakpoint.xsOnly
  }

  chars = [
    ['q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p'],
    ['a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l'],
    ['enter', 'z', 'x', 'c', 'v', 'b', 'n', 'm', '?', 'back'],
  ]

  setLetter(char: string) {
    this.wordleGame.currentWord.addLetter(char)
  }

  removeLetter() {
    this.wordleGame.currentWord.removeLetter()
  }

  guessWord() {
    if (
      this.wordleGame.currentWord.length ===
      this.wordleGame.currentWord.maxLetters
    ) {
      this.wordleGame.submitWord()
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
