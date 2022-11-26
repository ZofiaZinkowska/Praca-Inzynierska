import axios, { type Method } from "axios";

export abstract class ApiService {

    private readonly client = axios.create({
        baseURL: 'https://localhost:5001'
    });

    private async request<T>(method: Method, url: string, params?: any, data?: any) {
        const response = await this.client.request<T>({
            method,
            url,
            params,
            data,
        });
        return response.data;
    }

    protected delete<T>(path: string, params?: any) {
        return this.request<T>('delete', path, params);
    }

    protected get<T>(path: string, params?: any) {
        return this.request<T>('get', path, params);
    }

    protected put<T>(path: string, body?: any) {
        return this.request<T>('put', path, undefined, body);
    }
}