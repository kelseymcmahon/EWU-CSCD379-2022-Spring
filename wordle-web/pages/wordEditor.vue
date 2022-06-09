<template>
  <v-container fluid fill-height justify-center>
    <v-card :loading="getData" width="80%">
      <v-card-title>
        <h1 class="display-1">Game Words</h1>
      </v-card-title>
      <v-card-text>
          <v-text-field
          v-model="wordFilter"
          append-icon="mdi-magnify"
          label="Search"
          single-line
          hide-details
        ></v-text-field>
        <v-simple-table loading>
          <thead>
            <tr>
              <th>Word</th>
              <th>Playable</th>
              <th>Edit</th>
              <th>Delete</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(stat, index) in stats" :key="index">
              <td>{{ stat.value }}</td>
              <td>{{ stat.common }}</td>
              <td> 
                <v-btn icon> 
                    <v-icon small> mdi-pencil </v-icon>
                </v-btn>
              </td>
              <td> 
                <v-btn icon> 
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

}
</script>