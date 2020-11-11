// import the api endpoints
import { ProcessChoiceOnLocation, GetExploreOnLocation } from "@/api/clients/explore.api"

const state = {
    selectedExploration: null
}

//Getters du store direct pour le cache quand y'a pas besoin d'avoir la dernière version du server
const getters = {
    getLastSelectedExploration(state) {
        return state.selectedExploration;
    }
}

const actions = {
    async fetchSelectedExploration({ commit }, IdLocation) {
        try {
            const response = await GetExploreOnLocation(IdLocation)
            .then(res => {
                const payload = res;
                console.log('fetchSelectedExploration')
                console.log(res)
                commit('SET_SELECTEDEXPLORATION', payload.data);
            }); 
        } catch (error) {
            console.log('ERROR');
            commit('SET_SELECTEDEXPLORATION', null);
            console.log(error);
        }    
    }
}

const mutations = {
    SET_SELECTEDEXPLORATION(state, payload) {
        state.selectedExploration = payload;
    }
}

export default {
    //namespaced: true,
    state,
    getters,
    actions,
    mutations
}