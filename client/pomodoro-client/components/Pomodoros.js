import React from 'react';
import { StyleSheet, View, FlatList, Text } from 'react-native';
import moment from 'moment';
import axios from 'axios';
import server from '../serverConfig';

const Entry = props => (
  <View style={styles.item}>
    <Text style={styles.textEntry}>{props.name}</Text>
    <Text style={styles.textEntry}>{moment(props.time).fromNow()}</Text>
  </View>
);

export default class Pomodoros extends React.Component {
  constructor () {
    super();

    this.state = { pomodoros: [] };
  }

  async _loadPomodoros () {
    const response = await axios.get(`${server}/api/pomodoros`);
    const pomodoros = response.data.map(x => ({ key: x.name, name: x.name, time: x.time }));

    this.setState({ pomodoros });
  }

  async componentDidMount() {
    await this._loadPomodoros();

    this._timer = setInterval(async () => await this._loadPomodoros(), 1000);
  }

  componentWillUnmount () {
    if (this._timer) {
      clearInterval(this._timer);
      this._timer = null;
    }
  }

  render() {
    return (
      <View style={styles.container}>
        <FlatList
          data={this.state.pomodoros}
          renderItem={({ item }) => <Entry name={item.name} time={item.time} />}
          />
      </View>
    );
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1
  },
  item: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    padding: 10,
    height: 44
  },
  textEntry: {
    fontSize: 18
  }
});
