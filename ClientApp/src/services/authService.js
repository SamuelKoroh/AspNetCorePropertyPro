import http from './httpService';

const BASE_URL = 'https://localhost:5001/api/auth';
const config = { headers: {'Content-Type': 'application/json', 'X-Tenant-Id': 'b0ed668d-7ef2-4a23-a333-94ad278f45d7'}};


export const registerAsync = async (data) => {
    return  http.post(`${BASE_URL}/register`, data, config);
}
    
export const loginAsync  = async (data) => {
    return  http.post(`${BASE_URL}/login`, data, config);
}

