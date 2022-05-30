<template>
  <v-app light>
    <v-app-bar
        dense
        max-height="50"
      >  
        <router-link to="/">
        <game-logo  />
        </router-link>
        <v-spacer />
        <help />
        <settings-dialog />
        <v-app-bar-nav-icon @click="drawer = true"></v-app-bar-nav-icon>
      </v-app-bar>
    <v-main>
      <v-container>
        <Nuxt />
      </v-container>
    </v-main>

    <v-navigation-drawer v-model="drawer" absolute temporary right>
      <v-list nav dense>
        <v-list-item-group v-model="group" color="secondary">
          <v-list-item nuxt to="/">
            <v-list-item-icon>
              <v-icon>mdi-home</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Home</v-list-item-title>
          </v-list-item>

          <v-list-item nuxt to="/Game">
            <v-list-item-icon>
              <v-icon>mdi-clippy</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Game</v-list-item-title>
          </v-list-item>

          <v-list-item nuxt to="/DailyWord">
            <v-list-item-icon>
              <v-icon>mdi-clippy</v-icon>
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
              <v-icon>mdi-equalizer</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Daily Word Scores</v-list-item-title>
          </v-list-item>

          <v-list-item nuxt to="/About">
            <v-list-item-icon>
              <v-icon>mdi-cat</v-icon>
            </v-list-item-icon>
            <v-list-item-title>About</v-list-item-title>
          </v-list-item>

        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>

    <v-footer :absolute="!fixed" app>
      <v-spacer />
      <span>&copy; Kelsey McMahon {{ new Date().getFullYear() }}</span>
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

