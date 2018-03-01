import React from 'react';
import { StyleSheet, View, Text, Button } from 'react-native';
import moment from 'moment';
import axios from 'axios';

const formatTime = (duration = moment.duration()) => moment.utc(duration).format('mm:ss');

export default class Timer extends React.Component {
  constructor () {
    super();

    this.state = {
      buttonEnabeled: true,
      buttonText: 'Start',
      startTime: null,
      currentDurationText: formatTime()
    };
  }

  async componentDidMount () {
    const response = await axios.get('http://vc024.vescon.com:3000/api/pomodoros');

    if (this._isUnmounted) {
      return;
    }

    const myPomodoro = response.data.find(x => x.name === this.props.name);

    if (myPomodoro !== undefined) {
      this.setState({
        buttonEnabeled: true,
        buttonText: 'Stop',
        startTime: myPomodoro.time,
        currentDurationText: formatTime(moment().diff(myPomodoro.time))
      });

      this._enableTimer();
    }
  }

  componentWillUnmount () {
    this._isUnmounted = true;

    if (this._timer) {
      clearInterval(this._timer);
      this._timer = null;
    }
  }

  _enableTimer () {
    this._timer = setInterval(() => {
      this.setState({
        buttonEnabeled: this.state.buttonText,
        buttonText: this.state.buttonText,
        startTime: this.state.startTime,
        currentDurationText: formatTime(moment().diff(this.state.startTime))
      });
    }, 1000);
  }

  async _startOrStopTimer () {
    if (this._timer) {
      clearInterval(this._timer);
      this._timer = null;

      this.setState({
        buttonEnabeled: false,
        buttonText: 'Start',
        startTime: null,
        currentDurationText: formatTime()
      });

      await axios.delete('http://vc024.vescon.com:3000/api/pomodoro', {
        data:  {
          name: this.props.name
        }});

      this.setState({
        buttonEnabeled: true,
        buttonText: this.state.buttonText,
        startTime: this.state.startTime,
        currentDurationText: this.state.currentDurationText
      });

      return;
    }

    this._enableTimer();

    this.setState({
      buttonEnabeled: false,
      buttonText: 'Stop',
      startTime: moment(),
      currentDurationText: formatTime()
    });

    await axios.put('http://vc024.vescon.com:3000/api/pomodoro', {
      name: this.props.name
    });

    this.setState({
      buttonEnabeled: true,
      buttonText: this.state.buttonText,
      startTime: this.state.startTime,
      currentDurationText: this.state.currentDurationText
    });
  }

  render() {
    return (
      <View style={styles.container}>
        <Text style={styles.timer}>{this.state.currentDurationText}</Text>
        <Button title={this.state.buttonText} onPress={() => this._startOrStopTimer()} disabled={!this.state.buttonEnabeled} />
      </View>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center'
  },
  timer: {
    fontSize: 40
  }
});
