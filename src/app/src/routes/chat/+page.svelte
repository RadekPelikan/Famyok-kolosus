<script lang="ts">
	import { marked } from 'marked';

	type Character = {
		id: string;
		name: string;
		img: string;
	};

	type MessageType = {
		id: string;
		dateSend: Date;
		sender: Character;
		content: string;
	};

	type ChatRoomPropType = {
		name: string;
		description: string;
		onlinePlayers: Character[];
		messages: MessageType[];
	};

	const messages = $state<MessageType[]>([]);

	const addMessage = (senderName: string, content: string, img = 'default.png') => {
		messages.push({
			id: crypto.randomUUID(),
			dateSend: new Date(Date.now()),
			sender: {
				id: crypto.randomUUID(),
				name: senderName,
				img
			},
			content
		});
	};

	addMessage('Pepa Houska', 'Ahoj');
	addMessage('Joe Doe', 'No Nazdar \n # Ahoj');

	new Array(30).fill(0).forEach((n, i) => addMessage(`Joe Doe ${i}`, `Test ${i}`));
</script>

<section class="container mx-auto my-4 overflow-y-scroll">
	<ul class="grid gap-4 rounded bg-sky-200 px-2 py-4">
		{#each messages as message, index (message.id)}
			<li class="bg-inherit hover:brightness-90">
				<div class="flex">
					<div class="h-fit overflow-clip rounded-full">
						<img
							src={message.sender.img}
							alt={message.sender.img}
							class=" aspect-square max-w-10"
						/>
					</div>
					<div>
						<p>
							{message.sender.name}
						</p>
						<p>
							{message.dateSend.toLocaleString()}
						</p>
					</div>
				</div>
				<p class="marked">
					{@html marked.parse(message.content)}
				</p>
			</li>
		{/each}
	</ul>
</section>

<p>Hello</p>
