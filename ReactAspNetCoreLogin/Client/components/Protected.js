import React, {Component} from "react";
import api from "../utils/api";
import {Redirect} from "react-router-dom";

class Protected extends Component {

    constructor(props) {
        super(props);
        this.state = {
            msg: 'none',
            isLoggedIn: true
        };
    }

    componentDidMount() {
        api.getSecureMsg()
            .then(msg => {
                if (msg !== 401) {
                    this.setState(
                        {
                            msg: msg,
                            isLoggedIn: true
                        })
                }
                else {
                    this.setState(
                        {isLoggedIn: false}
                    )
                }
            });
    }

    render() {
        return (
            this.state.isLoggedIn ?
                <h1>{this.state.msg}</h1>
                :
                <Redirect to={
                    {
                        pathname: "/logout",
                        state: {from: this.props.location}
                    }
                }/>
            //<h1>Logged in:{this.state.isLoggedIn.toString()} message:{this.state.msg}</h1>
        );
    }
}

export default Protected;