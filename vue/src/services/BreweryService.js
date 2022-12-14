import axios from 'axios';

export default {

getAllBreweries(){
    return axios.get('/brewery')
},

getBreweryById(id){
    return axios.get(`/brewery/${id}`)
},

getBeersByBreweryId(id){
    return axios.get(`brewery/beers/${id}`)
},

getBreweriesByTrailId(id){
    return axios.get(`brewery/trails/${id}`)
}
}