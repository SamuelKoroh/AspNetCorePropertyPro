import React from 'react';
import {BrowserRouter, Switch, Route} from 'react-router-dom';
import { Provider } from 'react-redux';
import store from './store';
import './App.css';
import Landing from './components/home/Landing';
import Dashboard from './components/dashboard/dashboard';
import Login from './components/auth/login';
import Header from './components/common/header';
import Register from './components/auth/register';

const App = () => {
    return (
        <Provider store={store}>
            <BrowserRouter>
            <Header />
                <Switch>
                    <Route exact path='/login' component={Login} />
                    <Route exact path='/register' component={Register} />
                    <Route exact path='/dashboard' component={Dashboard} />
                    <Route exact path='/' component={Landing} />
                </Switch>
            </BrowserRouter>
        </Provider>
    )
}


export default App;