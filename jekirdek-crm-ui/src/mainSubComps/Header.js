import React from 'react';

//Renk Hex Kodları
//https://muratoner.net/online-araclar/html-renk-kodlari-color-codes-and-names Toolu Kullanıldı
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
  justifyContent: "center",
  width: "80vw",
  height: "5vh",
  backgroundColor: "#007bff",
  borderRadius: "5px"
};

const titleSyle = {
  color: "#ffffff",
  fontSize: "1.5rem"
};

export default function Header() {
  //Web Sayfasının Sabit Header Bileşeni
  //React Route ile Altındaki Main Bileşenler Değişecek
  return (
    <div className="headerContiner" style={containerStyle}>
      <header className="mainHeader" style={headerStyle}>
        <h1 style={titleSyle} className="mainHeaderTitle">Jekirdek Crm</h1>
      </header>
    </div>

  );
};