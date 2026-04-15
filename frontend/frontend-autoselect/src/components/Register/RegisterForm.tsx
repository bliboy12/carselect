import { useState } from "react";

const getPasswordStrength = (password: string) => {
  if (password.length === 0) return { score: 0, label: "", color: "" };
  if (password.length < 6) return { score: 1, label: "Zwak", color: "bg-red-500" };
  if (password.length < 10) return { score: 2, label: "Matig — voeg speciale tekens toe voor een sterker wachtwoord", color: "bg-yellow-500" };
  return { score: 3, label: "Sterk", color: "bg-green-500" };
};

const RegisterForm = () => {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [agreed, setAgreed] = useState(false);

  const strength = getPasswordStrength(password);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    console.log("register", { firstName, lastName, email, password });
  };

  return (
    <div className="flex items-center justify-center bg-gray-950 min-h-screen">
      <div className="w-full max-w-md flex flex-col gap-6 text-white p-8">

        {/* Header */}
        <div>
          <h1 className="text-2xl font-bold">Maak een gratis account</h1>
          <p className="text-gray-400 text-sm mt-1">Registreer via Google of vul je gegevens in</p>
        </div>

        {/* Google button */}
        <button className="flex items-center justify-center gap-2 w-full border border-gray-600 rounded-md py-2.5 text-sm hover:bg-white/5 transition-colors cursor-pointer">
          <img src="https://www.google.com/favicon.ico" alt="Google" className="w-4 h-4" />
          Registreren met Google
        </button>

        {/* Divider */}
        <div className="flex items-center gap-3 text-gray-500 text-sm">
          <hr className="flex-1 border-gray-700" />
          of
          <hr className="flex-1 border-gray-700" />
        </div>

        {/* Form */}
        <form onSubmit={handleSubmit} className="flex flex-col gap-4">

          {/* Name row */}
          <div className="grid grid-cols-2 gap-3">
            <div className="flex flex-col gap-1">
              <label className="text-sm text-gray-300">Voornaam</label>
              <input type="text"
                placeholder="Ali"
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
                className="bg-gray-800 border border-gray-700 rounded-md px-3 py-2.5 text-sm placeholder-gray-500 focus:outline-none focus:border-blue-500"
              />
            </div>
            <div className="flex flex-col gap-1">
              <label className="text-sm text-gray-300">Achternaam</label>
              <input
                type="text"
                placeholder="Boubou"
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
                className="bg-gray-800 border border-gray-700 rounded-md px-3 py-2.5 text-sm placeholder-gray-500 focus:outline-none focus:border-blue-500"
              />
            </div>
          </div>

          {/* Email */}
          <div className="flex flex-col gap-1">
            <label className="text-sm text-gray-300">E-mailadres</label>
            <input
              type="email"
              placeholder="jouw@email.be"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              className="bg-gray-800 border border-gray-700 rounded-md px-3 py-2.5 text-sm placeholder-gray-500 focus:outline-none focus:border-blue-500"
            />
          </div>

          {/* Password + strength */}
          <div className="flex flex-col gap-1">
            <label className="text-sm text-gray-300">Wachtwoord</label>
            <input
              type="password"
              placeholder="Min. 8 tekens"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="bg-gray-800 border border-gray-700 rounded-md px-3 py-2.5 text-sm placeholder-gray-500 focus:outline-none focus:border-blue-500"
            />
            {/* Strength bar */}
            {password.length > 0 && (
              <div className="mt-1">
                <div className="flex gap-1">
                  {[1, 2, 3].map((bar) => (
                    <div
                      key={bar}
                      className={`h-1 flex-1 rounded-full transition-colors duration-300 ${
                        bar <= strength.score ? strength.color : "bg-gray-700"
                      }`}
                    />
                  ))}
                </div>
                <p className="text-xs text-gray-400 mt-1">{strength.label}</p>
              </div>
            )}
          </div>

          {/* Confirm password */}
          <div className="flex flex-col gap-1">
            <label className="text-sm text-gray-300">Herhaal wachtwoord</label>
            <input
              type="password"
              placeholder="Herhaal wachtwoord"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
              className="bg-gray-800 border border-gray-700 rounded-md px-3 py-2.5 text-sm placeholder-gray-500 focus:outline-none focus:border-blue-500"
            />
          </div>

          {/* Terms checkbox */}
          <label className="flex items-start gap-3 cursor-pointer text-sm text-gray-400">
            <input
              type="checkbox"
              checked={agreed}
              onChange={(e) => setAgreed(e.target.checked)}
              className="mt-0.5 accent-blue-500 cursor-pointer"
            />
            <span>
              Ik ga akkoord met de{" "}
              <a href="/terms" className="text-blue-400 hover:underline">Algemene voorwaarden</a>
              {" "}en het{" "}
              <a href="/privacy" className="text-blue-400 hover:underline">Privacybeleid</a>
            </span>
          </label>

          {/* Submit */}
          <button
            type="submit"
            disabled={!agreed}
            className="w-full bg-blue-600 hover:bg-blue-700 disabled:opacity-50 disabled:cursor-not-allowed transition-colors rounded-md py-2.5 text-sm font-semibold cursor-pointer mt-2"
          >
            Account aanmaken
          </button>

        </form>

        {/* Login link */}
        <p className="text-center text-sm text-gray-500">
          Heb je al een account?{" "}
          <a href="/login" className="text-blue-400 hover:underline">Inloggen →</a>
        </p>

      </div>
    </div>
  );
};

export default RegisterForm;