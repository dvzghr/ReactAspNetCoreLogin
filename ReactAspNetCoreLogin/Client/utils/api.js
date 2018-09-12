import axios from "axios";
import {authHeader} from "./auth-header";

const uri = '/api';

// axios.interceptors.request.use(response => response,
//     error => {
//         const orgReq = error.config;
//         if (error.response.status === 401 && !orgReq._retry) {
//             return {data: 'relogin'};
//         }
//         return Promise.reject(error);
//     }
// );

export default {
    async login(user) {
        const response = await axios.post(`${uri}/token`, user);
        return response.data;
    },
    async getSecureMsg() {
        let response;
        try {
            response = await axios.get(`${uri}/secure`, {headers: authHeader()});
            console.log(response);
            return response.data;
        }
        catch (error) {
            console.log(error);
            return error.response.status;
        }
    }
};