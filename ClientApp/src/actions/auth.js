import {registerAsync, loginAsync} from "../services/authService";
import { REGISTER_SUCCESS, LOGIN_SUCCESS } from "../constants/types";

export const register = (data) => async (dispatch) => {
    try {
    var res = await registerAsync(data);
    
    dispatch({type: REGISTER_SUCCESS, payload: res.data});
    } catch (error) {
        console.log(error.data)
    }
}

export const login = (data) => async (dispatch) => {
    try {
        var res = await loginAsync(data);
        
        dispatch({type: LOGIN_SUCCESS, payload: res.data});
    } catch (error) {
        console.log(error.data)
    }
}