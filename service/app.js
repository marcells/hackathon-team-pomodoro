const express = require('express');
const bodyParser = require('body-parser');
const app = express();

const pomodoros = [];

app.use(bodyParser);

app.set('port', process.env.PORT || 3000);

app.get('/api/pomodoros', (req, res) => {
  res.send(pomodoros);
});

app.put('/api/pomodoro', (req, res) => {
  console.log(req.body);
  if (!req.body.name) {
    res.status(500).send("name not given");
    return;
  }

  pomodoros.push({
    name: req.body.name,
    time: new Date()
  });

  res.send(200);
});

app.delete('/api/pomodoro', (req, res) => {
  if (!req.body.name) {
    res.status(500).send("name not given");
    return;
  }

  const pomodoro = pomodoros.find(x => x.name === req.body.name);
  
  if (pomodoro !== undefined) {
    const indexOfPomodoro = pomodoros.indexOf(pomodoro);
    pomodoros.splice(indexOfPomodoro, 1);
  } else {
    res.status(500).send("pomodoro not found");
    return;
  }

  res.send(200);
});

app.listen(app.get('port'));