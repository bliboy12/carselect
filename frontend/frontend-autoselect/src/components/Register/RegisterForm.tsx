import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { object, string, ref } from "yup";
import { Link } from "react-router";

// ── Schema ────────────────────────────────────────────────────────────────────
const schema = object({
    firstName: string().required("Voornaam is verplicht"),
    lastName: string().required("Achternaam is verplicht"),
    email: string().email("Ongeldig e-mailadres").required("E-mailadres is verplicht"),
    password: string().min(8, "Minimaal 8 tekens").required("Wachtwoord is verplicht"),
    confirmPassword: string()
        .oneOf([ref("password")], "Wachtwoorden komen niet overeen")
        .required("Bevestig je wachtwoord"),
});

// ── Types ─────────────────────────────────────────────────────────────────────
type RegisterFormData = {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    confirmPassword: string;
};

// ── Component ─────────────────────────────────────────────────────────────────
const RegisterForm = () => {
    const {register, handleSubmit, formState: { errors, isSubmitting }} = useForm<RegisterFormData>({
        resolver: yupResolver(schema),
    });

    const onSubmit = (data: RegisterFormData) => {
        // API call will go here later
        console.log("register", data);
    };

    return (
        <div className="flex items-center justify-center bg-gray-950 min-h-screen">
            <div className="w-full max-w-md flex flex-col gap-6 text-white p-8">

                {/* Header */}
                <div>
                    <h1 className="text-2xl font-bold">Create Your Free Account</h1>
                    <p className="text-gray-400 text-sm mt-1">Sign up with Google or enter your details below</p>
                </div>

                {/* Google button */}
                <button
                    type="button"
                    className="flex items-center justify-center gap-2 w-full border border-gray-700 rounded-lg py-2.5 text-sm hover:bg-white/5 transition-colors cursor-pointer"
                >
                    <img src="https://www.google.com/favicon.ico" alt="Google" className="w-4 h-4" />
                    Register with Google
                </button>

                {/* Divider */}
                <div className="flex items-center gap-3 text-gray-500 text-sm">
                    <hr className="flex-1 border-gray-700" />
                    or
                    <hr className="flex-1 border-gray-700" />
                </div>

                {/* Form */}
                <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col gap-4">

                    {/* Name row */}
                    <div className="grid grid-cols-2 gap-3">
                        <div className="flex flex-col gap-1">
                            <label className="text-sm text-gray-300">Firstname</label>
                            <input type="text" placeholder="Ali" {...register("firstName")}className="bg-gray-800 border border-gray-700 rounded-lg px-3 py-2.5 text-sm placeholder-gray-500 focus:outline-none focus:border-blue-500"/>
                            {errors.firstName && ( <p className="text-red-400 text-xs mt-0.5">{errors.firstName.message}</p>)}
                        </div>

                        <div className="flex flex-col gap-1">
                            <label className="text-sm text-gray-300">Lastname</label>
                            <input type="text" placeholder="Boubou" {...register("lastName")} className="bg-gray-800 border border-gray-700 rounded-lg px-3 py-2.5 text-sm placeholder-gray-500 focus:outline-none focus:border-blue-500"/>
                            {errors.lastName && ( <p className="text-red-400 text-xs mt-0.5">{errors.lastName.message}</p>)}
                        </div>
                    </div>

                    {/* Email */}
                    <div className="flex flex-col gap-1">
                        <label className="text-sm text-gray-300">E-mail</label>
                        <input type="email" placeholder="your@email.com" {...register("email")} className="bg-gray-800 border border-gray-700 rounded-lg px-3 py-2.5 text-sm placeholder-gray-500 focus:outline-none focus:border-blue-500"/>
                        {errors.email && ( <p className="text-red-400 text-xs mt-0.5">{errors.email.message}</p> )}
                    </div>

                    {/* Password */}
                    <div className="flex flex-col gap-1">
                        <label className="text-sm text-gray-300">Password</label>
                        <input type="password" placeholder="Min. 8 tekens" {...register("password")} className="bg-gray-800 border border-gray-700 rounded-lg px-3 py-2.5 text-sm placeholder-gray-500 focus:outline-none focus:border-blue-500" />
                        {errors.password && ( <p className="text-red-400 text-xs mt-0.5">{errors.password.message}</p> )}
                    </div>

                    {/* Confirm password */}
                    <div className="flex flex-col gap-1">
                        <label className="text-sm text-gray-300">Repeat password</label>
                        <input type="password" placeholder="repeat password" {...register("confirmPassword")} className="bg-gray-800 border border-gray-700 rounded-lg px-3 py-2.5 text-sm placeholder-gray-500 focus:outline-none focus:border-blue-500" />
                        {errors.confirmPassword && ( <p className="text-red-400 text-xs mt-0.5">{errors.confirmPassword.message}</p> )}
                    </div>

                    {/* Submit */}
                    <button type="submit" disabled={isSubmitting} className="w-full bg-blue-600 hover:bg-blue-700 disabled:opacity-50 disabled:cursor-not-allowed transition-colors rounded-lg py-2.5 text-sm font-semibold cursor-pointer mt-2" >
                        {isSubmitting ? "loading..." : "Create Account"}
                    </button>
                </form>

                {/* Login link */}
                <p className="text-center text-sm text-gray-500">
                    Already Have An Account?{" "}
                    <Link to="/login" className="text-blue-400 hover:underline">Login →</Link>
                </p>

            </div>
        </div>
    );
};

export default RegisterForm;