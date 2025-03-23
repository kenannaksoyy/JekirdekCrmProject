import React, { useEffect, useState } from 'react';
import { createSession, removeSession } from '../../tools/general/sessionManagement';
import { userLogin } from '../../services/authServices';
import { useNavigate } from 'react-router-dom';


const styles = {
    container: {
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        height: "90vh",
    },
    loginBox: {
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
        backgroundColor: "#fff",
        padding: "40px",
        borderRadius: "10px",
        boxShadow: "0 4px 10px gray",
        width: "300px",
        textAlign: "center",
    },
    title: {
        fontSize: "2rem",
        color: "#333",
        marginBottom: "20px",
    },
    input: {
        width: "100%",
        padding: "10px",
        margin: "10px 0",
        borderRadius: "5px",
        border: "1px solid #ccc",
        fontSize: "16px",
    },
    button: {
        width: "100%",
        padding: "10px",
        backgroundColor: "#4bab0f",
        color: "white",
        border: "none",
        borderRadius: "5px",
        fontSize: "16px",
        cursor: "pointer",
        marginTop: "10px",
        transition: "background-color 0.3s ease",
    },
    buttonHover: {
        backgroundColor: "#5fd914",
    },
};

export default function Login() {
    //Ekran İlk Açılma
    useEffect(() => {
        // Mevcutta Session Varsa Temizle
        removeSession();
    }, []);

    const [loginInfos, setLoginInfos] = useState({
        userName: "",
        password: ""
    });
    //Ekran Yönledirme Hookumuz
    const navigate  =  useNavigate();

    const handleInputChange = (e) => {
        //İd ile State Porpsu Eşlescek
        const { id, value } = e.target;

        //Spread ve Prevstate State diğer Üyeleri Etkilenmeyecek
        setLoginInfos(prevState => ({
            ...prevState,
            [id]: value
        }));
    };

    const handleLogin = async () => {
        if (!loginInfos.userName || !loginInfos.password) {
        };
        const res = await userLogin(loginInfos.userName, loginInfos.password);
        //Servisden Gelen Statu İle Hata Yönetimi
        console.log(res.status);
        if(res.status === 200){
            const { token, userName} = res.data.customerLogin;
            let sessionOk = createSession(userName, token);
            if(sessionOk){
                //Herşey Okey Müşteri Yönetim Paneline Yönlendir
                alert("Kullanıcı Girişi Başarılı");
                navigate("/CustomerManagement");
            }
        }
        else{
            //Yönetilmiş Hatalarmız Statuler
            if(res.status === 400 || res.status === 404){
                alert(res.response.data.errorMessage);
            }
            //Yönetilmemiş İse Detay Verme
            else{
                alert("Beklenmedik Bir Hata Oluştu");
            }
        }
    }
    return (
        <div style={styles.container}>
            <div style={styles.loginBox}>
                <h1 style={styles.title}>Giriş Yap</h1>
                <input
                    style={styles.input}
                    type="text"
                    id="userName"
                    placeholder="Kullanıcı Adı Giriniz"
                    onChange={handleInputChange}
                />
                <input
                    style={styles.input}
                    type="password"
                    id="password"
                    placeholder="Şifre Giriniz"
                    onChange={handleInputChange}
                />
                <button
                    style={styles.button}
                    onClick={handleLogin}
                    onMouseOver={(e) =>
                        (e.target.style.backgroundColor = styles.buttonHover.backgroundColor)
                    }
                    onMouseOut={(e) =>
                        (e.target.style.backgroundColor = styles.button.backgroundColor)
                    }
                    type="submit"
                >
                    Giriş Yap
                </button>
            </div>
        </div>
    );
}