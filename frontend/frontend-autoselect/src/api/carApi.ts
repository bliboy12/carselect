import type { CarRequest } from "../types/index"
import { axiosCars } from "./axiosInstances"


export const UpdateCarById = async (carId: string, car: CarRequest): Promise<CarRequest> => {
    const response = await axiosCars.put<CarRequest>(`/${carId}`, car);
    return response.data;
}