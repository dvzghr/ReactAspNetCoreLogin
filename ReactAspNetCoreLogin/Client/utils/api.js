import axios from "axios";

const uri = '/api';

export default {
    async getSomething(id) {
        const response = await axios.get(`${uri}/${id}`);
        return response.data;
    }
};