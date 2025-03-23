import React, { useEffect } from 'react';
import { removeSession } from '../../tools/general/sessionManagement';


const styles = {
    container: {
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        height: "90vh",
    },
    title: {
        fontSize: "2.5rem",
        color: "#333",
        marginBottom: "20px",
    },
    input: {
        width: "250px",
        padding: "10px",
        margin: "10px 0",
        borderRadius: "5px",
        border: "1px solid #ccc",
        fontSize: "16px",
    },
    button: {
        width: "150px",
        padding: "10px",
        backgroundColor: "#4bab0f",
        color: "white",
        border: "none",
        borderRadius: "5px",
        fontSize: "16px",
        cursor: "pointer",
        marginTop: "10px",
    },
    buttonHover: {
        backgroundColor: "#5fd914",
    },
};

export default function Login() {
    useEffect(() => {
        //Mevcutta Session Varsa Temizle
        removeSession();
    });

    return (
        <div style={styles.container}>
            <h1 style={styles.title}>Giriş Yap</h1>
            <input
                style={styles.input}
                type="text"
                placeholder="Kullanıcı Adı Giriniz"
            />
            <input
                style={styles.input}
                type="password"
                placeholder="Kullanıcı Şifrenizi Giriniz"
            />
            <button
                style={styles.button}
                onMouseOver={(e) =>
                    //Mouse Üstüne Gelirse Rengini Yumuşat
                    e.target.style.backgroundColor = styles.buttonHover.backgroundColor
                }
                onMouseOut={(e) =>
                    //Mouse Ayrılırsa Eski Renkine Döner
                    e.target.style.backgroundColor = styles.button.backgroundColor
                }
                type="submit">
                Giriş Yap
            </button>
        </div>
    );
};