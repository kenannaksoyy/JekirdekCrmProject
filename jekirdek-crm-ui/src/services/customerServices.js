import { axiosWithAuth } from "../tools/connector/axiostWithAuth";

export const getCustomersService = async () => {
    try {
        const res = await axiosWithAuth.get("/Customer/GetCustomers");
        return res;
    }
    catch (err) {
        return err;
    }
};

export const createCustomerService = async (data) => {
    try {
        const res = await axiosWithAuth.put("/Customer/CreateCustomer", {
            firstName: data.firstName,
            lastName: data.lastName,
            email: data.email,
            region: data.region
        });
        return res;
    }
    catch (err) {
        return err;
    }
};

export const updateCustomerService = async (data) => {
    try {
        const res = await axiosWithAuth.post("/Customer/UpdateCustomer", {
            id: data.id,
            firstName: data.firstName,
            lastName: data.lastName,
            email: data.email,
            region: data.region
        });
        return res;
    }
    catch (err) {
        return err;
    }
};

export const deleteCustomerService = async (id) => {
    try {
        const res = await axiosWithAuth.delete(`/Customer/DeleteCustomer/${id}`);
        return res;
    }
    catch (err) {
        return err;
    }
};


export const GetFilteredCustomersService = async (name, region, startDate, endDate) => {
    try {
        const res = await axiosWithAuth.get(`/Customer/GetFilteredCustomers?FirstName=${name}&Region=${region}&StartDate=${startDate}&EndDate=${endDate}`);
        return res;
    }
    catch (err) {
        return err
    }
};