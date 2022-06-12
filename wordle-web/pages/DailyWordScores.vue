<template>
  <v-container fluid fill-height justify-center>
    <v-card class="pa-5" :loading="getData" width="80%">
      <v-card-title>
        <h1 class="display-1">Daily Word Stats</h1>
      </v-card-title>
      <v-card-text>
        <v-simple-table loading>
          <thead>
            <tr>
              <th>Date</th>
              <th>Avg Seconds</th>
              <th>Avg Attempts</th>
              <th>Number of Games</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(stat, index) in stats" :key="index">
              <td>{{ stat.date }}</td>
              <td>{{ stat.averageSeconds }}</td>
              <td>{{ stat.averageAttempts }}</td>
              <td>{{ stat.gameCount }}</td>
              <td style="text-align: center">
                <v-checkbox v-model="stat.playedByPlayer" disabled />
              </td>
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
export default class DailyWordScores extends Vue {
  stats: any = []
  getData = true

  mounted() {
    this.getStats()
  }

  refreshStats() {
    this.getData = true
    this.getStats()
  }

  getStats() {
    this.$axios
      .get(`/DateWord/GetLast10DateWords`, {
        params: { playerName: this.userName },
      })
      .then((response) => {
        this.stats = response.data
        this.getData = false
      })
  }

  get userName() {
    const userName = localStorage.getItem('userName')
    if (userName == null || userName === '') {
      return 'Guest'
    }
    return userName
  }
}
</script>
