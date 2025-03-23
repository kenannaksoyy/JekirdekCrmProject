import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';
import { removeSession } from '../../tools/general/sessionManagement';

const styles = {
    container: {
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
        height: "100vh",
        backgroundColor: "#f8f9fa",
        color: "#343a40",
        textAlign: "center",
    },
    title: {
        fontSize: "3rem",
        fontWeight: "bold",
        marginBottom: "1rem",
    },
    message: {
        fontSize: "1.2rem",
        marginBottom: "2rem",
    },
    link: {
        padding: "0.5rem 1rem",
        backgroundColor: "#007bff",
        color: "#fff",
        textDecoration: "none",
        borderRadius: "5px",
        fontSize: "1rem",
        transition: "background-color 0.3s ease",
    },
    linkHover: {
        backgroundColor: "#0056b3",
    },
};
export default function NotFound() {

    useEffect(() => {
        removeSession();
    });

    return (
        <div style={styles.container}>
            <h1 style={styles.title}>404 - Sayfa Bulunamadı</h1>
            <p style={styles.message}>Üzgünüz, aradığınız sayfa mevcut değil.</p>
            <Link
                to="/"
                style={styles.link}
                onMouseEnter={(e) => e.target.style.backgroundColor = styles.linkHover.backgroundColor}
                onMouseLeave={(e) => e.target.style.backgroundColor = styles.link.backgroundColor}
            >
                Malesef Tekrar Giriş Yapman Gerekiyor
            </Link>
        </div>
    );
}