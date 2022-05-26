<template>
  <v-container fluid fill-height class="pa-0">
    <v-btn outlined absolute top right color="primary" @click="dialog = true">
      <v-icon primary> mdi-account </v-icon>
      {{ playerName }}
    </v-btn>
    
    <valid-words :wordle-game="wordleGame" />
    
    <v-dialog v-model="dialog" width="450" persistent>
      <v-card>
        <v-card-title>Enter Your Name!</v-card-title>
        <v-card-text>
          Add you name to our score board so you can save your game scores!
          <v-text-field 
            v-model="playerName"
            type="text"
            placeholder="Guest">
          </v-text-field>
        </v-card-text>
        <v-card-actions>
          <v-btn @click=";(dialog = false), setUserName(playerName)"> Save </v-btn>
          <v-btn @click="dialog = false"> I prefer to remain nameless </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-row>
      <v-spacer />
      <v-alert v-if="wordleGame.gameOver" :type="gameResult.type" width="450">
        {{ gameResult.text }}
        <v-btn class="ml-2" @click="resetGame"> Play Again? </v-btn>
      </v-alert>
      <v-spacer />
    </v-row>

    <game-board :wordle-game="wordleGame" />

    <keyboard :wordle-game="wordleGame" />

    <v-overlay v-if="this.overlay">

        <v-progress-circular
          indeterminate
          absolute
          size="64">
        </v-progress-circular>
        <br>
        <v-chip class="ma-2" outlined>
          Getting Daily Word ...
        </v-chip>
    </v-overlay>
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
export default class DailyWordGame extends Vue {
  word: string = ""
  wordleGame = new WordleGame(this.word)
  playerName = ''
  dialog = false
  isLoaded: boolean = false
  timeInSeconds: number = 0
  startTime: number = 0
  endTime: number = 0
  intervalID: any
  overlay = true;

  mounted() {
    setTimeout(() => this.startTimer(), 5000)
    this.retrieveUserName()
    this.getRandomWord()
  }

  getRandomWord() {
    this.overlay = true;
    this.$axios.get('/DateWord/GetRandomWord').then((response) => {
      this.word = response.data  
    }).finally(() => {
      this.overlay = false
      this.wordleGame = new WordleGame(this.word)
    });
  }

  resetGame() {
    this.getRandomWord()
    this.timeInSeconds = 0
    this.startTimer()
  }

  get gameResult() {
    this.stopTimer()
    this.timeInSeconds = Math.floor(this.endTime - this.startTime)
    if (this.wordleGame.state === GameState.Won) {
      if (
        this.playerName.toLocaleLowerCase() !== 'guest' &&
        this.playerName !== ''
      ) {
        this.endGameSave()
      } else {
        this.dialog = true
      }
      return { type: 'success', text: 'You won! :^)' }
    }
    if (this.wordleGame.state === GameState.Lost) {
      return {
        type: 'error',
        text: `You lost... :^( The word was ${this.word}`,
      }
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

  retrieveUserName() {
    const userName = localStorage.getItem('userName')
    if (userName == null) {
      this.playerName = 'Guest'
    } else {
      this.playerName = userName
    }
  }

  setUserName(userName: string) {
    localStorage.setItem('userName', userName)
    if (this.wordleGame.state === GameState.Won) {
      this.endGameSave()
    }
  }

  startTimer() {
    this.startTime = Date.now() / 1000
    this.intervalID = setInterval(this.updateTimer, 1000)
  }

  updateTimer() {
    this.timeInSeconds = Math.floor(Date.now() / 1000 - this.startTime)
  }

  stopTimer() {
    this.endTime = Date.now() / 1000
    clearInterval(this.intervalID)
  }

  endGameSave() {
    this.$axios.post('/api/Players', {
      name: this.playerName,
      attempts: this.wordleGame.words.length,
      seconds: this.timeInSeconds,
    })
  }
}
</script>
