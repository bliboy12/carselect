import type { User } from "../types";
import { axiosUsers } from "./axiosInstances";

export const getUserById = async (id: string): Promise<User> => {
    const response = await axiosUsers.get<User>(`/${id}`);
    return response.data;
}

export const updateUser = async (user: User): Promise<User> => {
    const response = await axiosUsers.post<User>(`${user.id}`, user);
    return response.data;
}