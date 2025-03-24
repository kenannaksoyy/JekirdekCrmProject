import { axiosWithAuth } from "../tools/connector/axiostWithAuth";

export const getCustomersService = async () => {
    try{
        const res = await axiosWithAuth.get("/Customer/GetCustomers");
        return res;
    }
    catch(err){
        return err;
    }
};