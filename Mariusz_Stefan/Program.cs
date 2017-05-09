using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

//todo:
// -IMAGE SEARCH
// -move slap command tets to resx
// -create 3 dictionaries with string -> proper func mapping

namespace Mariusz_Stefan
{
	public class Program
	{
		private static readonly string YouTubeDataApiKey = "AIzaSyBrWyFrmFM7whCXLTjpqIxXB23X2jQdH4I";
		private static readonly string GoogleSearchApiKey = "AIzaSyCufIRURGbtamFxKMBvD1-0eVkdnHRm1YY";
		private static readonly string GoogleSearchEngineId = "006368784700686828841:4upmxfu-xhe";
		private static readonly char SplitCharacter = ',';
		private CustomsearchService _customsearchService;

		private readonly Dictionary<string, Func<string>> _zeroArgumentCommands = new Dictionary<string, Func<string>>();
		private readonly Dictionary<string, Func<string, string, ISocketMessageChannel, string>> _slapCommands 
			= new Dictionary<string, Func<string, string, ISocketMessageChannel, string>>();
		private readonly Dictionary<string, Func<string, string>> _oneStringArgumentCommands = new Dictionary<string, Func<string, string>>();
		private readonly Dictionary<string, Func<ISocketMessageChannel, string>> _oneChannelArgumentCommands = new Dictionary<string, Func<ISocketMessageChannel, string>>();

		private readonly Dictionary<string, bool> _channelWhitelist = new Dictionary<string, bool>();

		private void InitDictionaries()
		{
			_channelWhitelist.Add("tts", true);
			_channelWhitelist.Add("politbiuro", true);
			_channelWhitelist.Add("linki", true);

			_zeroArgumentCommands.Add(".help", Help);
			_zeroArgumentCommands.Add(".gdzie", Gdzie);
			_zeroArgumentCommands.Add(".kim", Kim);
			_zeroArgumentCommands.Add(".kto", Kto);
			_zeroArgumentCommands.Add(".kogo", Kogo);
			_zeroArgumentCommands.Add(".czyje", Kogo);
			_zeroArgumentCommands.Add(".czyja", Kogo);
			_zeroArgumentCommands.Add(".czyim", Kogo);
			_zeroArgumentCommands.Add(".pfrt", Pfrt);
			_zeroArgumentCommands.Add(".tebeg", Tebeg);
			_zeroArgumentCommands.Add(".ocen", Ocen);
			_zeroArgumentCommands.Add(".behe", Behe);
			_zeroArgumentCommands.Add(".kiceg", Kicek);
			_zeroArgumentCommands.Add(".kicek", Kicek);
			_zeroArgumentCommands.Add(".witam", Witam);
			_zeroArgumentCommands.Add(".cześć", Witam);
			_zeroArgumentCommands.Add(".czesc", Witam);
			_zeroArgumentCommands.Add(".siemka", Witam);
			_zeroArgumentCommands.Add(".czy", Czy);
			_zeroArgumentCommands.Add(".ile", Ile);
			_zeroArgumentCommands.Add(".mmm", () => new string('m', _random.Next(3, 15)));
			_zeroArgumentCommands.Add(".brawo", Boruc);
			_zeroArgumentCommands.Add(".boruc", Boruc);
			_zeroArgumentCommands.Add(".taknie", TakNie);
			_zeroArgumentCommands.Add(".abcd", Abcd);
			_zeroArgumentCommands.Add(".bazinga", Bazinga);
			_zeroArgumentCommands.Add(".rimshot", () => "Ba-dum-pish!");
			_zeroArgumentCommands.Add(".pedal", Pedal);
			_zeroArgumentCommands.Add(".gej", Pedal);
			_zeroArgumentCommands.Add(".gejas", Pedal);
			_zeroArgumentCommands.Add(".gay", Pedal);

			_oneChannelArgumentCommands.Add(".piwo", LiwkoLiwkoSkoczPoPiwko);
			_oneChannelArgumentCommands.Add(".barman", LiwkoLiwkoSkoczPoPiwko);

			_oneStringArgumentCommands.Add(".fullwidth", FullWidth);
			_slapCommands.Add(".slap", Slap);

		}

		private readonly Random _random = new Random();

		public static void Main(string[] args)
			=> new Program().MainAsync().GetAwaiter().GetResult();

		public async Task MainAsync()
		{
			InitDictionaries();

			_customsearchService = new CustomsearchService(new BaseClientService.Initializer {ApiKey = GoogleSearchApiKey});

			var client = new DiscordSocketClient();

			client.Log += Logger;
			client.MessageReceived += MessageReceived;

			var token = "MzA3NjEzMzU0OTUwNzg3MDcy.C-U27g.MDa8cJhnFFaL9JIalK7llW4MFrk"; // Remember to keep this private!
			await client.LoginAsync(TokenType.Bot, token);
			await client.StartAsync();

			// Block this task until the program is closed.
			await Task.Delay(-1);
		}

		#region Helpers
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

		private async void SendMessage(ISocketMessageChannel channel, string message)
		{
			await channel.SendMessageAsync(message);
		}
		
		private static string[] GetStringArrayFromResources(string input)
		{
			return input.Trim().Split(',').ToArray();
		}
		#endregion

		private async Task MessageReceived(SocketMessage message)
		{
			if (!_channelWhitelist.ContainsKey(message.Channel.Name))
				return;

			var possibleCommand = message.Content.Contains(" ") ? 
				message.Content.Substring(0, message.Content.IndexOf(' ')).Trim() : message.Content;
			possibleCommand = possibleCommand.ToLower();

			#region Normal Commands
			if (_zeroArgumentCommands.ContainsKey(possibleCommand))
			{
				SendMessage(message.Channel, _zeroArgumentCommands[possibleCommand].Invoke());
				return;
			}
			if (_oneChannelArgumentCommands.ContainsKey(possibleCommand))
			{
				SendMessage(message.Channel, _oneChannelArgumentCommands[possibleCommand].Invoke(message.Channel));
				return;
			}
			if (_oneStringArgumentCommands.ContainsKey(possibleCommand))
			{
				var arg = message.Content.Substring(".fullwidth".Length);
				SendMessage(message.Channel, _oneStringArgumentCommands[possibleCommand].Invoke(arg));
				return;
			}
			if (_slapCommands.ContainsKey(possibleCommand))
			{
				var arg = message.Content.Substring(".slap".Length);
				SendMessage(message.Channel, _slapCommands[possibleCommand].Invoke(message.Author.Mention, arg, message.Channel));
				return;
			}
			#endregion

			#region Google Commands
			if (possibleCommand == ".yt")
			{
				var video = await YoutubeQuery(message.Content.Substring(".yt".Length));
				SendMessage(message.Channel, video);
				return;
			}
			if (possibleCommand == ".g")
			{
				var arg = message.Content.Substring(".g".Length);
				SendMessage(message.Channel, GoogleSearchQuery(arg));
			}
			#endregion


			var content = message.Content;
			if (content.Length > 500)
				return;

			#region Pushing Gaywards
			if ("it gets bigger when I pull" == message.Content)
				await message.Channel.SendMessageAsync(new string('m', _random.Next(3, 15)));
			if (Regex.IsMatch(content, "^(O|o)h shit (I|i)\'?m sorry(!|.)?$"))
				await message.Channel.SendMessageAsync("Sorry for what?");
			else if (Regex.IsMatch(content, "^m{8,}$"))
				await message.Channel.SendMessageAsync("I RIP THE SKIN!");
			#endregion
			else if (content.StartsWith("...") && _random.Next(0, 10) == 1)
				await message.Channel.SendMessageAsync("Ruchasz psa jak sra");
			else if (content.Contains("opti") && _random.Next(0, 10) == 1)
				await
					message.Channel.SendMessageAsync("Uprzejmie proszę o nieużywanie słowa \"opti\" na terenie #politbiuro. Dziękuję");
			else if (Regex.IsMatch(content, "^cz?(o+|0)$") && _random.Next(0, 10) == 1)
				await message.Channel.SendMessageAsync("gówno 1:0");
			else if (content.StartsWith("8=D"))
				await message.Channel.SendMessageAsync(Dick());
			else if (Regex.IsMatch(content, "^((C|c)ze(ś|s)(ć|c)|(S|s)iemk?a|((W|w)itam){1,}|(F|f)eedlysiemka)") &&
					    !message.Author.IsBot)
				await message.Channel.SendMessageAsync(Witam());
			else
			{
				foreach (var s in Resources.wulg.Split(SplitCharacter))
				{
					if (!content.Contains(s)) continue;
					if (_random.Next(0, 10) == 1)
						await message.Channel.SendMessageAsync(WulgResponse());
					else
						return;
				}
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
			return new string('p', _random.Next(1, 5)) +
			       new string('f', _random.Next(2, 8)) +
			       new string('r', _random.Next(3, 10)) + "t";
		}

		private string Tebeg()
		{
			return "T E B E G\nE\nB\nE\nG";
		}

		private string Ocen()
		{
			var mark = _random.Next(0, 10);
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
			return "Be" + string.Concat(Enumerable.Repeat("he", _random.Next(2, 10))) + "mort";
		}

		private string Witam()
		{
			return Extensions.RandomChoice(Resources.witam.Split(SplitCharacter).ToArray());
		}

		private string Czy()
		{
			if (_random.Next(0, 15) == 1)
				return "nie wiem, spytaj " + Kogo();
			return Extensions.RandomChoice(Resources.czy.Split(SplitCharacter).ToArray());
		}

		private string Ile()
		{
			var zeros = _random.Next(1, 4);
			return _random.Next((int) Math.Pow(10, zeros), (int) Math.Pow(10, zeros + 1)).ToString();
		}

		private string WulgResponse()
		{
			return Extensions.RandomChoice(Resources.wulg_response.Split(SplitCharacter).ToArray());
		}

		private string Dick()
		{
			return "8" + new string('=', _random.Next(1, 20)) + "D" +
			       (_random.Next(0, 3) == 1 ? Extensions.RandomChoice(new[] {" ((0)) ", " (.)(.)"}) : string.Empty);
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
			       "behe, kicek, tebeg, ocen, pfrt, kto, kogo, gdzie, kim, fullwidth, piwo, yt, g";
		}

		private string Pedal()
		{
			return Extensions.RandomChoice(Resources.pedal.Split(','));
		}

		private string LiwkoLiwkoSkoczPoPiwko(IChannel msgChannel)
		{
			var usersList = msgChannel.GetUsersAsync(CacheMode.CacheOnly).ToArray().Result;
			var t = usersList.ElementAt(_random.Next(0, usersList.Length)).Where(u => u.Status != UserStatus.Offline).ToArray();
			var user = t.ElementAt(_random.Next(0, t.Length)).Mention;

			return user + ", " + Extensions.RandomChoice(new[] {"skocz", "idź", "przeleć się"}) + " do " +
			       Extensions.RandomChoice(new[]
				       {"osiedlowego", "monopolowego", "lodówki", "barku", "sklepu", "Biedronki", "baru"}) + " po " +
			       Extensions.RandomChoice(new[]
			       {
				       "zimnego Lecha", "Ciechana miodowego", "Złotego Bażanta", "Heinekena",
				       "Carlsberga", "Pilsnera Urquella", "Budweisera", "Rolling Rock", "Koczkodana"
			       });
		}
		

		//todo: spelling
		private string Slap(string slappingUser, string userToSlap, IChannel msgChannel)
		{
			var usersList = msgChannel.GetUsersAsync(CacheMode.CacheOnly).ToArray().Result;
			var t = usersList.ElementAt(_random.Next(0, usersList.Length)).Where(u => u.Status != UserStatus.Offline).ToArray();
			var user = t.Where(u => u.Username.ToLower().Trim().Equals(userToSlap.ToLower().Trim())).FirstOrDefault();
			
			var slappedPerson = "himself";
			if (user != null)
				slappedPerson = user.Mention;
			
			return
				$"{slappingUser} {Extensions.RandomChoice(GetStringArrayFromResources(Resources.slap_verbs))} {slappedPerson} " +
				$"{Extensions.RandomChoice(GetStringArrayFromResources(Resources.slap_areas))} with a " +
				$"{Extensions.RandomChoice(GetStringArrayFromResources(Resources.slap_sizes))} {Extensions.RandomChoice(GetStringArrayFromResources(Resources.slap_tools))}";
		}

		#endregion

		#region Google
		private async Task<string> YoutubeQuery(string query)
		{
			var youtubeService = new YouTubeService(new BaseClientService.Initializer()
			{
				ApiKey = YouTubeDataApiKey,
				ApplicationName = this.GetType().ToString()
			});

			var searchListRequest = youtubeService.Search.List("snippet");
			searchListRequest.Q = query;
			searchListRequest.MaxResults = 1;

			// Call the search.list method to retrieve results matching the specified query term.
			var searchListResponse = await searchListRequest.ExecuteAsync();

			return $"https://www.youtube.com/watch?v={searchListResponse.Items[0].Id.VideoId}";
		}

		private string GoogleSearchQuery(string query)
		{
			var request = _customsearchService.Cse.List(query);
			request.Cx = GoogleSearchEngineId;

			var results = request.Execute().Items;
			return results[0].Link;
		}
		#endregion
	}
}