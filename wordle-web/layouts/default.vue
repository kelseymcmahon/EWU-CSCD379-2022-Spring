<template>
  <v-app light>
    <v-app-bar dense max-height="50">
      <router-link to="/">
        <game-logo />
      </router-link>
      <v-spacer />
      <help />
      <settings-dialog />
      <v-app-bar-nav-icon @click="drawer = true" />
    </v-app-bar>
    <v-main class="main-page">
      <v-container>
        <Nuxt />
      </v-container>
    </v-main>

    <v-navigation-drawer v-model="drawer" app temporary right>
      <v-list nav dense>
        <v-list-item-group color="secondary">
          <v-list-item nuxt to="/">
            <v-list-item-icon>
              <v-icon>mdi-home</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Home</v-list-item-title>
          </v-list-item>

          <v-list-item nuxt to="/Game">
            <v-list-item-icon>
              <v-icon>mdi-file-word-box</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Game</v-list-item-title>
          </v-list-item>

          <v-list-item nuxt to="/DailyWord">
            <v-list-item-icon>
              <v-icon>mdi-star-circle</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Daily Word Game</v-list-item-title>
          </v-list-item>

          <v-list-item nuxt to="/PlayerScores">
            <v-list-item-icon>
              <v-icon>mdi-equalizer</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Player Scores</v-list-item-title>
          </v-list-item>

          <v-list-item nuxt to="/DailyWordScores">
            <v-list-item-icon>
              <v-icon>mdi-crown</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Daily Word Scores</v-list-item-title>
          </v-list-item>

          <v-list-item nuxt to="/About">
            <v-list-item-icon>
              <v-icon>mdi-cat</v-icon>
            </v-list-item-icon>
            <v-list-item-title>About</v-list-item-title>
          </v-list-item>

          <v-list-item nuxt to="/Instructions">
            <v-list-item-icon>
              <v-icon>mdi-help-circle</v-icon>
            </v-list-item-icon>
            <v-list-item-title>How To Play</v-list-item-title>
          </v-list-item>

          <v-list-item nuxt to="/WordEditor">
            <v-list-item-icon>
              <v-icon>mdi-pencil</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Word Editor</v-list-item-title>
          </v-list-item>

        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>

    <v-footer app>
      <v-spacer />
      <span>&copy; Kelsey McMahon &#38; Ray Tanner {{ new Date().getFullYear() }}</span>
      <v-spacer />
    </v-footer>
  </v-app>
</template>

<script lang="ts">
import { Vue, Component } from 'vue-property-decorator'
import SettingsDialog from '@/components/settings-dialog.vue'
import HelpDialog from '@/components/help.vue'

@Component({ components: { SettingsDialog, HelpDialog } })
export default class DefaultLayout extends Vue {
  drawer = false
  dialog = false
}
</script>

<style>
.main-page {
  background: url(static\wordle-bg.svg) bottom center no-repeat;
}

@media (max-width: 500px) {
  .main-page {
    background: url(static\wordle-bg-mobile.svg) bottom center no-repeat;
  }
}
</style>
