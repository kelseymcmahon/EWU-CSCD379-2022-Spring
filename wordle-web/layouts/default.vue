<template>
  <v-app light>
    <v-app-bar
      :clipped-left="clipped"
      fixed
      app
      color="primary"
      dark
      height="50"
    >
      <v-btn icon to="/">
        <v-icon>mdi-cat</v-icon>
      </v-btn>
      <router-link to="/" class="title-bar-text">
        <v-toolbar-title> Generic Word Game </v-toolbar-title>
      </router-link>
      <v-spacer />
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
          <v-list-item>
            <v-list-item-icon>
              <v-icon>mdi-home</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Home</v-list-item-title>
          </v-list-item>

          <v-list-item>
            <v-list-item-icon>
              <v-icon>mdi-clippy</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Game</v-list-item-title>
          </v-list-item>

          <v-list-item>
            <v-list-item-icon>
              <v-icon>mdi-cat</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Info</v-list-item-title>
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

@Component({ components: { SettingsDialog } })
export default class DefaultLayout extends Vue {
  drawer = false
  dialog = false
}
</script>

<style>
.title-bar-text {
  text-decoration: none;
  font-weight: bold;
}

.title-bar-text a {
  color: white;
}

.title-bar-text:hover {
  opacity: 60%;
}
</style>
