import React, {Component, Fragment} from "react";
import api from "../utils/api";

class Protected extends Component {

    constructor(props) {
        super(props);
        this.state = {
            msg: 'no msg',
        };

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick() {
        api.getSecureMsg()
            .then(msg => {
                this.setState(
                    {msg: msg.data})
            })
            .catch(err => console.log(err));
    }

    render() {
        return (
            <Fragment>
                <h1>{this.state.msg}</h1>
                <button onClick={this.handleClick}>Get secure msg</button>
            </Fragment>
        )
    }
}

export default Protected;