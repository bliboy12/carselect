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