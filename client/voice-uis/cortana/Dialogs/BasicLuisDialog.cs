using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace SimpleEchoBot.Dialogs
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class BasicLuisDialog : LuisDialog<object>
    {
        // CONSTANTS        
        // Entity
        public const string Entity_Device = "HomeAutomation.Device";
        public const string Entity_Room = "HomeAutomation.Room";
        public const string Entity_Operation = "HomeAutomation.Operation";

        // Intents
        public const string Intent_TurnOn = "HomeAutomation.TurnOn";
        public const string Intent_TurnOff = "HomeAutomation.TurnOff";
        public const string Intent_None = "None";

        public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(
            ConfigurationManager.AppSettings["LuisAppId"],
            ConfigurationManager.AppSettings["LuisAPIKey"],
            domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        {
        }

        [LuisIntent("IsInPomodoro")]
        public async Task IsInPomodoroIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        private async Task ShowLuisResult(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"You have reached {result.Intents[0].Intent}. You said: {result.Query}");           
        }
    }


}