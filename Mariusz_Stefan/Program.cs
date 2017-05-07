using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Services;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Google.Apis.Services;

using Google.Apis.Translate.v2;
using Google.Apis.Translate.v2.Data;
using TranslationsResource = Google.Apis.Translate.v2.Data.TranslationsResource;

namespace Mariusz_Stefan
{
	public class Program
	{
		private static readonly string GoogleTranslateAPIKey = "AIzaSyBekSX7IyrgWGBYNpHP3sm4-_jnqgt4e94";
		private static readonly char SplitCharacter = ',';
		private readonly Random r = new Random();

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
			if (message.Channel.Name != "tts")
				return;

			string possibleCommand;
			if (message.Content.Contains(" "))
				possibleCommand = message.Content.Substring(0, message.Content.IndexOf(' ')).Trim();
			else
				possibleCommand = message.Content;

			switch (possibleCommand)
			{
				case "!help":
					await message.Channel.SendMessageAsync(Help());
					break;
				#region Gdzie/Komu/Kiedy

				case "!gdzie":
					await message.Channel.SendMessageAsync(Gdzie());
					break;
				case "!kim":
					await message.Channel.SendMessageAsync(Kim());
					break;
				case "!kto":
					await message.Channel.SendMessageAsync(Kto());
					break;
				case "!kogo":
				case "!czyje":
				case "!czyja":
				case "!czyim":
					await message.Channel.SendMessageAsync(Kogo());
					break;

				#endregion
				case "!pfrt":
					await message.Channel.SendMessageAsync(Pfrt());
					break;
				case "!tebeg":
					await message.Channel.SendMessageAsync(Tebeg());
					break;
				case "!ocen":
					await message.Channel.SendMessageAsync(Ocen());
					break;
				case "!fullwidth":
					await message.Channel.SendMessageAsync(FullWidth(message.Content.Substring("!fullwidth".Length)));
					break;
				case "!behe":
					await message.Channel.SendMessageAsync(Behe());
					break;
				case "!kiceg":
				case "!kicek":
					await message.Channel.SendMessageAsync(Kicek());
					break;
				case "!witam":
				case "!cześć":
				case "!czesc":
				case "!siemka":
					await message.Channel.SendMessageAsync(Witam());
					break;
				case "!czy":
					await message.Channel.SendMessageAsync(Czy());
					break;
				case "!ile":
					await message.Channel.SendMessageAsync(Ile());
					break;
				case "!mmm":
					await message.Channel.SendMessageAsync(new string('m', r.Next(3, 15)));
					break;
				case "!brawo":
				case "!boruc":
					await message.Channel.SendMessageAsync(Boruc());
					break;
				case "!taknie":
					await message.Channel.SendMessageAsync(TakNie());
					break;
				case "!abcd":
					await message.Channel.SendMessageAsync(Abcd());
					break;
				case "!bazinga":
					await message.Channel.SendMessageAsync(Bazinga());
					break;
				case "!piwo":
				case "!barman":
					await message.Channel.SendMessageAsync(LiwkoLiwkoSkoczPoPiwko(message.Channel));
					break;
				case "!pedal":
				case "!gej":
				case "!gejas":
				case "!gay":
					await message.Channel.SendMessageAsync(Pedal());
					break;
				default:
				{
					var content = message.Content;
					if (content.Length > 500)
						return;

					#region Pushing Gaywards

					if ("it gets bigger when I pull" == message.Content)
						await message.Channel.SendMessageAsync(new string('m', r.Next(3, 15)));

					if (Regex.IsMatch(content, "^(O|o)h shit (I|i)\'?m sorry(!|.)?$"))
						await message.Channel.SendMessageAsync("Sorry for what?");
					else if (Regex.IsMatch(content, "^m{8,}$"))
						await message.Channel.SendMessageAsync("I RIP THE SKIN!");
					#endregion

					else if (content.StartsWith("...") && r.Next(0, 10) == 1)
						await message.Channel.SendMessageAsync("Ruchasz psa jak sra");
					else if (content.Contains("opti") && r.Next(0, 10) == 1)
						await
							message.Channel.SendMessageAsync("Uprzejmie proszę o nieużywanie słowa \"opti\" na terenie #politbiuro. " +
							                                 "Dziękuję");
					else if (Regex.IsMatch(content, "^cz?(o+|0)$") && r.Next(0, 10) == 1)
						await message.Channel.SendMessageAsync("gówno 1:0");
					else if (content.StartsWith("8=D"))
						await message.Channel.SendMessageAsync(Dick());
					//else if (content.StartsWith("!trpl") && content.Length > 6)
					//{
					//	var str = await Translate(content.Substring(5), "pl");
					//	await message.Channel.SendMessageAsync(str);
					//}
					//else if (content.StartsWith("!tren") && content.Length > 6)
					//{
					//	var str = await Translate(content.Substring(5), "en");
					//	await message.Channel.SendMessageAsync(str);
					//}
					else
					{
						foreach (var s in Resources.wulg.Split(SplitCharacter))
						{
							if (!content.Contains(s)) continue;
							if (r.Next(0, 10) == 1)
								await message.Channel.SendMessageAsync(WulgResponse());
							else
								return;
						}
					}
				}
					break;
			}
		}

		#region Gdzie/Komu/Kiedy

		private string Gdzie()
		{
			return Extensions.RandomChoice(Resources.gdzie_prefix.Split(SplitCharacter).ToArray()).TrimEnd() + " " +
			       Extensions.RandomChoice(Resources.gdzie_suffix.Split(SplitCharacter).ToArray()).TrimStart();
		}

		private string Kim()
		{
			return Extensions.RandomChoice(Resources.kim.Split(SplitCharacter).ToArray()).Trim();
		}

		private string Kto()
		{
			return Extensions.RandomChoice(Resources.kto.Split(SplitCharacter).ToArray()).Trim();
		}

		private string Kogo()
		{
			return Extensions.RandomChoice(Resources.kogo.Split(SplitCharacter).ToArray()).Trim();
		}

		#endregion

		#region Misc

		private string Pfrt()
		{
			return new string('p', r.Next(1, 5)) +
			       new string('f', r.Next(2, 8)) +
			       new string('r', r.Next(3, 10)) + "t";
		}

		private string Tebeg()
		{
			return "T E B E G\nE\nB\nE\nG";
		}

		private string Ocen()
		{
			var mark = r.Next(0, 10);
			var retval = mark.ToString();

			if (mark != 0 && mark != 10)
				retval += Extensions.RandomChoice(new[] {"", ",5", "-", "+"}) + "/10";
			if (mark > 7)
				retval += Extensions.RandomChoice(new[] {"", "+ znak jakości CD-Action", "- Berlin poleca"});

			return retval;
		}

		private string FullWidth(string input)
		{
			var s = Regex.Replace(input.ToUpper(), ".{1}", "$0 ");
			return s.Trim();
		}

		private string Kicek()
		{
			return Extensions.RandomChoice(new[] {"kicek", "kiceg"}) + " mały " +
			       Extensions.RandomChoice(new[] {"bicek", "dicek"});
		}

		private string Behe()
		{
			return "Be" + string.Concat(Enumerable.Repeat("he", r.Next(2, 10))) + "mort";
		}

		private string Witam()
		{
			return Extensions.RandomChoice(Resources.witam.Split(SplitCharacter).ToArray());
		}

		private string Czy()
		{
			if (r.Next(0, 15) == 1)
				return "nie wiem, spytaj " + Kogo();
			return Extensions.RandomChoice(Resources.czy.Split(SplitCharacter).ToArray());
		}

		private string Ile()
		{
			var zeros = r.Next(1, 4);
			return r.Next((int) Math.Pow(10, zeros), (int) Math.Pow(10, zeros + 1)).ToString();
		}

		private string WulgResponse()
		{
			return Extensions.RandomChoice(Resources.wulg_response.Split(SplitCharacter).ToArray());
		}

		private string Dick()
		{
			return "8" + new string('=', r.Next(1, 20)) + "D" +
			       (r.Next(0, 3) == 1 ? Extensions.RandomChoice(new[] {" ((0)) ", " (.)(.)"}) : string.Empty);
		}

		private string TakNie()
		{
			return Extensions.RandomChoice(new[] {"tak", "nie"});
		}

		private string Abcd()
		{
			return Extensions.RandomChoice(new[] {"a", "b", "c", "d"});
		}

		private string Bazinga()
		{
			return Extensions.RandomChoice(new[] {"BAZINGA!", "BIGANPIZA!", "ZIMBABWE!"});
		}

		private string Boruc()
		{
			return Extensions.RandomChoice(new[] {"brawo brawo ", "brawo "}) + "Artur Boruc";
		}

		private string Help()
		{
			return "Mariusz Stefan poleca następujące funkcje: \n" +
			       "help, ,boruc, bazinga, abcd, taknie, czy, ile, witam, " +
			       "behe, kicek, tebeg, ocen, pfrt, kto, kogo, gdzie, kim, fullwidth, piwo";
		}

		private string Pedal()
		{
			return Extensions.RandomChoice(Resources.pedal.Split(','));
		}

		private string LiwkoLiwkoSkoczPoPiwko(IChannel msgChannel)
		{
			var usersList = msgChannel.GetUsersAsync(CacheMode.CacheOnly).ToArray().Result;
			var t = usersList.ElementAt(r.Next(0, usersList.Length)).Where(u => u.Status != UserStatus.Offline).ToArray();
			var user = t.ElementAt(r.Next(0, t.Length)).Mention;
			
			return user + ", " + Extensions.RandomChoice(new[] {"skocz", "idź", "przeleć się"}) + " do " + 
			       Extensions.RandomChoice(new[]
				       {"osiedlowego", "monopolowego", "lodówki", "barku", "sklepu", "Biedronki", "baru"}) + " po " +
			       Extensions.RandomChoice(new[]
			       {
				       "zimnego Lecha", "Ciechana miodowego", "Złotego Bażanta", "Heinekena",
				       "Carlsberga", "Pilsnera Urquella", "Budweisera", "Rolling Rock", "Koczkodana"
			       });
		}
		#endregion

		// TODO: returns 403 daily limit exceeded :(
		#region Google Translate

		private async Task<string> Translate(string input, string targetLang)
		{
			var service = new TranslateService(new BaseClientService.Initializer()
			{
				ApiKey = GoogleTranslateAPIKey,
				ApplicationName = "Mariusz_stefan"
			});

			// Execute the first translation request.
			try
			{

				var response = await service.Translations.List(input, targetLang).ExecuteAsync();
				var translations = new List<string>();
				foreach (var translation in response.Translations)
				{
					translations.Add(translation.TranslatedText);
					Console.WriteLine("translation :" + translation.TranslatedText);
				}

				return translations.FirstOrDefault();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				Console.WriteLine(e.StackTrace);
			}
			return string.Empty;
		}

		#endregion
	}
}