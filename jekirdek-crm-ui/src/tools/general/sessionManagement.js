import { jwtDecode } from "jwt-decode";

export const removeSession = () => {
    //Oturuma Ait bütün Bilgileri Kaldır Login Özelliğini Engelle
    sessionStorage.removeItem("userName");
    sessionStorage.removeItem("userRole");
    sessionStorage.setItem("isLogin", "false");
    sessionStorage.removeItem("token");

};

export const createSession = (userName, token) => {

    if (!userName  || !token) {
        //Eksik Bilgide Ekrana Uyarı Vercek
        alert("Otorum Bilgileri Eksik");
        return false;
    };
    //Apiden Gelen Token Ayrıştırıldı Role Bilgisi Alındı
    const decodeToken = jwtDecode(token);
    if(!decodeToken.role){
        alert("Tokende Eksik Bilgi Var");
        return false;
    }

    // SessionStore Oturum Kaydı Yapıldı
    sessionStorage.setItem("userName", userName);
    sessionStorage.setItem("userRole", decodeToken.role);
    sessionStorage.setItem("isLogin", "true");
    sessionStorage.setItem("token", token);
    return true;
};
