import React, {Component} from "react";
import api from "../utils/api";

class Protected extends Component {

    constructor(props) {
        super(props);
        this.state = {
            msg: 'no msg',
        };
    }

    componentDidMount() {
        api.getSecureMsg()
            .then(msg => {
                this.setState(
                    {msg : msg.data})
            })
            .catch(err => console.log(err));
    }

    render() {
        return (
            <h1>{this.state.msg}</h1>
        );
    }
}

export default Protected;