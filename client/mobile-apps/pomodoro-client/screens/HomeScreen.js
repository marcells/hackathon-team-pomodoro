import React from 'react';
import { StyleSheet, View } from 'react-native';
import Timer from '../components/Timer';
import Pomodoros from '../components/Pomodoros';

export default class HomeScreen extends React.Component {
  static navigationOptions = {
    title: 'Pomodoros',
  };

  render() {
    const { params } = this.props.navigation.state;

    return (
      <View style={styles.container}>
        <View style={{ flex: 4 }}><Pomodoros /></View>
        <View style={{ flex: 1 }}><Timer name={params.name} /></View>
      </View>);
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff'
  }
});
