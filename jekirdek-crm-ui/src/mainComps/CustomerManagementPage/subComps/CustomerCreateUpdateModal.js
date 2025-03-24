import React, { useEffect, useState } from 'react';
import { customerModalStyles } from '../styles/customerModalStyle';
import { regions } from '../../../constanst/sources';
import { createCustomerService, updateCustomerService } from '../../../services/customerServices';

export default function CustomerCreateUpdateModal(props) {
    const { isModalOpen, onClose, selectedCustomer, modalMode, getCustomers } = props;
    const [formData, setFormData] = useState(null);

    useEffect(() => {
        //customer Baba Bileşende Güncelleme İçin Olcak Müşteri Eklemede null atanıyor
        //Modal Arkada Duruyor Müşteri İle Setlenmeli
        setFormData({
            id: selectedCustomer?.id || 0,
            firstName: selectedCustomer?.firstName || "",
            lastName: selectedCustomer?.lastName || "",
            email: selectedCustomer?.email || "",
            region: selectedCustomer?.region || "Europe",
        });
    }, [selectedCustomer]);

    //Form Değişimi İd ile Eşleniyor
    const handleChange = (e) => {
        const { id, value } = e.target;
        setFormData((prev) => ({ ...prev, [id]: value }));
    };

    const updateCustomer = async (data) => {
        //Müşteri Güncelleme
        const res = await updateCustomerService(data);
        if (res.status === 204) {
            alert(data.id + " 'liMüşteri Güncellendi");
            await getCustomers();
            onClose();
        }
        else {
            if (res.status === 404 || res.status === 409 || res.status === 404) {
                alert(res.response.data.errorMessage);
            }
            else {
                alert("Beklenmedik Bir Hata Oluştu");
            }
        }

    };

    const crateCustomer = async (data) => {
        //Müşteri Oluşturma
        const res = await createCustomerService(data);
        if (res.status === 201) {
            alert(res.data.newCustomerId + " ile Yeni Müşteri Oluşturuldu");
            await getCustomers();
            onClose();
        }
        else {
            if (res.status === 409 || res.status === 400) {
                alert(res.response.data.errorMessage);
            }
            else {
                alert("Beklenmedik Bir Hata Oluştu");
            }
        }
    };

    //Duruma Göre Servisleri Çağırcak 
    const handleModalSubmit = async (e) => {
        e.preventDefault();
        if (!formData.firstName || !formData.lastName || !formData.email || !formData.region) {
            alert("Eksik Bilgiler Mevcut")
        }
        else {
            if (modalMode === "update") {
                await updateCustomer(formData);

            } else if (modalMode === "add") {
                await crateCustomer(formData);
            }
        }
    };

    //Zaten Kapalı İse Null Dönsün Güncelle Ve Ekle Butonları İle Sağlancak
    if (!isModalOpen) return null;

    return (
        <div style={customerModalStyles.overlay}>
            <div style={customerModalStyles.modal}>
                <h2>{modalMode === "update" ? "Müşteri Güncelle" : "Müşteri Ekle"}</h2>
                <form onSubmit={handleModalSubmit}>
                    <div style={customerModalStyles.formGroup}>
                        <label>İsim</label>
                        <input
                            type="text"
                            id="firstName"
                            value={formData.firstName}
                            onChange={handleChange}
                            style={customerModalStyles.input}
                        />
                    </div>
                    <div style={customerModalStyles.formGroup}>
                        <label>Soyisim</label>
                        <input
                            type="text"
                            id="lastName"
                            value={formData.lastName}
                            onChange={handleChange}
                            style={customerModalStyles.input}
                        />
                    </div>
                    <div style={customerModalStyles.formGroup}>
                        <label>Email</label>
                        <input
                            type="email"
                            id="email"
                            value={formData.email}
                            onChange={handleChange}
                            style={customerModalStyles.input}
                        />
                    </div>
                    <div style={customerModalStyles.formGroup}>
                        <label>Bölge</label>
                        <select
                            id="region"
                            value={formData.region}
                            onChange={handleChange}
                            style={customerModalStyles.input}
                        >
                            {
                                regions.map(r => (
                                    <option value={r}>{r}</option>
                                ))
                            }
                        </select>
                    </div>
                    <div style={customerModalStyles.buttonGroup}>
                        <button type="button" onClick={onClose} style={customerModalStyles.cancelButton}>
                            İptal
                        </button>
                        <button type="submit" style={customerModalStyles.submitButton}>
                            {modalMode === "update" ? "Güncelle" : "Oluştur"}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

