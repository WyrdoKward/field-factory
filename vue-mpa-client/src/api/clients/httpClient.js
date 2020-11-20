//https://haxzie.com/architecting-http-clients-vue-js-network-layer 
import axios from 'axios';

const httpClient = axios.create({
    baseUrl: process.env.VUE_APP_BASE_URL,
    timeout : 3000,
    headers: {
        "Content-Type": "application/json",
    }
});

export default httpClient;