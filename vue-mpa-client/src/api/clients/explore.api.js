import httpClient from './httpClient';

const END_POINT = 'api/explore/';

//const json = JSON.stringify({IdLocation: this.explore.IdLocation,  IdChoice: choiceId});
// Routes
const PutChoiceOnLocation = (IdLocation, IdChoice) => httpClient.put(END_POINT, {IdLocation, IdChoice});
const GetExploreOnLocation = (IdLocation) => httpClient.get(END_POINT+IdLocation);
//const PostExploreOnLocation = (IdLocation, IdFollower) => httpClient.post(END_POINT, JSON.stringify({IdFollower: IdFollower,  IdLocation: IdLocation}));
const PostExploreOnLocation = (IdLocation, IdFollower) => httpClient.post(END_POINT, {IdFollower, IdLocation});
//seul IdLocation est envoyé à la requete ??
export {
    PutChoiceOnLocation,
    GetExploreOnLocation,
    PostExploreOnLocation
}


// examples w/ and w/o params
/*
const getAllUsers = () => httpClient.get(END_POINT);
const createUser = (username, password) => httpClient.post(END_POINT, { username, password });
*/