import { LOGIN_SUCCESS, AUTH_FAIL } from "../constants/types";

const initialState = { 
    token: localStorage.getItem('token'),
    isAuthenticated: false,
    loading: true
}
export default function auth (state=initialState, action)
{
    const {type, payload} = action;
    
    switch(type)
    {
        case LOGIN_SUCCESS:
            localStorage.setItem('token', payload.data);
            return { 
                ...state, 
                token: payload.data, 
                isAuthenticated: true, 
                loading: false
            };
        case AUTH_FAIL:
            localStorage.removeItem('token');
            return {
                ...state,
                token: null,
                isAuthenticated: false,
                loading: false
            }
        default:
            return state;
    }
};