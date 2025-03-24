import React, { useState } from "react";

import { customerFilterstyles } from "../styles/customerFilterStyle";
import { regions } from "../../../constanst/sources";
import { GetFilteredCustomersService } from "../../../services/customerServices";

export default function CustomerFilter(props) {
    const {getCustomers, setCustomers} = props;
    const [filters, setFilters] = useState({
        name: "",
        region: "Africa",
        startDate: "",
        endDate: "",
    });

    const filterCustomer = async () => {
        const res = await GetFilteredCustomersService(filters.name, filters.region, filters.startDate, filters.endDate);
        if(res.status === 200){
            if(res.data.filteredCustomers.length === 0){
                alert("Hiç Müşteri Bulunamadı");
            }
            else{
                setCustomers(res.data.filteredCustomers);
            }
        }
        else{
            if(res.status === 400){
                alert(res.response.data.errorMessage);
            }
            else{
                alert("Beklenmedik Bir Hata Oluştu");
            }
        }
    };

    // Filtereler Değiştikte Değiş
    const handleFilterChange = (e) => {
        const { id, value } = e.target;
        setFilters((prevFilters) => ({
            ...prevFilters,
            [id]: value,
        }));
    };

    // Filtreleri Temizle
    const clearFilters = async () => {
        setFilters({
            name: "",
            region: "Africa",
            startDate: "",
            endDate: "",
        });
        await getCustomers();
    };

    return (
        <div style={customerFilterstyles.container}>
            <h1 style={customerFilterstyles.header}>Müşteri Filtreleme</h1>
            <div style={customerFilterstyles.filterContainer}>
                {/* İsim ve Bölge Alanları */}
                <div style={customerFilterstyles.inputGroup}>
                    <input
                        type="text"
                        id="name"
                        value={filters.name}
                        onChange={handleFilterChange}
                        style={customerFilterstyles.input}
                        placeholder="İsim Giriniz"
                    />
                    <select
                        id="region"
                        value={filters.region}
                        onChange={handleFilterChange}
                        style={customerFilterstyles.input}
                    >
                        {
                            regions.map(r => (
                                <option value={r}>{r}</option>
                            ))
                        }
                    </select>

                </div>
                <div style={customerFilterstyles.dateGroup}>
                    <label style={customerFilterstyles.label}>
                        Başlangıç
                        <input
                            type="date"
                            id="startDate"
                            value={filters.startDate}
                            onChange={handleFilterChange}
                            style={customerFilterstyles.input}
                        />
                    </label>
                    <label style={customerFilterstyles.label}>
                        Bitiş
                        <input
                            type="date"
                            id="endDate"
                            value={filters.endDate}
                            onChange={handleFilterChange}
                            style={customerFilterstyles.input}
                        />
                    </label>
                </div>
                <div style={customerFilterstyles.buttonContainer}>
                    <button
                        onClick={filterCustomer}
                        style={customerFilterstyles.button}
                    >
                        Filtrele
                    </button>
                    <button
                        onClick={clearFilters}
                        style={{ ...customerFilterstyles.button, ...customerFilterstyles.clearButton }}
                    >
                        Temizle
                    </button>
                </div>
            </div>
        </div>
    );
}