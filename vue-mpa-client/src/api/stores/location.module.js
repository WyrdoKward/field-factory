// import the api endpoints
import { getLocationWithActions } from "@/api/clients/location.api"

const state = {
    locationWithActions: {}
}

//Getters du store direct pour le cache quand y'a pas besoin d'avoir la dernière version du server
/*onst getters = {
    getUsers(state) {
        return state.users;
    }
}*/

const actions = {
    async fetchLocationWithActions({ commit }, locId) {
        try {
            console.log('2 - locId :'+locId);
            const response = await getLocationWithActions(locId); 
            console.log('3 - res :'+response);
            commit('SET_LOCATIONWITHACTIONS', response.data);
        } catch (error) {
            console.log(error);
        }    
    }, 
    async foo({commit}){
        console.log('foo');
    }
}

const mutations = {
    SET_LOCATIONWITHACTIONS(state, data) {

        console.log('4 - rmutateur : '+data);
        state.locationWithActions = data;
    }
}

export default {
    namespaced: true,
    state,
    //getters,
    actions,
    mutations
}