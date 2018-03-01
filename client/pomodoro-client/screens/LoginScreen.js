import React from 'react';
import { StyleSheet, Image, View, Button, Text, TextInput } from 'react-native';

export default class LoginScreen extends React.Component {
  static navigationOptions = {
    title: 'Login'
  };

  constructor () {
    super();

    this.state = { text: '' };
  }

  render() {
    const { navigate } = this.props.navigation;

    return (
      <View style={styles.container}>
        <View style={styles.imagePanel}>
          <Image style={styles.image} source={require('../images/pomodoro.png')} />
        </View>
        <View style={styles.loginPanel}>
          <TextInput style={styles.text}
                     placeholder="Enter your name"
                     placeholderTextColor="gray"
                     textAlign="center"
                     onChangeText={(text) => this.setState({text})}
                     value={this.state.text}
          />
          <Button title="Login" onPress={() => navigate('Home', { name: this.state.text })} disabled={this.state.text.length == 0} />
        </View>

        <Text>made by React with ♥</Text>
      </View>);
  }
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center'
  },
  imagePanel: {
    flex: 1,
    justifyContent: 'center'
  },
  image: {
    flex: 1,
    resizeMode: 'contain'
  },
  text: {
    width: 200,
    fontSize: 20
  },
  loginPanel: {
    flex: 1,
    justifyContent: 'center'
  }
});
