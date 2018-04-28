const axios = require('axios')
const express = require('express')
const moment = require('moment')
const bodyParser = require('body-parser')
const { dialogflow, SimpleResponse } = require('actions-on-google')
const app = dialogflow()
const baseUrl = 'https://team-pomodoro.azurewebsites.net/api';

moment.locale('de');

const userMaps = { };

app.intent('ReadUserName', (conv, {version}) => {
  const name = conv.parameters['given-name'];

  userMaps[conv.user.id] = name;

  conv.ask(new SimpleResponse({
    speech: `Hallo ${name}, was kann ich für dich tun?`,
    text: `Hallo ${name}!`
  }));
});

app.intent('StartPomodoro', async (conv, {version}) => {
  await onlyExecuteIfNamed(conv, async name => {
    await axios.put(`${baseUrl}/pomodoro`, { name });

    conv.ask(new SimpleResponse({
      speech: `Los geht's, ${name}`,
      text: `Los geht's, ${name}`
    }));
  });
});

app.intent('StopPomodoro', async (conv, {version}) => {
  await onlyExecuteIfNamed(conv, async name => {
    const response = await axios.get(`${baseUrl}/pomodoros`);
    const startTime = response.data.find(x => x.name == name);

    if (startTime == null) {
      conv.ask(new SimpleResponse({
        speech: `Du Lügner, ${name}. Du bist nicht im Pomodoro.`,
        text: `Du Lügner, ${name}. Du bist nicht im Pomodoro.`
      }));

      return;
    }

    await axios.delete(`${baseUrl}/pomodoro`, { data: { name }});

    const duration = moment(startTime.time).toNow(true);

    conv.ask(new SimpleResponse({
      speech: `Gut gemacht, ${name}! Du warst ${duration} hochkonzentriert.`,
      text: `Gut gemacht, ${name}! Du warst ${duration} hochkonzentriert.`
    }));
  });
});

app.intent('GetPomodoros', async (conv, {version}) => {
  const response = await axios.get(`${baseUrl}/pomodoros`);

  if (response.data.length == 0) {
    conv.ask(new SimpleResponse({
      speech: "Niemand! Du darfst alle stören!",
      text: "Niemand! Du darfst alle stören!"
    }));

    return;
  }

  const namesWithDuration = response.data.map(x => `${x.name} ${moment(x.time).fromNow()}`).join(', ');
  const output = `Pomodoro begonnen haben ${namesWithDuration}.`

  conv.ask(new SimpleResponse({
    speech: output,
    text: output
  }));
});

async function onlyExecuteIfNamed(conv, action) {
  const name = userMaps[conv.user.id];

  if (!name) {
    conv.ask(new SimpleResponse({
      speech: `Bitte verrate mir zuerst deinen Namen.`,
      text: `Bitte verrate mir zuerst deinen Namen.`
    }));

    return;
  }

  await action(name);
}

express().use(bodyParser.json(), app).listen(3000)
