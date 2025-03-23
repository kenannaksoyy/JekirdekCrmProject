import axios from 'axios';

const apiUrl = "http://localhost:8080/api";

//Base Url Sabitleyerek ve Oturumdan Token Bilgisini Alarak axios Modifiye Edildi
export const axiosWithAuth = axios.create({
    baseURL : apiUrl,
    headers : {
        "Content-Type" : "application/json",
        "Authorization" : `Bearer ${sessionStorage.getItem("token")}`
    }
});

