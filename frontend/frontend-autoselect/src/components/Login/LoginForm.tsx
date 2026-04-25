import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { object, string } from "yup";
import { Link } from "react-router";

// ── Schema ────────────────────────────────────────────────────────────────────
const schema = object({
    email: string().email("Invalid email address").required("Email is required"),
    password: string().required("Password is required"),
});

// ── Types ─────────────────────────────────────────────────────────────────────
type LoginFormData = {
    email: string;
    password: string;
};

// ── Component ─────────────────────────────────────────────────────────────────
const LoginForm = () => {
    const {
        register,
        handleSubmit,
        formState: { errors, isSubmitting },
    } = useForm<LoginFormData>({
        resolver: yupResolver(schema),
    });

    const onSubmit = (data: LoginFormData) => {
        // API call will go here later
        console.log("login", data);
    };

    return (
        <div className="flex items-center justify-center bg-gray-950 min-h-screen">
            <div className="w-full max-w-md flex flex-col gap-6 text-white p-8">

                {/* Header */}
                <div>
                    <h1 className="text-2xl font-bold">Welcome back</h1>
                    <p className="text-gray-400 text-sm mt-1">Sign in with Google or enter your details</p>
                </div>

                {/* Google button */}
                <button
                    type="button"
                    className="flex items-center justify-center gap-2 w-full border border-gray-700 rounded-lg py-2.5 text-sm hover:bg-white/5 transition-colors cursor-pointer"
                >
                    <img src="https://www.google.com/favicon.ico" alt="Google" className="w-4 h-4" />
                    Continue with Google
                </button>

                {/* Divider */}
                <div className="flex items-center gap-3 text-gray-500 text-sm">
                    <hr className="flex-1 border-gray-700" />
                    or
                    <hr className="flex-1 border-gray-700" />
                </div>

                {/* Form */}
                <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col gap-4">

                    {/* Email */}
                    <div className="flex flex-col gap-1">
                        <label className="text-sm text-gray-300">Email address</label>
                        <input
                            type="email"
                            placeholder="your@email.com"
                            {...register("email")}
                            className="bg-gray-800 border border-gray-700 rounded-lg px-3 py-2.5 text-sm placeholder-gray-500 focus:outline-none focus:border-blue-500"
                        />
                        {errors.email && (
                            <p className="text-red-400 text-xs mt-0.5">{errors.email.message}</p>
                        )}
                    </div>

                    {/* Password */}
                    <div className="flex flex-col gap-1">
                        <div className="flex items-center justify-between">
                            <label className="text-sm text-gray-300">Password</label>
                            <a href="/forgot-password" className="text-xs text-blue-400 hover:underline">
                                Forgot password?
                            </a>
                        </div>
                        <input
                            type="password"
                            placeholder="••••••••"
                            {...register("password")}
                            className="bg-gray-800 border border-gray-700 rounded-lg px-3 py-2.5 text-sm placeholder-gray-500 focus:outline-none focus:border-blue-500"
                        />
                        {errors.password && (
                            <p className="text-red-400 text-xs mt-0.5">{errors.password.message}</p>
                        )}
                    </div>

                    {/* Submit */}
                    <button
                        type="submit"
                        disabled={isSubmitting}
                        className="w-full bg-blue-600 hover:bg-blue-700 disabled:opacity-50 disabled:cursor-not-allowed transition-colors rounded-lg py-2.5 text-sm font-semibold cursor-pointer mt-2"
                    >
                        {isSubmitting ? "Signing in..." : "Sign in"}
                    </button>

                </form>

                {/* Register link */}
                <p className="text-center text-sm text-gray-500">
                    Don't have an account?{" "}
                    <Link to="/register" className="text-blue-400 hover:underline">Register for free →</Link>
                </p>

            </div>
        </div>
    );
};

export default LoginForm;