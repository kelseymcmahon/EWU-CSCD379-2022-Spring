<template>
  <div class>
    <v-btn @click="toggleDialog"> 
        <v-icon> mdi-account </v-icon> 
        {{ loginText }}
    </v-btn>

    <v-dialog v-model="dialog" width="450">
      <v-card class="pa-3">
        <v-container>
          <v-card-title>Login To Edit the Words</v-card-title>
          <v-row>
            <v-col>
              <v-text-field
                v-model="username"
                label="Username"
                required
              ></v-text-field>
            </v-col>

            <v-col>
              <v-text-field
                v-model="password"
                label="Password"
                :type="hidePassword ? 'text' : 'password'"
                :append-icon="showIcon ? 'mdi-eye' : 'mdi-eye-off'"
                @click:append="showIcon = !showIcon; hidePassword = !hidePassword"
                required
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col>
            <v-alert v-model="showError" type="error" text dense outlined>
                Incorrect username or password
            </v-alert>
            </v-col>
          </v-row>
        </v-container>
        <v-card-actions>
          <v-btn @click="getToken"> Submit </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script lang="ts">
import { Vue, Component, Watch } from 'vue-property-decorator'
import { JWT } from '~/scripts/jwt'
@Component({})
export default class LoginPopUp extends Vue {
  username: string = localStorage.getItem('editorName') || ''
  password: string = ''
  dialog: boolean = true
  showError: boolean = false
  loginText = "Login"
  showIcon = false
  hidePassword = false

  toggleDialog() {
    this.dialog = !this.dialog
  }

  @Watch('password')
  updateError() {
    this.showError = false
  }

  @Watch('dialog')
    updateError2() {
    this.showError = false
  }

  getToken() {
    this.$axios
      .post('Token/GetToken', {
        username: this.username,
        password: this.password,
      })
      .then((result) => {
        if (result.status === 200) {
          JWT.setToken(result.data.token, this.$axios)
          localStorage.setItem('editorName', this.username)
          this.$emit('loginSuccess', true)
          this.dialog = false
          this.loginText = "Logged in as " + this.username
        }
      })
      .catch((error) => {
        console.log(error)
        this.$emit('loginSuccess', false)
        this.showError = true;
      })
  }
}
</script>
