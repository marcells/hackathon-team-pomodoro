import React from 'react';
import { StyleSheet, View, Text, Button, Animated } from 'react-native';
import moment from 'moment';
import axios from 'axios';
import server from '../serverConfig';

const formatTime = (duration = moment.duration()) => moment.utc(duration).format('mm:ss');

export default class Timer extends React.Component {
  constructor () {
    super();

    this.state = {
      buttonEnabeled: true,
      buttonText: 'Start',
      startTime: null,
      currentDurationText: formatTime(),
      backgroundColor: new Animated.Value(0)
    };
  }

  async componentDidMount () {
    const response = await axios.get(`${server}/api/pomodoros`);

    if (this._isUnmounted) {
      return;
    }

    const myPomodoro = response.data.find(x => x.name === this.props.name);

    if (myPomodoro !== undefined) {
      this.setState({
        buttonEnabeled: true,
        buttonText: 'Stop',
        startTime: myPomodoro.time,
        currentDurationText: formatTime(moment().diff(myPomodoro.time)),
        backgroundColor: this.state.backgroundColor
      });

      Animated.timing(this.state.backgroundColor, {
        delay: 0,
        duration: 150,
        toValue: 1
      }).start();

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
        currentDurationText: formatTime(moment().diff(this.state.startTime)),
        backgroundColor: this.state.backgroundColor
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
        currentDurationText: formatTime(),
        backgroundColor: this.state.backgroundColor
      });

      Animated.timing(this.state.backgroundColor, {
        delay: 0,
        duration: 150,
        toValue: 0
      }).start();

      await axios.delete(`${server}/api/pomodoro`, {
        data:  {
          name: this.props.name
        }});

      this.setState({
        buttonEnabeled: true,
        buttonText: this.state.buttonText,
        startTime: this.state.startTime,
        currentDurationText: this.state.currentDurationText,
        backgroundColor: this.state.backgroundColor
      });

      return;
    }

    this._enableTimer();

    this.setState({
      buttonEnabeled: false,
      buttonText: 'Stop',
      startTime: moment(),
      currentDurationText: formatTime(),
      backgroundColor: this.state.backgroundColor
    });

    Animated.timing(this.state.backgroundColor, {
      delay: 0,
      duration: 150,
      toValue: 1
    }).start();

    await axios.put(`${server}/api/pomodoro`, {
      name: this.props.name
    });

    this.setState({
      buttonEnabeled: true,
      buttonText: this.state.buttonText,
      startTime: this.state.startTime,
      currentDurationText: this.state.currentDurationText,
      backgroundColor: this.state.backgroundColor
    });
  }

  render() {
    const backgroundColor = this.state.backgroundColor.interpolate({
        inputRange: [0, 1],
        outputRange: ['gray', 'red']
    });

    return (
      <Animated.View style={[styles.container, { backgroundColor }]}>
        <Text style={styles.timer}>{this.state.currentDurationText}</Text>
        <Button title={this.state.buttonText} onPress={() => this._startOrStopTimer()} disabled={!this.state.buttonEnabeled} />
      </Animated.View>
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
