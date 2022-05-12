<template>
  <v-container fluid fill-height>
    <v-btn outlined absolute top right color="primary" @click="dialog = true">
      <v-icon primary> mdi-account </v-icon>
      {{ playerName }}
    </v-btn>

    <v-dialog v-model="dialog" width="450" persistent>
      <v-card>
        <v-card-title>Enter Your Name!</v-card-title>
        <v-card-text>
          Add you name to our score board so you can save your game scores!
          <v-text-field v-model="playerName"></v-text-field>
        </v-card-text>
        <v-card-actions>
          <v-btn @click="addPlayer"> Submit </v-btn>
          <v-btn @click="dialog = false"> I prefer to remain nameless </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-row>
      <v-spacer />
      <v-alert v-if="wordleGame.gameOver" :type="gameResult.type" width="450">
        {{ gameResult.text }}
        {{ setGameTime() }}
        {{ addStats() }}
        <v-btn class="ml-2" @click="resetGame"> Play Again? </v-btn>
      </v-alert>
      <v-spacer />
    </v-row>

    <game-board :wordle-game="wordleGame" />

    <keyboard :wordle-game="wordleGame" />
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import { WordsService } from '~/scripts/wordsService'
import { GameState, WordleGame } from '~/scripts/wordleGame'
import KeyBoard from '@/components/keyboard.vue'
import GameBoard from '@/components/game-board.vue'
import { Word } from '~/scripts/word'

@Component({ components: { KeyBoard, GameBoard } })
export default class Game extends Vue {
  word: string = WordsService.getRandomWord()
  wordleGame = new WordleGame(this.word)
  playerName = 'Guest'
  dialog = false
  finalTime = 0

  resetGame() {
    this.word = WordsService.getRandomWord()
    this.wordleGame = new WordleGame(this.word)
  }

  get gameResult() {
    if (this.wordleGame.state === GameState.Won) {
      return { type: 'success', text: 'Yay! You won!' }
    }
    if (this.wordleGame.state === GameState.Lost) {
      return { type: 'error', text: `You lost... :( The word was ${this.word}` }
    }
    this.setGameTime()
    return { type: '', text: '' }
  }

  getLetter(row: number, index: number) {
    const word: Word = this.wordleGame.words[row - 1]
    if (word !== undefined) {
      return word.letters[index - 1]?.char ?? ''
    }
    return ''
  }

  addStats() {
    this.$axios.post('/api/Player', {
      name: this.playerName,
      gameCount: 1,
      averageAttempts: this.wordleGame.guessNumber,
      averageSeconds: this.finalTime,
    })
  }

  addPlayer() {
    this.$axios.post('/api/Player', {
      name: this.playerName,
      gameCount: 0,
      averageAttempts: 0,
      averageSeconds: 0,
    })
    this.dialog = false
  }

  setGameTime() {
    const now = new Date()
    this.finalTime = now.getSeconds()
  }
}
</script>
