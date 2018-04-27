import React from 'react';
import { StyleSheet, Text, View, FlatList, Button, TextInput } from 'react-native';
import { StackNavigator } from 'react-navigation';
import LoginScreen from './screens/LoginScreen';
import HomeScreen from './screens/HomeScreen';

const RootStack = StackNavigator({
    Login: { screen: LoginScreen },
    Home: { screen: HomeScreen }
  },
  {
    initialRouteName: 'Login',
  }
);

export default class App extends React.Component {
  render() {
    return (
      <RootStack />
    );
  }
}
