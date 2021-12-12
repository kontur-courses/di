import React, { Component } from 'react';
import { Route } from 'react-router';

import './custom.css'
import {TagCloud} from "./components/TagCloud";

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <Route path='/' component={TagCloud} />
    );
  }
}
