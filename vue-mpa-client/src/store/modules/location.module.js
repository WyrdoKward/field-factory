// import the api endpoints
import { getLocationWithActions } from "@/api/clients/location.api"

const state = {
    locationWithActions: null,
    locations : [] // se rempli au fur et à mesure avec les locations ? car le store va recevoir plsrs loc au fur et à mesure des axios async sur la page en cliquant d'une location à une autre
}

//Getters du store direct pour le cache quand y'a pas besoin d'avoir la dernière version du server
const getters = {
    getLastSelectedLocation(state) {
        return state.locationWithActions;
    }
}

const actions = {
    async fetchLocationWithActions({ commit }, locId) {
        try {
            console.log('2 - locId :'+locId);
            const response = await getLocationWithActions(locId)
            .then(res => {
                console.log('3 - res :')
                console.log(res);
                commit('SET_LOCATIONWITHACTIONS', res.data);
            }); 
        } catch (error) {
            console.log(error);
        }    
    }, 
    async fooLocation({commit}){
        console.log('foo location');
    }
}

const mutations = {
    SET_LOCATIONWITHACTIONS(state, data) {

        console.log('4 - rmutateur : ');
        console.log(data);
        state.locationWithActions = data;
    }
}

export default {
    //namespaced: true,
    state,
    getters,
    actions,
    mutations
}