const axios = require('axios');
const express = require("express");
const alexa = require("alexa-app");
const moment = require('moment')
const express_app = express();

const app = new alexa.app("teamPomodoro");

const baseUrl = 'https://team-pomodoro.azurewebsites.net/api';

moment.locale('de');

const userMaps = { };

app.launch(function(request, response) {
  response.say("Willkommen bei Team-Pomodoro!").shouldEndSession(false);
});

app.intent(
  "ReadUserName",
  function(request, response) {
    var name = request.slot("name");

    userMaps[request.userId] = name;

    response.say(`Hallo ${name}, was kann ich für dich tun?`).shouldEndSession(false);
  }
);

app.intent(
  "GetPomodoros",
  async function(request, response) {
    const pomodorosResponse = await axios.get(`${baseUrl}/pomodoros`);

    if (pomodorosResponse.data.length == 0) {
      response.say("Niemand! Du darfst alle stören!").shouldEndSession(false);;
      return;
    }

    const namesWithDuration = pomodorosResponse.data.map(x => `${x.name} ${moment(x.time).fromNow()}`).join(', ');
    const output = `Pomodoro begonnen haben ${namesWithDuration}.`

    response.say(output).shouldEndSession(false);
  }
);

app.intent(
  "StartPomodoro",
  async function(request, response) {
    await onlyExecuteIfNamed(request, response, async name => {
      await axios.put(`${baseUrl}/pomodoro`, { name });

      response.say(`Los geht's, ${name}`).shouldEndSession(false);
    });
  }
);

app.intent(
  "StopPomodoro",
  async function(request, response) {
    await onlyExecuteIfNamed(request, response, async name => {
      const pomodorosResponse = await axios.get(`${baseUrl}/pomodoros`);
      const startTime = pomodorosResponse.data.find(x => x.name == name);

      if (startTime == null) {
        response.say(`Du Lügner, ${name}. Du bist nicht im Pomodoro.`).shouldEndSession(false);
        return;
      }

      await axios.delete(`${baseUrl}/pomodoro`, { data: { name }});

      const duration = moment(startTime.time).toNow(true);

      response.say(`Gut gemacht, ${name}! Du warst ${duration} hochkonzentriert.`).shouldEndSession(false);
    });
  }
);

app.intent(
  "AMAZON.HelpIntent",
  async function(request, response) {
    response.say('Verrate mir zuerst deinen Namen zum Beispiel mit ich heiße Tim. Dann sage Los um einen Pomodoro zu starten, sage Fertig um ihn zu beenden. Frage mich jederzeit wer im Pomodoro ist und ich werde es dir verraten.').shouldEndSession(false);
  }
);

async function onlyExecuteIfNamed(request, response, action) {
  const name = userMaps[request.userId];

  if (!name) {
    response.say(`Bitte verrate mir zuerst deinen Namen.`).shouldEndSession(false);;

    return;
  }

  await action(name);
}

app.express({ expressApp: express_app });

const PORT = process.env.PORT || 3000;

express_app.listen(PORT, () => console.log("Listening on port " + PORT + "."));
