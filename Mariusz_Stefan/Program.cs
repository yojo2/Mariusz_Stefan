using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Mariusz_Stefan
{
	public class Program
	{
		public static void Main(string[] args)
			=> new Program().MainAsync().GetAwaiter().GetResult();

		public async Task MainAsync()
		{
			var client = new DiscordSocketClient();

			client.Log += Logger;
			client.MessageReceived += MessageReceived;

			var token = "MzA3NjEzMzU0OTUwNzg3MDcy.C-U27g.MDa8cJhnFFaL9JIalK7llW4MFrk"; // Remember to keep this private!
			await client.LoginAsync(TokenType.Bot, token);
			await client.StartAsync();

			// Block this task until the program is closed.
			await Task.Delay(-1);
		}

		private static Task Logger(LogMessage message)
		{
			var cc = Console.ForegroundColor;
			switch (message.Severity)
			{
				case LogSeverity.Critical:
				case LogSeverity.Error:
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				case LogSeverity.Warning:
					Console.ForegroundColor = ConsoleColor.Yellow;
					break;
				case LogSeverity.Info:
					Console.ForegroundColor = ConsoleColor.White;
					break;
				case LogSeverity.Verbose:
				case LogSeverity.Debug:
					Console.ForegroundColor = ConsoleColor.DarkGray;
					break;
			}
			Console.WriteLine($"{DateTime.Now,-19} [{message.Severity,8}] {message.Source}: {message.Message}");
			Console.ForegroundColor = cc;
			return Task.CompletedTask;
		}

		private async Task MessageReceived(SocketMessage message)
		{
			if (message.Content == "!ping")
			{
				await message.Channel.SendMessageAsync($"Pong {message.Author.Username}!");
			}
		}
	}
}