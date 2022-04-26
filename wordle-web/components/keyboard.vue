<template>

    <!-- <v-row v-for="(charRow, i) in chars" :key="i" no-gutters justify="center">
      <v-col v-for="char in charRow" :key="char" cols="1">
        <v-container class="text-center ">
          <v-btn
            min-width="50"
            height="60"
            
            :color="letterColor(char)"
            :disabled="wordleGame.gameOver"
            @click="setLetter(char)"
            class="gradient-2"
          >
            {{ char }}
          </v-btn>
        </v-container>
      </v-col>
    </v-row> -->

  <v-container fluid="false">
    <v-row
      v-for="(charRow, i) in chars" :key="i"
      class="keyboard">
      <v-spacer />
      <v-col v-for="char in charRow" :key="char" class="pa-1">
        <v-card
          height="50"
          width="40"
          :color="letterColor(char)"
          :disabled="wordleGame.gameOver"
          @click="setLetter(char)">
          <v-card-text class="letter-card text-center text-h6 font-weight-bold">
            {{ char }}
          </v-card-text>
        </v-card>
      </v-col>
      <v-spacer />
    </v-row>
 
    <v-row align="center" class="pa-2">
      <v-spacer />
          <v-btn :disabled="wordleGame.gameOver" @click="setLetter('?')" class="ms-1 gradient text-h6 font-weight-bold">
          ?
        </v-btn>

        <valid-words :wordle-game="wordleGame" />

        <v-btn :disabled="wordleGame.gameOver" @click="guessWord" class="ms-1 gradient font-weight-bold">
          Guess
        </v-btn>

        <v-btn :disabled="wordleGame.gameOver" @click="removeLetter" class="ms-1 gradient">
          <v-icon>mdi-backspace</v-icon>
        </v-btn>
        <v-spacer />
    </v-row>
   </v-container>  

</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'
import { Letter, LetterStatus } from '~/scripts/letter'
import { WordleGame } from '~/scripts/wordleGame'
import { colors } from 'vuetify/lib'

@Component
export default class KeyBoard extends Vue {
  @Prop({ required: true })
  wordleGame!: WordleGame

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
  .gradient {
    background: rgb(48,219,255);
    background: linear-gradient(302deg, rgba(48,219,255,1) 0%, rgba(27,129,210,1) 100%);
  }

  .gradient-2 {
    background: rgb(154,156,164);
    background: linear-gradient(302deg, rgba(154,156,164,1) 0%, rgba(199,202,212,1) 100%);
    font-weight: bold;
  }

  .keyboard .col {
  flex-grow: 0;
}

.letter-card .v-card__subtitle,
.v-card__text,
.v-card__title {
  padding: 10px;
}

</style>
