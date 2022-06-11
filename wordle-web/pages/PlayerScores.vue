<template>
  <v-container fluid fill-height justify-center>
    <v-card class="pa-5" :loading="getData" width="80%">
      <v-card-title>
        <h1 class="display-1">Player Stats</h1>
      </v-card-title>
      <v-card-text>
        <v-simple-table loading>
          <thead>
            <tr>
              <th>#</th>
              <th>Name</th>
              <th>Games</th>
              <th>Avg Attempts</th>
              <th>Avg Seconds</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(stat, index) in stats" :key="index">
              <td>
                {{ index + 1 }}
                <v-icon v-if="index === 0" color="#DAA520"> mdi-crown </v-icon>
                <v-icon v-if="index === 1" color="#A9A9A9"> mdi-crown </v-icon>
                <v-icon v-if="index === 2" color="#D2691E"> mdi-crown </v-icon>
              </td>
              <td>{{ stat.name }}</td>
              <td>{{ stat.gameCount }}</td>
              <td>{{ stat.averageAttempts }}</td>
              <td>{{ stat.averageSecondsPerGame }}</td>
            </tr>
          </tbody>
        </v-simple-table>
      </v-card-text>
      <v-card-actions>
        <v-btn color="primary" @click="refreshStats"> Refresh </v-btn>
      </v-card-actions>
    </v-card>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
@Component({})
export default class PlayerScores extends Vue {
  stats: any = []
  getData = true

  mounted() {
    this.$axios.get('/api/Players/GetTop10').then((response) => {
      this.stats = response.data
      this.getData = false
    })
  }

  refreshStats() {
    this.getData = true
    this.$axios.get('/api/Players/GetTop10').then((response) => {
      this.stats = response.data
      this.getData = false
    })
  }
}
</script>
