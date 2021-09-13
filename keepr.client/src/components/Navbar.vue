<template>
  <nav class="navbar navbar-expand-lg navbar-light nav-border">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center" title="Go to Homepage">
        <h1 class="m-0">Keepr</h1>
      </div>
    </router-link>
    <button
      class="navbar-toggler"
      type="button"
      data-toggle="collapse"
      data-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav mr-auto">
        <li class="nav-item" title="Go to Homepage">
          <router-link :to="{ name: 'Home' }" class="nav-link text-dark-grey">
            Home
          </router-link>
        </li>
        <li v-if="account.id" class="nav-item" title="Go to your Profile Page">
          <router-link :to="{ name: 'Profile', params: {id: account.id}}" class="nav-link text-dark-grey">
            Profile
          </router-link>
        </li>
      </ul>
      <span class="navbar-text">
        <button
          class="btn btn-outline-primary text-uppercase"
          title="log in"
          @click="login"
          v-if="!user.isAuthenticated"
        >
          Login
        </button>

        <div class="dropdown" v-else>
          <div
            class="dropdown-toggle"
            @click="state.dropOpen = !state.dropOpen"
          >
            <img
              :src="user.picture"
              alt="user photo"
              height="40"
              class="rounded"
            />
            <span class="mx-3 text-dark-grey hoverable">{{ user.name }}</span>
          </div>
          <div
            class="dropdown-menu p-0 list-group w-100"
            :class="{ show: state.dropOpen }"
            @click="state.dropOpen = false"
          >
            <div
              class="list-group-item list-group-item-action hoverable"
              title="log out"
              @click="logout"
            >
              logout
            </div>
          </div>
        </div>
      </span>
    </div>
  </nav>
</template>

<script>
import { AuthService } from '../services/AuthService'
import { AppState } from '../AppState'
import { computed, reactive } from 'vue'
export default {
  setup() {
    const state = reactive({
      dropOpen: false
    })
    return {
      state,
      account: computed(()=>AppState.account),
      user: computed(() => AppState.user),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style scoped>
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}
.nav-link{
  text-transform: uppercase;
}
.nav-item .nav-link.router-link-exact-active{
  color: var(--primary);
}
</style>
