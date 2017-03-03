# DiscordBot
This is a discord bot

# Installing
- Add https://www.myget.org/F/discord-net/api/v3/index.json to your NuGet sources (under Tools -> Options -> NuGet Package Manager -> Package Sources)
- Check Include prereleases if you're using the NuGet browser
- Install Discord.Net 1.0.0.* from NuGet like you normally would.

This requires a framework which satisfies .netstandard1.3. I targeted the 4.6.1 Framework and ran into PlatformNotSupportException. I had to pass DiscordSocketClient a DiscordSocketConfig with a different WebSocketProvider. I pulled in Discord.Net.Providers.WS4Net (which also required WebSocket4Net) to satisfy this.

```
var client = new DiscordSocketClient(new DiscordSocketConfig
{
	WebSocketProvider = Discord.Net.Providers.WS4Net.WS4NetProvider.Instance
});
```