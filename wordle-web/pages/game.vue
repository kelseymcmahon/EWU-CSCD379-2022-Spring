<template>
  <v-container fluid fill-height justify-center>

    <v-btn plain @click="dialog = true">
      <v-icon primary>
        mdi-account
      </v-icon>
      {{ playerName }}
    </v-btn>

    <v-dialog v-model="dialog" width="450">
      <v-card> 
        <v-card-title>Enter Your Name!</v-card-title>
        <v-card-text>
         Add you name to our score board so you can save your game scores!
         <v-text-field v-model=playerName></v-text-field>
        </v-card-text>
        <v-card-actions>
          <v-btn @click="addPlayer"> 
            Submit
          </v-btn>
          <v-btn @click="dialog=false"> I prefer to remain nameless </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-alert v-if="wordleGame.gameOver" width="80%" :type="gameResult.type">
      {{ gameResult.text }}
      <v-btn class="ml-2" @click="resetGame"> Play Again? </v-btn>
      <v-btn class="ml-2" @click="addStats" v-if="wordleGame.gameWon"> Add Score To Player Stats </v-btn>
    </v-alert>

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
  playerName = "Guest"
  dialog = false

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
    if(this.playerName === "Guest") {
      this.dialog = true;
    }
    else {
      this.$axios.post('/api/Player', {
      name: this.playerName,
      gameCount: 1,
      averageAttempts: this.wordleGame.guessNumber
      })
    }
    
  }

  addPlayer() {
    this.$axios.post('/api/Player', {
      name: this.playerName,
      gameCount: 0,
      averageAttempts: 0
      })

      this.dialog = false
  }
}
</script>
