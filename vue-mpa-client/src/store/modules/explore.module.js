// import the api endpoints
import { PostExploreOnLocation, GetExploreOnLocation, PutChoiceOnLocation } from "@/api/clients/explore.api"

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
    },
    async addNewExploration({commit}, payload){
        try {
            const response = await PostExploreOnLocation(payload.IdLocation, payload.IdFollower)
            .then(res => {
                const payload = res;
                console.log('addNewExploration')
                console.log(res)
                commit('SET_SELECTEDEXPLORATION', payload.data);
            }); 
        } catch (error) {
            console.log(error);
        } 
    },
    async processChoiceOnLocation({commit}, payload){
        console.log(payload.IdLocation+' - '+payload.IdChoice)
        try {
            const response = await PutChoiceOnLocation(payload.IdLocation, payload.IdChoice)
            .then(res => {
                const payload = res;
                console.log('processChoiceOnLocation')
                console.log(res)
                commit('SET_SELECTEDEXPLORATION', payload.data);
            }); 
        } catch (error) {
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