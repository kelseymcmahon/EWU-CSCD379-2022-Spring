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
               <v-switch v-model="switch1" inset :label=message @click="toggleDarkMode"></v-switch>

               Choose a color:
               <v-radio-group
                v-model="ex7"
                column
              >
                <v-radio
                  label="red"
                  color="red"
                  value="red"
                ></v-radio>
                <v-radio
                  label="red darken-3"
                  color="red darken-3"
                  value="red darken-3"
                ></v-radio>
                <v-radio
                  label="indigo"
                  color="indigo"
                  value="indigo"
                ></v-radio>
              </v-radio-group>
          </v-card-text>
      </v-card>
    </v-dialog>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator'

@Component
export default class SettingsDialog extends Vue {
    dialog = false
    message = "Dark Mode"

    toggleDarkMode() {
    this.$vuetify.theme.dark = !this.$vuetify.theme.dark
    if(this.$vuetify.theme.dark) {
        this.message = "Light Mode"
    }
    else {
        this.message = "Dark Mode"
    }
  }
}

</script>
