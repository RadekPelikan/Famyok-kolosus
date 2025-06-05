<script lang="ts">
	import '../app.css';

	let { children } = $props();
</script>

<div class="grid h-screen">
	<header>
		<nav class="bg-sky-300">
			<ul class="container mx-auto flex max-w-80 justify-between">
				<li>
					<a href="/">Home</a>
				</li>
				<li>
					<a href="/chat">Chat</a>
				</li>
			</ul>
		</nav>
	</header>

	{@render children()}
</div>
