import React, {Component} from "react";
import {BrowserRouter as Router, Link, Route, Switch} from "react-router-dom"
import Login from "./Login";
import PrivateRoute from "./PrivateRoute";
import Protected from "./Protected";

class App extends Component {
    constructor(props) {
        super(props);
        this.state = {
            user: {
                token: '',
                isAuth: false
            }
        };
    }

    render() {
        return (
            <Router>
                <Switch>
                    <PrivateRoute exact path="/" component={Protected}/>
                    <Route path='/logout' component={Logout}/>
                    <Route path='/login' component={Login}/>
                    <Route component={NotFound404}/>
                </Switch>
            </Router>
        )
    }
}

const Logout = () => (
    <div>
        <h1>You have been logged out!</h1>
        <Link to='/login'>Login</Link>
    </div>
);

const NotFound404 = () => (<h1>Not Found 404</h1>);

export default App;

