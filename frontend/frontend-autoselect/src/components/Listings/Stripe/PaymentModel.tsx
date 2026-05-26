import { useState } from "react";
import { loadStripe } from "@stripe/stripe-js";
import { Elements, PaymentElement, useStripe, useElements } from "@stripe/react-stripe-js";

interface CheckoutFormProps {
    onSuccess: () => void
    onClose: () => void
}

const CheckoutForm = ({ onSuccess, onClose }: CheckoutFormProps) => {
    const stripe = useStripe();
    const elements = useElements();
    const [isProcessing, setIsProcessing] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const handleSubmit = async () => {
        if (!stripe || !elements) return;

        setIsProcessing(true);
        setError(null);

        const { error: submitError } = await stripe.confirmPayment({
            elements,
            redirect: "if_required"
        });

        if (submitError) {
            setError(submitError.message ?? "Payment failed");
            setIsProcessing(false);
        } else {
            onSuccess();
        }
    };

    return (
        <div className="flex flex-col gap-4">
            <PaymentElement />
            {error && <p className="text-red-400 text-sm">{error}</p>}
            <div className="flex gap-3 mt-2">
                <button
                    onClick={onClose}
                    className="flex-1 py-2.5 border border-gray-700 text-gray-400 hover:text-white rounded-xl text-sm transition-colors"
                >
                    Cancel
                </button>
                <button
                    onClick={handleSubmit}
                    disabled={isProcessing || !stripe}
                    className="flex-1 py-2.5 bg-blue-600 hover:bg-blue-500 disabled:opacity-50 disabled:cursor-not-allowed text-white rounded-xl text-sm transition-colors"
                >
                    {isProcessing ? "Processing..." : "Pay now"}
                </button>
            </div>
        </div>
    );
};

interface PaymentModalProps {
    clientSecret: string
    publishableKey: string
    amount: number
    onSuccess: () => void
    onClose: () => void
}

const PaymentModal = ({ clientSecret, publishableKey, amount, onSuccess, onClose }: PaymentModalProps) => {
    const stripePromise = loadStripe(publishableKey);

    return (
        <div className="fixed inset-0 bg-black/70 backdrop-blur-sm z-50 flex items-center justify-center p-4">
            <div className="bg-gray-900 border border-gray-700 rounded-2xl p-6 w-full max-w-md">
                <div className="mb-6">
                    <h2 className="text-lg font-semibold text-white">Complete payment</h2>
                    <p className="text-sm text-gray-400 mt-1">
                        Amount: <span className="text-white font-medium">€{amount.toLocaleString()}</span>
                    </p>
                </div>
                <Elements stripe={stripePromise} options={{ clientSecret }}>
                    <CheckoutForm onSuccess={onSuccess} onClose={onClose} />
                </Elements>
            </div>
        </div>
    );
};

export default PaymentModal;