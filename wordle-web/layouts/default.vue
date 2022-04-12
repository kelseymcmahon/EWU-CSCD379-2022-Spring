<template>
  <v-app light>

    <v-app-bar :clipped-left="clipped" fixed app color="secondary" dark>
      <v-spacer />
      <v-toolbar-title v-text="title" />
      <v-spacer />
      <v-btn icon @click.stop="rightDrawer = !rightDrawer">
        <v-icon>mdi-menu</v-icon>
      </v-btn>
    </v-app-bar>

    <v-main>
      <v-container>
        <Nuxt />
      </v-container>
    </v-main>

    <v-navigation-drawer v-model="rightDrawer" :right="right" temporary fixed>
      <v-list>
        <v-list-item
          v-for="(item, i) in items"
          :key="i"
          :to="item.to"
          router
          exact
        >
          <v-list-item-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title v-text="item.title" />
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-footer :absolute="!fixed" app>
      <v-spacer />
      <span>&copy; Kelsey McMahon {{ new Date().getFullYear() }}</span>
      <v-spacer />
    </v-footer>
  </v-app>
</template>

<script>
export default {
  name: 'DefaultLayout',
  data() {
    return {
      clipped: false,
      drawer: false,
      fixed: false,
      items: [
        {
          icon: 'mdi-chevron-right',
          title: 'Home',
          to: '/',
        },
        {
          icon: 'mdi-chevron-right',
          title: 'Game',
          to: '/Game',
        },
        {
          icon: 'mdi-chevron-right',
          title: 'Info',
          to: '/Info',
        },
      ],
      miniVariant: false,
      right: true,
      rightDrawer: false,
      title: 'Generic Word Game',
    }
  },
}
</script>
