import { axiosWithAuth } from "../tools/connector/axiostWithAuth";

export const userLogin = async (userName, password) => {
    try {
        const res = await axiosWithAuth.post("/Authentication/Login", {
            UserName: userName,
            Password: password
        });
        return res;
    } catch (err) {
        return err;
    }
};