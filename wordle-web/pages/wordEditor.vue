<template>
  <v-container fluid fill-height justify-center>
    <v-card :loading="getData" width="80%">
      <v-card-title>
        <h1 class="display-1">Game Words</h1>
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
            <v-text-field label="Add a new word" v-model="newWord"></v-text-field>
          </v-col>
          <v-col>
            <v-btn @click="addWord()">Add Word</v-btn>
            
          </v-col>
          <v-col>
            <LoginPopUp />
          </v-col>
          <v-col>
            <v-select
            :items="items"
            outlined
            v-model="wordsPerPage"
            @input="getWords"
          ></v-select>
          </v-col>
        </v-row>
          
        <v-simple-table loading>
          <thead>
            <tr>
              <th>Word</th>
              <th>Playable</th>
              <th>Delete</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(word, index) in words" :key="index">
              <td>{{ word.value }}</td>
              <td>
                <v-checkbox
                v-model="word.common"
                color="red"
                @click="changeCommon(word.value, word.common)"
              ></v-checkbox></td>
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
        v-model="currentPage"
        :length="totalPages"
        @input="accessPage"
      ></v-pagination>
      </v-card-actions>

    </v-card>    
  </v-container>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'vue-property-decorator'
import LoginPopUp from '@/components/login.vue'
import { JWT } from '~/scripts/jwt';

@Component({ components: { LoginPopUp } })
export default class WordEditor extends Vue {

  pageNum: number = 1
  currentPage: number= 1
  words: any = []
  getData: boolean = true
  hasAuth: boolean = false;
  totalPages: number = 10
  wordsPerPage: number = 20
  wordFilter: string = "a"
  newWord = ""
  items = [5, 10, 15, 20]

  mounted() {
    this.getWords();
  }

  // mounted() {
  //   if (!this.stopwatch.isRunning) {
  //     this.stopwatch.Start()
  //   }
  //   this.retrieveUserName()
  //   setTimeout(() => {
  //     this.isLoaded = true
  //   }, 2500)
  //   this.$axios
  //     .post('Token/GetToken', {
  //       username: 'Admin@intellitect.com',
  //       password: 'P@ssw0rd123',
  //     })
  //     .then((result) => {
  //       JWT.setToken(result.data.token, this.$axios)
  //       // console.log(result)
  //       console.log(JWT.tokenData)
  //       console.log(JWT.tokenData.roles)
  //       // this.$axios.defaults.headers.common.Authorization =
  //       //   'Bearer ' + result.data.token
  //       this.$axios.get('Token/TestAdmin').then((result) => {
  //         console.log(result)
  //       })
  //     })
  // }

  @Watch("wordFilter")
    getWords(){
      var wordsPerPage = this.wordsPerPage
      var pageNum = this.pageNum
      var wordFilter = this.wordFilter
      this.getData = true
      this.$axios.get('/api/Word/GetWordsPerPage', { params: { wordsPerPage : wordsPerPage, pageNum : pageNum, wordFilter : wordFilter } }).then((response) => {
      this.words = response.data
      this.pageNum = 1
      console.log("Current Page: " + pageNum)
    })
    this.$axios.get('/api/Word/GetTotalWordCount', { params: { wordFilter } }).then((response) => {
      this.totalPages = Math.round(response.data / this.wordsPerPage)
      this.getData = false;
    })
    }


//  if(response.data as boolean){
//       var newWord = this.newWord
//       this.$axios.post('/api/Word/AddWord', null, {
//         params: { newWord: newWord}
//       }).then((response) => {
//         this.getWords()
//         console.log(response)
//         console.log("Added word " + this.newWord)
//         this.newWord = ""
//       })
    addWord() {

      
      this.$axios.get('/Token/testmasteroftheuniverse').then((response) => {
        if(response.data as boolean){
          var newWord = this.newWord
      this.$axios.post('/api/Word/AddWord', null, {
        params: { newWord: newWord}
      }).then((response) => {
        this.getWords()
        console.log(response)
        console.log("Added word " + this.newWord)
        this.newWord = ""
      })
        }
      })
      
     
    }

    addPageNum() {
      this.pageNum++
      this.getWords()
    }


    deleteWord(word : string) {

     this.$axios.get('/Token/testmasteroftheuniverse').then((response) => {
      
      if(response.data as boolean){

      this.$axios.post('/api/Word/DeleteWord', null, {
        params: { givenWord: word}
      }).then((response) => {
        this.getWords()
        this.newWord = ""
      })
      }
     
    })

    }

  changeCommon(word : string, common : boolean) {
    
     this.$axios.get('/Token/testmasteroftheuniverse').then((response) => {
      
      if(response.data as boolean){
    this.$axios.post('/api/Word/ChangeWordCommon', null, {
        params: { givenWord: word, common: common }
      }).then((response) => {
        console.log(response)
        console.log("Changed word " + word + " with added common: " + common)
      })
      }
     })
  }

  accessPage(newPage : number){
    this.pageNum = newPage
    this.getWords();
  }

}
</script>