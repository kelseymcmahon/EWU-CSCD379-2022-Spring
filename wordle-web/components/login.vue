<template>
  <div class>

    <v-btn @click="toggleDialog">
        Login
    </v-btn>

    <v-dialog v-model="dialog" width="450">
      <v-card>
        <v-container>
          <v-card-title>Login</v-card-title>

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
              required
            ></v-text-field>
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
import { Vue, Component } from 'vue-property-decorator'
import { JWT } from '~/scripts/jwt';
@Component({})
export default class LoginPopUp extends Vue {
    username: string = localStorage.getItem("editorName") || "";
    password: string = "";
    dialog: boolean = true;
    toggleDialog() {
    this.dialog = !this.dialog
    console.log(this.dialog)
    }
    getToken(){
      this.$axios
      .post('Token/GetToken', {
        username: this.username,
        password: this.password,
      })
      .then((result) => {
        if (result.status == 200){
        JWT.setToken(result.data.token, this.$axios)
        localStorage.setItem("editorName" , this.username);
        this.$emit("loginSuccess", true)
        this.dialog = false;
        } 
      }).catch((error) =>{
        this.$emit("loginSuccess", false);
      })
    }
}
</script>