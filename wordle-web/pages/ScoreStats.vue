<template>
  <v-container fluid fill-height justify-center>
    <v-card>
      <v-card-title>
        <h1 class="display-1">Player Stats</h1>
      </v-card-title>
      <v-card-text>
        <v-simple-table>
          <thead>
            <tr>
              <th>#</th>
              <th>Name</th>
              <th>Score</th>
              <th>Avg. Seconds</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(stat, index) in stats" :key="index">
              <td>{{ index }}</td>
              <td>{{ stat.name }}</td>
              <td>{{ stat.gameCount }}</td>
              <td>{{ stat.averageAttempts }}</td>
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
export default class ScoreStats extends Vue {

  stats: any = []

  refreshStats() {
    this.$axios.get('/api/Player').then((response) => {
      this.stats = response.data
    })
  }

}
</script>