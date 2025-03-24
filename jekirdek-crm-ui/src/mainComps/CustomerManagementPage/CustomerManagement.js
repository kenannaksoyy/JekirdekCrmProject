import React, { useEffect, useState } from 'react';
import { custManStyles } from './styles/customerManStyle';
import CustomerManPagination from './subComps/CustomerManPagination';
import { getCustomersService } from '../../services/customerServices';
import { useNavigate } from 'react-router-dom';

export default function CustomerManagement() {
    const customersPerPage = 5;
    const navigate = useNavigate();

    const [customers, setCustomers] = useState([]);
    const [pageNumber, setPageNumber] = useState(0);
    const [pageCount, setPageCount] = useState();
    const pagesVisited = pageNumber * customersPerPage;

    //Sayfa İlk Yüklemesi 
    useEffect(() => {
        getCustomers();
    }, []);

    //Müşteriler Operasyonlar Sonrası Değiştikçe Sayfa Sayısı Değişcek
    useEffect(() => {
        setPageCount(Math.ceil(customers.length / customersPerPage));
    }, [customers]);

    const checkGeneralAuthorization = () => {
        //Genel Yetki Kontrolü
        const userRole = sessionStorage.getItem("userRole");
        return userRole === "Admin" || userRole === "User";
    };
    const checkAdminAuthorization = () => {
        //Sadece Admin Kontrolü
        const userRole = sessionStorage.getItem("userRole");
        return userRole === "Admin"
    };

    const getCustomers = async () => {
        if (checkGeneralAuthorization()) {
            const res = await getCustomersService();
            if (res.status === 200) {
                setCustomers(res.data.customers);
            }
            else {
                //Kullanıcı Tokeni Sorun Olduysa
                if (res.status === 403) {
                    alert("Tekrar Giriş Yapman Gerekiyor");
                    navigate("/");
                }
                else {
                    alert("Müşteriler Getirilken Beklenmedik Bir Hata Oluştu");
                }
            }

        }
        //Ekran Yetki Kontorü Tekrar Logine Dön
        else {
            alert("Senin Buraya Yetkin Yok");
            navigate("/");
            return;
        }

    };


    //Temel Olarak Sayfalama Yapımız
    //EarlyLoading İle Tüm Datalar Alındığı İçin
    //EasyLoading ile Olsaydı Api tarafında yapılcaktı ve İleri Geri Butonlarında İstekler Sağlancaktı
    const displayCustomers = customers
        .slice(pagesVisited, pagesVisited + customersPerPage)
        .map((customer) => (
            <tr key={customer.id}>
                <td style={custManStyles.td}>{customer.id}</td>
                <td style={custManStyles.td}>{customer.firstName}</td>
                <td style={custManStyles.td}>{customer.lastName}</td>
                <td style={custManStyles.td}>{customer.email}</td>
                <td style={custManStyles.td}>{customer.region}</td>
                <td style={custManStyles.td}>{customer.registrationDate}</td>
                <td style={custManStyles.td}>
                    <div style={{ display: 'flex', gap: '10px' }}>
                        <button
                            style={custManStyles.updateButton}
                            onMouseEnter={(e) => e.target.style.backgroundColor = "#218838"}
                            onMouseLeave={(e) => e.target.style.backgroundColor = "#28a745"}
                            onClick={() => handleUpdate(customer)}
                        >
                            Güncelle
                        </button>
                        <button
                            style={custManStyles.deleteButton}
                            onMouseEnter={(e) => e.target.style.backgroundColor = "#c82333"}
                            onMouseLeave={(e) => e.target.style.backgroundColor = "#dc3545"}
                            onClick={() => handleDelete(customer)}
                        >
                            Sil
                        </button>
                    </div>
                </td>
            </tr>
        )
    );

    //Müşteri Güncelleme
    const handleUpdate = (customer) => {
        if(checkAdminAuthorization()){
            console.log("Güncellenecek", customer);
        }
        else{
            alert("Müşteri Eklemeye Yetkin Yok");
        }
        
    };

    //Müşteri Silme
    const handleDelete = (customer) => {
        if(checkAdminAuthorization()){
            console.log("Silinecek", customer);
        }
        else{
            alert("Müşteri Eklemeye Yetkin Yok");
        }
        
    };

    //Müşteri Ekleme
    const handleAddCustomer = () => {
        if(checkAdminAuthorization()){
            console.log("Yeni Müşteri Ekleme");
        }
        else{
            alert("Müşteri Eklemeye Yetkin Yok");
        }
        
    };



    return (
        <div style={custManStyles.container}>
            <h1 style={custManStyles.header}>Müşteri Yönetimi</h1>
            <button
                style={custManStyles.addButton}
                onMouseEnter={(e) => e.target.style.backgroundColor = "#0056b3"}
                onMouseLeave={(e) => e.target.style.backgroundColor = "#007bff"}
                onClick={handleAddCustomer}
            >
                Yeni Müşteri Ekle
            </button>
            <table style={custManStyles.table}>
                <thead>
                    <tr>
                        <th style={custManStyles.th}>ID</th>
                        <th style={custManStyles.th}>First Name</th>
                        <th style={custManStyles.th}>Last Name</th>
                        <th style={custManStyles.th}>Email</th>
                        <th style={custManStyles.th}>Region</th>
                        <th style={custManStyles.th}>Registration Date</th>
                        <th style={custManStyles.th}>İşlemler</th>
                    </tr>
                </thead>
                <tbody>{displayCustomers}</tbody>
            </table>
            <CustomerManPagination pageNumber={pageNumber} pageCount={pageCount} setPageNumber={setPageNumber} />
        </div>
    );
}

