import React from 'react';
import { useNavigate } from 'react-router-dom';
import { removeSession } from '../tools/general/sessionManagement';

// Renk Hex Kodları
// https://muratoner.net/online-araclar/html-renk-kodlari-color-codes-and-names Toolu Kullanıldı
const containerStyle = {
  display: "flex",
  alignItems: "center",
  justifyContent: "center",
  width: "100vw",
  height: "6vh"
};

const headerStyle = {
  display: "flex",
  alignItems: "center",
  justifyContent: "space-between",
  width: "80vw",
  height: "5vh",
  backgroundColor: "#007bff",
  borderRadius: "5px",
  padding: "0 20px"
};

const titleStyle = {
  color: "#ffffff",
  fontSize: "1.5rem"
};

const logoutButtonStyle = {
  backgroundColor: "#dc3545",
  color: "#ffffff",
  border: "none",
  borderRadius: "5px",
  padding: "5px 10px",
  cursor: "pointer"
};

export default function Header() {
  // Oturum Durumunu Kontrol et
  const isLoggedIn = sessionStorage.getItem("isLogin") === "true";
  const navigate = useNavigate();

  // Logout işlemi
  const handleLogout = () => {
    removeSession();
    navigate("/");
  };

  return (
    <div className="headerContainer" style={containerStyle}>
      <header className="mainHeader" style={headerStyle}>
        <h1 style={titleStyle} className="mainHeaderTitle">Jekirdek Crm</h1>
        {isLoggedIn && (
          <button style={logoutButtonStyle} onClick={handleLogout}>Çıkış Yap</button>
        )}
      </header>
    </div>
  );
}