// import the api endpoints
import { getFurniturePossessed, getFurnitureAvailable, postNewFurniture, putLvlUpFurniture } from "@/api/clients/furniture.api"

const state = {
    possessedFurnitures: null,
    availableFurnitures: null,
}

//Getters du store direct pour le cache quand y'a pas besoin d'avoir la dernière version du server
const getters = {
    getPossessedFurnitures(state) {
        return state.possessedFurnitures;
    },
    getAvailableFurnitures(state) {
        return state.availableFurnitures;
    }
}

const actions = {
    async fetchPossessedFurnitures({ commit }) {
        try {
            const response = await getFurniturePossessed()
            .then(res => {
                const payload = res;
                commit('SET_POSSESSEDFURNITURES', payload);
            }); 
        } catch (error) {
            console.log(error);
        }    
    }, 
    async fetchAvailableFurnitures({ commit }) {
        try {
            const response = await getFurnitureAvailable()
            .then(res => {
                const payload = res;
                commit('SET_AVAILABLEFURNITURES', payload);
            }); 
        } catch (error) {
            console.log(error);
        }    
    }
}

const mutations = {
    SET_POSSESSEDFURNITURES(state, payload) {
        state.possessedFurnitures = payload.data;
    },
    SET_AVAILABLEFURNITURES(state, payload) {
        state.availableFurnitures = payload.data;
    }
}

export default {
    //namespaced: true,
    state,
    getters,
    actions,
    mutations
}