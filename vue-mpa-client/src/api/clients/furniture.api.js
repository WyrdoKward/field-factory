import httpClient from './httpClient';

const END_POINT = 'api/furniture/';

// Routes
const getFurniturePossessed = () => httpClient.get(END_POINT+'possessed');
const getFurnitureAvailable = () => httpClient.get(END_POINT+'available');
/*const postNewFurniture = (idFurniture) => httpClient.post(END_POINT+idFurniture);
const putLvlUpFurniture = (idFurniture) => httpClient.put(END_POINT+idFurniture);*/
//passer par provide.api.js

export {
    getFurniturePossessed,
    getFurnitureAvailable,
   /* postNewFurniture,
    putLvlUpFurniture*/
}


// examples w/ and w/o params
/*const getAllUsers = () => httpClient.get(END_POINT);
const createUser = (username, password) => httpClient.post(END_POINT, { username, password });
*/