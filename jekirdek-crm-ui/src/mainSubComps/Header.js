import React from 'react';

//Renk Hex Kodları
//https://muratoner.net/online-araclar/html-renk-kodlari-color-codes-and-names Toolu Kullanıldı
const headerStyle = {
  display: "flex",
  alignItems: "center",
  justifyContent: "center",
  backgroundColor: "#007bff",
  margin: "auto",
  padding: "10px 0px",
  width: "80vw",
  borderRadius: "5px"
};

const titleSyle = {
  color: "#ffffff",
  fontSize: "24px",
  margin: "0"
};

export default function Header() {
  //Web Sayfasının Sabit Header Bileşeni
  //React Route ile Altındaki Main Bileşenler Değişecek
  return (
    <header className="mainHeader" style={headerStyle}>
      <h1 style={titleSyle} className="mainHeaderTitle">Jekirdek Crm</h1>
    </header>
  );
};