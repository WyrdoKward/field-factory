import httpClient from './httpClient';

const END_POINT = '/location/';

// Routes
const getLocationWithActions = (locId) => httpClient.get(END_POINT+locId+'/withactions');

export {
    getLocationWithActions
}


// examples w/ and w/o params
/*const getAllUsers = () => httpClient.get(END_POINT);
const createUser = (username, password) => httpClient.post(END_POINT, { username, password });
*/