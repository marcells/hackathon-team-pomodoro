using System;
using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Web.Http.Description;
using System.Net.Http;
using SimpleEchoBot.Dialogs;

namespace Microsoft.Bot.Sample.SimpleEchoBot
{
  [BotAuthentication]
  public class MessagesController : ApiController
  {
    /// <summary>
    /// POST: api/Messages
    /// receive a message from a user and send replies
    /// </summary>
    /// <param name="activity"></param>
    [ResponseType(typeof(void))]
    public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
    {
      // check if activity is of type message
      if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
      {
        await Conversation.SendAsync(activity, () => new BasicLuisDialog());
      }
      else
      {
        HandleSystemMessage(activity);
      }

      return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
    }

    private bool _introductions;

    private Activity HandleSystemMessage(Activity message)
    {
      if (message.Type == ActivityTypes.DeleteUserData)
      {
        // Implement user deletion here
        // If we handle user deletion, return a real message
      }
      else if (message.Type == ActivityTypes.ConversationUpdate)
      {
        // Handle conversation state changes, like members being added and removed
        // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
        // Not available in all channels        
        if (_introductions)
          return null;

        _introductions = true;
        var connector = new ConnectorClient(new Uri(message.ServiceUrl));
        var reply = message.CreateReply("Welcome to team pomodoro. Who are you?");
        connector.Conversations.ReplyToActivityAsync(reply);
      }
      else if (message.Type == ActivityTypes.ContactRelationUpdate)
      {
        // Handle add/remove from contact lists
        // Activity.From + Activity.Action represent what happened
      }
      else if (message.Type == ActivityTypes.Typing)
      {
        // Handle knowing tha the user is typing
      }
      else if (message.Type == ActivityTypes.Ping)
      {
      }
      else if (message.Type == ActivityTypes.EndOfConversation)
      {

      }

      return null;
    }
  }
}