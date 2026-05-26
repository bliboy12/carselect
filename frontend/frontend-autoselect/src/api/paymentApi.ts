import { axiosApi } from "./axiosInstances";

interface PaymentIntentResponse {
    clientSecret: string
    publishableKey: string
    amount: number
    currency: string
}

export const createPaymentIntent = async (listingId: string, buyerId: string): Promise<PaymentIntentResponse> => {
    const response = await axiosApi.post<PaymentIntentResponse>('/payments/create-payment-intent', {
        listingId,
        buyerId
    });
    return response.data;
}