<script setup lang="ts">
    import { ref, onMounted } from "vue";
    import { getCurrentUser } from "@/api/apiUsers.ts";
    import type { User } from "@/types/User.ts";

    const user = ref<User | null>(null);
    const loading = ref(true);

    onMounted(async () => {
    try {
    user.value = await getCurrentUser();
} finally {
    loading.value = false;
}
});
</script>

<template>
    <div v-if="loading">
        Loading...
    </div>

    <div v-else-if="user">
        <h2>{{ user.name }}</h2>
        <p>{{ user.id }}</p>
        <p>{{ user.status }}</p>
    </div>

    <div v-else>
        User not found
    </div>
</template>