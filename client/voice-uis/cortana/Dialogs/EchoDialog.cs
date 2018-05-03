using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace SimpleEchoBot.Dialogs
{
  [Serializable]
  public class EchoDialog : IDialog<object>
  {
    private string _username;

    public async Task StartAsync(IDialogContext context)
    {
      context.Wait(MessageReceivedAsync);
    }

    public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
    {
      var api = new Api(new NetWebRequester());

      var message = await argument;
      var messageLower = message.Text.ToLower();

      if (_username == null)
      {
        _username = messageLower;
        await context.PostAsync($"Hello {_username}, what can i do for you?");
        context.Wait(MessageReceivedAsync);
        return;
      }
      
      switch (messageLower)
      {
        case "ask":
          PromptDialog.Text(
            context,
            ResumeAfter,
            "Who the fuck are you?",
            "Didn't get that!");
          break;
        case "start":
        case "start pomodoro":
          if (_username == null)
            goto case "ask";

          api.Start(_username);
          await context.PostAsync($"go go go!");
          break;
        case "stop":
        case "stop pomodoro":
          if (_username == null)
            goto case "ask";

          var startTime = api.GetAll()
            .Where(x => x.Name == _username)
            .Select(x => x.Time)
            .Single();

          var duration = DateTime.Now - startTime;
          var humanDuration = Api.Humanize(duration);

          api.Stop(_username);          
          await context.PostAsync($"Nice work! You took " + humanDuration);
          break;
        case "who?":
        case "who is in pomodoro?":
          var pomodoros = api.GetAll();

          var first = true;
          var nameString = new StringBuilder();
          var names = pomodoros
            .Take(5)
            .Select(x => x.Name);
          foreach (var name in names)
          {
            if (first)
              first = false;
            else
              nameString.Append(", ");

            nameString.Append(name);
          }
          
          await context.PostAsync(nameString.ToString());
          break;
        case "is xx in pomodoro":
          break;

        default:
          await context.PostAsync($"Didn't understand");
          context.Wait(MessageReceivedAsync);
          break;
      }
    }

    private async Task ResumeAfter(IDialogContext context, IAwaitable<string> result)
    {
      _username = await result;
      await context.PostAsync($"Hello {_username}, What can i do for you?");
      context.Wait(MessageReceivedAsync);
    }
  }
}