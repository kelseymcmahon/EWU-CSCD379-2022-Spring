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
            <tr v-for="(stat, index) in stats" :key="index">
              <td>{{ stat.value }}</td>
              <td>
                <v-checkbox
                v-model="stat.common"
                color="red"
                @click="changeCommon(stat.value, stat.common)"
              ></v-checkbox></td>
              <td> 
                <v-btn icon @click="deleteWord(stat.value)"> 
                    <v-icon small> mdi-delete </v-icon>
                </v-btn>
              </td>
            </tr>
          </tbody>
        </v-simple-table>
      </v-card-text>
      <v-card-actions>
          <v-pagination
        v-model="this.currentPage"
        :length="this.totalPages"
        @input="this.nextPage"
      ></v-pagination>
      </v-card-actions>
    </v-card>    
  </v-container>
</template>

<script lang="ts">
import { Component, Vue, Watch } from 'vue-property-decorator'
@Component({})
export default class WordEditor extends Vue {
  stats: any = []
  getData: boolean = true
  pageNum: number = 1;
  wordsPerPage: number = 20;
  totalPages: number = 10;
  wordFilter: string = "a"
  newWord = ""

  mounted() {
    var wordsPerPage = this.wordsPerPage
    var pageNum = this.pageNum
    var wordFilter = this.wordFilter
    this.$axios.get('/api/Word/GetWordsPerPage', { params: { wordsPerPage : wordsPerPage, pageNum : pageNum, wordFilter : wordFilter } }).then((response) => {
      this.stats = response.data
    })
    this.$axios.get('/api/Word/GetTotalWordCount', { params: { wordFilter } }).then((response) => {
      this.totalPages = Math.round(response.data / this.wordsPerPage)
    })
  }

  @Watch("wordFilter")
    doSomething(){
        var wordsPerPage = this.wordsPerPage
        var pageNum = this.pageNum
        var wordFilter = this.wordFilter
        this.$axios.get('/api/Word/GetWordsPerPage', { params: { wordsPerPage, pageNum, wordFilter } }).then((response) => {
            this.stats = response.data
        })
    }

    addWord() {
      var newWord = this.newWord
      this.$axios.post('/api/Word/AddWord', null, {
        params: { newWord: newWord}
      }).then((response) => {
        console.log(response)
        console.log("Added word " + this.newWord)
        this.newWord = ""
        this.doSomething()
      })
      
  }

  changeCommon(word : string, common : boolean) {
    this.$axios.post('/api/Word/ChangeWordCommon', null, {
        params: { givenWord: word, common: common }
      }).then((response) => {
        console.log(response)
        console.log("Changed word " + word + " with added common: " + common)
      })
  }

  deleteWord(word : string) {
    this.$axios.post('/api/Word/DeleteWord', null, {
        params: { givenWord: word }
      }).then((response) => {
        console.log(response)
        console.log("Deleted word " + word)
        this.doSomething()
      })
    
  }

}
</script>