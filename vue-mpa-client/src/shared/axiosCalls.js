
  const axios = require('axios');
  //Exporte une fonction depuis un fichier
    export function getLocationWithActions(locId, callback, errorCallback){
        console.log('2- axiosCalls.GetLocationWithActions');
        axios
        .get('http://localhost:8080/api/location/'+locId+'/withactions')
        .then(res => {
            if(callback != null){
                console.log('3 OK');
                callback(res);
            }
        }).catch(err => {
            if(errorCallback != null){
                console.log('3 KO');
                errorCallback(res);
         }
        });
    }




  

  export const onAuthenticate = locId => {
    const URL = 'http://localhost:8080/api/location/'+locId+'/withactions';
    return axios(URL, {
      method: 'GET',
      headers: {
        'content-type': 'application/json', // whatever you want
      }
    })
      .then(response => response.data)
      .catch(error => {
        throw error;
      });
  };