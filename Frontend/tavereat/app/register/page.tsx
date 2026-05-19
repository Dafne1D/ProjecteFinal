"use client";

import { useState } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";

import { registerUser } from "@/Services/registerAPI";

export default function RegisterPage() {
  const router = useRouter();

  const [nom, setNom] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const handleRegister = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      setLoading(true);
      setError("");

      await registerUser({
        nom,
        email,
        password,
      });

      localStorage.setItem("verifyEmail", email);

      router.push("/verify");
    } catch (err) {
      console.error(err);

      setError("No s'ha pogut completar el registre");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-slate-50 flex items-center justify-center px-4">
      <div className="w-full max-w-md bg-white rounded-3xl shadow-xl border border-slate-100 p-8">

        <div className="text-center mb-8">
          <h1 className="text-4xl font-black text-slate-800">
            Crear compte
          </h1>

          <p className="text-slate-500 mt-2">
            Registra&apos;t per començar a demanar
          </p>
        </div>

        {error && (
          <div className="mb-4 bg-red-50 border border-red-200 text-red-500 rounded-2xl px-4 py-3 text-sm">
            {error}
          </div>
        )}

        <form onSubmit={handleRegister} className="space-y-5">

          <div>
            <label className="block text-sm font-semibold text-slate-700 mb-2">
              Nom
            </label>

            <input
              type="text"
              value={nom}
              onChange={(e) => setNom(e.target.value)}
              required
              className="w-full h-12 rounded-2xl border border-slate-200 px-4 outline-none focus:border-sky-500 transition"
              placeholder="El teu nom"
            />
          </div>

          <div>
            <label className="block text-sm font-semibold text-slate-700 mb-2">
              Correu electrònic
            </label>

            <input
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              className="w-full h-12 rounded-2xl border border-slate-200 px-4 outline-none focus:border-sky-500 transition"
              placeholder="email@gmail.com"
            />
          </div>

          <div>
            <label className="block text-sm font-semibold text-slate-700 mb-2">
              Contrasenya
            </label>

            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              className="w-full h-12 rounded-2xl border border-slate-200 px-4 outline-none focus:border-sky-500 transition"
              placeholder="********"
            />
          </div>

          <button
            type="submit"
            disabled={loading}
            className="w-full h-12 rounded-2xl bg-sky-500 hover:bg-sky-600 transition text-white font-bold disabled:opacity-50"
          >
            {loading ? "Registrant..." : "Continuar"}
          </button>
        </form>

        <div className="mt-6 text-center text-sm text-slate-500">
          Ja tens compte?{" "}

          <Link
            href="/login"
            className="text-sky-600 font-semibold hover:text-sky-700"
          >
            Inicia sessió
          </Link>
        </div>
      </div>
    </div>
  );
}