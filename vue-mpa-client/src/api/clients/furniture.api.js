import httpClient from './httpClient';

const END_POINT = 'api/furniture/';

// Routes
const getFurniturePossessed = () => httpClient.get(END_POINT+'possessed');
const getFurnitureAvailable = () => httpClient.get(END_POINT+'available');

export {
    getFurniturePossessed,
    getFurnitureAvailable
}


// examples w/ and w/o params
/*const getAllUsers = () => httpClient.get(END_POINT);
const createUser = (username, password) => httpClient.post(END_POINT, { username, password });
*/