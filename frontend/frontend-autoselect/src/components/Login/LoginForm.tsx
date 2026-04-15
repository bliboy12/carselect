import { Link } from "react-router";

const LoginForm = () => {
    
    return (
        <div className="flex flex-col gap-6 p-8 bg-gray-900 text-white">
            <div>
                <h3 className="text-2xl font-bold">Welkom Terug</h3>
                <p className="text-gray-400 text-sm mt-1">Login of maak een gratis account</p>
            </div>
            
            <div>
                <button className="flex items-center justify-center gap-2 w-full border border-gray-600 rounded-md py-2.5 text-sm hover:bg-white/5 transition-colors cursor-pointer">
                    <img src="https://www.google.com/favicon.ico" alt="Google" className="w-4 h-4"/>
                    Doorgaan met Google
                </button>
            </div>

            <div className="flex items-center gap-3 text-gray-500 text-sm">
                <hr className="flex-1 border-gray-600"/>
                of
                <hr className="flex-1 border-gray-600"/>
            </div>

            <form className="flex flex-col gap-4">
                <div className="flex flex-col gap-1">
                    <label className="ext-sm text-gray-300">E-mailadres</label>
                    <input type="email" placeholder="your@email.be" className="bg-gray-800 border border-gray-700 rounded-md px-3 py-2.5 text-sm placeholder-gray-500 focus:outline-none focus:border-blue-500"/> 
                </div>

                <div className="flex flex-col gap-1">
                    <label className="text-sm text-gray-300">Wachtwoord</label>
                    <input type="wachtwoord" placeholder="*******" className="bg-gray-800 border border-gray-700 rounded-md px-3 py-2.5 text-sm placeholder-gray-500 focus:outline-none focus:border-blue-500"></input>
                </div>

                <button type="submit" className="w-full bg-blue-600 hover:bg-blue-700 transition-colors rounded-md py-2.5 text-sm font-semibold cursor-pointer">Inloggen</button>
            </form>

            <p className="text-center text-sm text-gray-500">
                Nog geen account{" "}
                <Link to={"/register"} className="text-blue-400 hover:underline cursor-pointer">Registreer gratis</Link>
            </p>
        </div>
    )
}

export default LoginForm;