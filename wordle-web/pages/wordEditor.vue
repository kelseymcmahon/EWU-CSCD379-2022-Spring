<template>
  <v-container fluid fill-height justify-center>
    <v-card :loading="getData" width="80%" class="pa-2">
      <v-card-title>
        <v-col>
            <h1 class="display-1">Game Words</h1>
        </v-col>
        <v-col align="right">
        <LoginPopUp @loginSuccess="loginSuccess" />
        </v-col>
      </v-card-title>

      <v-card-text>
        <v-row>
          <v-col>
            <v-text-field
              v-model="wordFilter"
              append-icon="mdi-magnify"
              label="Search"
              single-line
              hide-details
            ></v-text-field>
          </v-col>
          <v-col>
            <v-text-field
              v-model="newWord"
              label="Add a new word"
            ></v-text-field>
          </v-col>
          <v-col>
            <v-btn @click="addWord()">Add Word</v-btn>
          </v-col>
          <v-col>
            <v-select
              v-model="wordsPerPage"
              dense
              :items="items"
              outlined
              hint="Words per page"
              persistent-hint
              @input="getWords"
            ></v-select>
          </v-col>
        </v-row>

        <v-simple-table loading>
          <thead>
            <tr>
              <td>Word</td>
              <td>Playable</td>
              <td>Delete</td>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(word, index) in words" :key="index">
              <td colspan="4">{{ word.value }}</td>
              <td>
                <v-checkbox
                  v-model="word.common"
                  :disabled="!isUser"
                  color="primary"
                  @click="changeCommon(word.value, word.common)"
                ></v-checkbox>
              </td>
              <td>
                <v-btn icon @click="deleteWord(word.value)">
                  <v-icon small> mdi-delete </v-icon>
                </v-btn>
              </td>
            </tr>
          </tbody>
        </v-simple-table>
      </v-card-text>
      <v-card-actions>
        <v-pagination
          v-model="pageNum"
          :length="totalPages"
          :total-visible="10"
          @input="accessPage"
        ></v-pagination>
      </v-card-actions>
    </v-card>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'vue-property-decorator'
import LoginPopUp from '@/components/login.vue'

@Component({ components: { LoginPopUp } })
export default class WordEditor extends Vue {

  username: string = localStorage.getItem('editorName') || ''
  pageNum: number = 1
  words: any = []
  getData: boolean = true
  hasAuth: boolean = false
  isUser: boolean = false
  totalPages: number = 10
  wordsPerPage: number = 20
  wordFilter: string | null = ''
  newWord = ''
  items = [5, 10, 15, 20]

  mounted() {
    this.getWords()
  }

  @Watch('wordFilter')
  updateSearch() {
    this.pageNum = 1
    this.getWords()
  }

  getWords() {
    if (this.wordFilter === '') this.wordFilter = '?????'
    this.getData = true
    this.$axios
      .get('/api/Word/GetWordsPerPage', {
        params: {
          wordsPerPage: this.wordsPerPage,
          pageNum: this.pageNum,
          wordFilter: this.wordFilter,
        },
      })
      .then((response) => {
        this.words = response.data
        console.log('Current Page: ' + this.pageNum)
      })
    this.$axios
      .get('/api/Word/GetTotalWordCount', {
        params: { wordFilter: this.wordFilter },
      })
      .then((response) => {
        this.totalPages = Math.round(response.data / this.wordsPerPage)
        this.getData = false
      })
    if (this.wordFilter === '?????') this.wordFilter = ''
  }

  addWord() {
    this.$axios.get('/Token/testmasteroftheuniverse').then((response) => {
      if (response.data as boolean) {
        const newWord = this.newWord
        this.$axios
          .post('/api/Word/AddWord', null, {
            params: { newWord },
          })
          .then((response) => {
            this.getWords()
            console.log(response)
            console.log('Added word ' + this.newWord)
            this.newWord = ''
          })
      }
    })
  }

  addPageNum() {
    this.pageNum++
    this.getWords()
  }

  deleteWord(word: string) {
    this.$axios.get('/Token/testmasteroftheuniverse').then((response) => {
      if (response.data as boolean) {
        this.$axios
          .post('/api/Word/DeleteWord', null, {
            params: { givenWord: word },
          })
          .then((response) => {
            this.getWords()
            this.newWord = ''
          })
      }
      else {
        console.log("we got here!")
      }
    })
  }

  changeCommon(word: string, common: boolean) {
    if (this.isUser) {
      this.$axios
        .post('/api/Word/ChangeWordCommon', null, {
          params: { givenWord: word, common },
        })
        .then((response) => {
          console.log(response)
          console.log('Changed word ' + word + ' with added common: ' + common)
        })
    }
  }

  accessPage(newPage: number) {
    this.pageNum = newPage
    this.getWords()
  }

  loginSuccess(success: boolean) {
    this.isUser = success
    console.log(this.isUser)
  }
}
</script>
