import { Navigate } from "react-router-dom";

export default function PrivateRoute({ children }) {
  //SessionStroge' dan Kullanıcı Login Kontrolü Yapacak 
  //Kullanıcı Login Değilse Müşteri Yönetim Paneli Açılmayacak
  //Login Ekranına Yönlendirecek
  const isAuthenticated = sessionStorage.getItem("isLogin") === "true";
  if (!isAuthenticated) {
    return <Navigate to="/" replace />;
  }
  return children;
};