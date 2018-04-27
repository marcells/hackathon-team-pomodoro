namespace PomodoroBridgeClient
{
    /// <summary>
    /// file:///C:\Users\r.kornelius\Desktop\PomodoroBridgeClient\PomodoroBridgeClient\bin\Debug/bridge/index.html
    /// https://deck.net/webservice
    /// https://github.com/bridgedotnet/Bridge/wiki
    /// </summary>
    public class App
    {
        public static void Main()
        {
            var client = new PomodoroClient();
            client.Render();
        }
    }
}