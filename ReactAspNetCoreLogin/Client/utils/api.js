import axios from "axios";
import {authHeader} from "./auth-header";

const uri = '/api';

axios.interceptors.response.use(response => response,
    error => {
        if (error.response.status === 401) {
            localStorage.clear();
            //location.reload(true);
            location.href = '/logout'
        }

        console.log(error)
        const msg = (error && error.message);
        return Promise.reject(msg);
    }
);

async function login(user) {
    const response = await axios.post(`${uri}/token`, user);
    return response.data;
}

async function getSecureMsg() {
    const response = await axios.get(`${uri}/secure`, {headers: authHeader()});
    return response;
}

export default {
    login,
    getSecureMsg
}