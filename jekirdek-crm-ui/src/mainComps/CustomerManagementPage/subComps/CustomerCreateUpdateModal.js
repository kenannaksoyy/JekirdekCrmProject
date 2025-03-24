import React, { useEffect, useState } from 'react';
import { customerModalStyles } from '../styles/customerModalStyle';
import { regions } from '../../../constanst/sources';

export default function CustomerCreateUpdateModal(props) {
    const { isModalOpen, onClose, selectedCustomer, modalMode } = props;
    const [formData, setFormData] = useState(null);

    useEffect(() => {
        //customer Baba Bileşende Güncelleme İçin Olcak Müşteri Eklemede null atanıyor
        //Modal Arkada Duruyor Müşteri İle Setlenmeli
        setFormData({
            id : selectedCustomer?.id || 0,
            firstName: selectedCustomer?.firstName || "",
            lastName: selectedCustomer?.lastName || "",
            email: selectedCustomer?.email || "",
            region: selectedCustomer?.region || "",
        });
    }, [selectedCustomer]);



    //Form Değişimi İd ile Eşleniyor
    const handleChange = (e) => {
        const { id, value } = e.target;
        setFormData((prev) => ({ ...prev, [id]: value }));
    };

    //Duruma Göre Servisleri Çağırcak 
    const handleModalSubmit = (e) => {
        e.preventDefault();
        if (modalMode === "update") {
            console.log('Güncellenecek Veri:', formData);

        } else if (modalMode === "add") {
            console.log('eklenecek Müşteri:', formData);

        }
        onClose();
    };

    //Zaten Kapalı İse Null Dönsün Güncelle Ve Ekle Butonları İle Sağlancak
    if (!isModalOpen) return null;

    return (
        <div style={customerModalStyles.overlay}>
            <div style={customerModalStyles.modal}>
                <h2>{modalMode === "update" ? "Müşteri Güncelle" : "Müşteri Ekle"}</h2>
                <form onSubmit={handleModalSubmit}>
                    <div style={customerModalStyles.formGroup}>
                        <label>First Name</label>
                        <input
                            type="text"
                            id="firstName"
                            value={formData.firstName}
                            onChange={handleChange}
                            style={customerModalStyles.input}
                        />
                    </div>
                    <div style={customerModalStyles.formGroup}>
                        <label>Last Name</label>
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
                        <label>Region</label>
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

