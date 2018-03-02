const express = require('express');
const bodyParser = require('body-parser');
const app = express();
const cors = require('cors')

const pomodoros = [];

app.use(cors());
app.use(bodyParser.json());

app.set('port', process.env.PORT || 3000);

app.get('/api/pomodoros', (req, res) => {
  res.send(pomodoros);
});

app.put('/api/pomodoro', (req, res) => {
  if (!validateBody(req, res)) return;

  tryRemovePomodoro(req.body.name);

  pomodoros.push({
    name: req.body.name,
    time: new Date()
  });

  res.sendStatus(200);
});

app.delete('/api/pomodoro', (req, res) => {
  if (!validateBody(req, res)) return;

  const wasRemoved = tryRemovePomodoro(req.body.name);
  
  if (wasRemoved) {
    res.sendStatus(200);
  } else {
    res.status(500).send("pomodoro not found");
  }
});

app.listen(app.get('port'));

// **************** Helpers **************** //

function validateBody (req, res) {
  const ip = req.connection.remoteAddress;

  console.log(ip);
  // if (ip === '::ffff:10.110.3.7') {
  //   res.status(500).send("blocked!");
  //   return false;
  // }

  if (!req.body.name) {
    res.status(500).send("name not given");
    return false;
  }
  
  if (req.body.name.length > 255) {
    
    res.status(500).send("fuck ya! too long!");
    return false;
  }

  return true;
}

function tryRemovePomodoro(name) {
  const pomodoro = pomodoros.find(x => x.name === name);
  
  if (pomodoro !== undefined) {
    const indexOfPomodoro = pomodoros.indexOf(pomodoro);
    pomodoros.splice(indexOfPomodoro, 1);

    return true;
  }

  return false;
}