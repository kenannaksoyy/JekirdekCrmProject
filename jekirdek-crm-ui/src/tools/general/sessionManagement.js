const removeSession = () => {

    sessionStorage.removeItem("userName");
    sessionStorage.removeItem("userRole");
    sessionStorage.setItem("isLogin", "false");
    sessionStorage.removeItem("token");

};

const createSession = (userName, userRole, token) => {

    if (!userName || !userRole || !token) {
        //Ekrana Uyarı Vercek
        alert("Otorum Bilgileri Eksik");
        return;
    }
    // SessionStore Oturum Kaydı Yapıldı
    sessionStorage.setItem("userName", userName);
    sessionStorage.setItem("userRole", userRole);
    sessionStorage.setItem("isLogin", "true");
    sessionStorage.setItem("token", token);
    console.log("Oturum Açıldı");

};

module.exports = {
    removeSession,
    createSession
}