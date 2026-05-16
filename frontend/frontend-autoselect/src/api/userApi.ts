import type { User, UserRequest} from "../types";
import { axiosUsers } from "./axiosInstances";

export const getUserById = async (id: string): Promise<User> => {
    const response = await axiosUsers.get<User>(`/${id}`);
    return response.data;
}

export const updateUserById = async (user: UserRequest, id: string): Promise<UserRequest> => {
    const response = await axiosUsers.put<UserRequest>(`/${id}`, user);
    return response.data;
}

export const updateUserRoleById = async (userId: string, isAdmin: boolean): Promise<User> => {
    const response = await axiosUsers.patch<User>(`/${userId}/role`, isAdmin, {
        headers: { "Content-Type": "application/json"}
    });
    return response.data;
}

export const getAllUsers = async (): Promise<User[]> => {
    const response = await axiosUsers.get<User[]>(`/`);
    return response.data;
}

export const deleteUserById = async (userId: string): Promise<void> => {
    await axiosUsers.delete(`/${userId}`);
}
