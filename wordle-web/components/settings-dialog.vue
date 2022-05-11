<template>
  <div>
    <v-btn icon @click="dialog = true">
      <v-icon>mdi-cog</v-icon>
    </v-btn>

    <v-dialog v-model="dialog" width="450">
      <v-card>
        <v-card-title>Settings</v-card-title>
        <v-card-text>
          Choose a theme:
          <v-switch
            v-model="switch1"
            inset
            :label="message"
            @click="toggleDarkMode"
          >
          </v-switch>

          Choose a color:
          <v-list>
            <v-list-item-group>
              <v-list-item color="pink" @click="pinkTheme">
                <v-list-item-icon>
                  <v-icon color="pink">mdi-cat</v-icon>
                </v-list-item-icon>
                <v-list-item-title>Pinky</v-list-item-title>
              </v-list-item>
              <v-list-item color="blue" @click="blueTheme">
                <v-list-item-icon>
                  <v-icon color="blue">mdi-cat</v-icon>
                </v-list-item-icon>
                <v-list-item-title>I'm feeling blue</v-list-item-title>
              </v-list-item>
            </v-list-item-group>
          </v-list>
        </v-card-text>
      </v-card>
    </v-dialog>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import { colors } from 'vuetify/lib'

@Component
export default class SettingsDialog extends Vue {
  dialog = false
  message = 'Dark Mode'

  toggleDarkMode() {
    this.$vuetify.theme.dark = !this.$vuetify.theme.dark
    if (this.$vuetify.theme.dark) {
      this.message = 'Light Mode'
    } else {
      this.message = 'Dark Mode'
    }
  }

  pinkTheme() {
    const pinkTheme = {
      primary: colors.pink,
      accent: colors.pink.accent2,
      secondary: colors.purple,
      info: colors.pink.accent1,
      warning: colors.amber.base,
      error: colors.deepOrange.accent4,
      success: colors.green.accent3,
    }
    this.$vuetify.theme.themes.dark = pinkTheme
    this.$vuetify.theme.themes.light = pinkTheme
  }

  blueTheme() {
    const blueTheme = {
      primary: colors.blue,
      accent: colors.blue.accent4,
      secondary: colors.indigo,
      info: colors.blue.lighten3,
      warning: colors.amber.base,
      error: colors.deepOrange.accent4,
      success: colors.green.accent3,
    }
    this.$vuetify.theme.themes.dark = blueTheme
    this.$vuetify.theme.themes.light = blueTheme
  }
}
</script>
