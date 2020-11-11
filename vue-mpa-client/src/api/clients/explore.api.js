import httpClient from './httpClient';

const END_POINT = 'api/explore/';

//const json = JSON.stringify({IdLocation: this.explore.IdLocation,  IdChoice: choiceId});
// Routes
const ProcessChoiceOnLocation = (IdLocation, IdChoice) => httpClient.put(END_POINT, {IdLocation, IdChoice});
const GetExploreOnLocation = (IdLocation) => httpClient.get(END_POINT+IdLocation);

export {
    ProcessChoiceOnLocation,
    GetExploreOnLocation
}


// examples w/ and w/o params
/*
const getAllUsers = () => httpClient.get(END_POINT);
const createUser = (username, password) => httpClient.post(END_POINT, { username, password });
*/