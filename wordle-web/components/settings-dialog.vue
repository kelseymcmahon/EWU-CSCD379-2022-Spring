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
            @click="toggleDarkMode">
            </v-switch>

          Choose a color:
              <v-list>
                <v-list-item-group>
                  <v-list-item @click="pinkTheme" color="pink">
                    <v-list-item-title> PINK!!! </v-list-item-title>
                  </v-list-item>
                  <v-list-item @click="blueTheme" color="blue">
                    <v-list-item-title> I'm feeling blue </v-list-item-title>
                  </v-list-item>
                  <v-list-item @click="revertTheme" color="orange">
                    <v-list-item-title>Back to normal</v-list-item-title>
                  </v-list-item>
                </v-list-item-group>
              </v-list>

        </v-card-text>
      </v-card>
    </v-dialog>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'
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
      accent: colors.pink.accent3,
      secondary: colors.purple,
      info: colors.purple.accent2,
      warning: colors.pink.accent1,
      error: colors.pink.accent3,
      success: colors.deepPurple.lighten4,
    }
    this.$vuetify.theme.themes.dark = pinkTheme
    this.$vuetify.theme.themes.light = pinkTheme
  }

  blueTheme() {
    const blueTheme = {
      primary: colors.blue,
      accent: colors.blue.accent3,
      secondary: colors.indigo,
      info: colors.blue.accent2,
      warning: colors.indigo.accent1,
      error: colors.indigo.accent3,
      success: colors.deepPurple.lighten4,
    }
    this.$vuetify.theme.themes.dark = blueTheme
    this.$vuetify.theme.themes.light = blueTheme
  }

  revertTheme() {
    this.$vuetify.theme.themes.dark = this.$vuetify.theme.themes.dark
    this.$vuetify.theme.themes.light = this.$vuetify.theme.themes.light
  }
}
</script>
