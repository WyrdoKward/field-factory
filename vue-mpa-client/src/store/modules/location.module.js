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
    async fetchLocationWithActions({ commit }, locId) { //récup l'explore via un autre endpoint pour qu'il soit dans le bon store ? Là on a Explore dans le store de location...
        try {
            const response = await getLocationWithActions(locId)
            .then(res => {
                const payload = res;
                payload.data.IdLoc = locId;
                commit('SET_LOCATIONWITHACTIONS', payload);
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
    SET_LOCATIONWITHACTIONS(state, payload) {
        state.locationWithActions = payload.data;
        state.locationWithActions.Id = payload.data.IdLoc
    }
}

export default {
    //namespaced: true,
    state,
    getters,
    actions,
    mutations
}