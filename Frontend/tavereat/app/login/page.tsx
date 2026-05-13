"use client";

import { useState, useEffect } from "react";
import { useRouter } from "next/navigation";
import { login } from "@/Services/loginAPI";
import { setToken, isLoggedIn } from "@/Services/authAPI";

export default function LoginPage() {
  const router = useRouter();

  const [email, setEmail] = useState("");
  const [contrasenya, setContrasenya] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  // si ya está logueado -> home
  useEffect(() => {
    if (isLoggedIn()) {
      router.replace("/");
    }
  }, [router]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    try {
      const res = await login({
        email,
        contrasenya,
      });

      setToken(res.token);

      router.replace("/");
    } catch (err) {
      if (err instanceof Error) setError(err.message);
      else setError("Error desconocido");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-slate-50">
      <form
        onSubmit={handleSubmit}
        className="bg-white p-6 rounded-xl shadow w-[320px] space-y-3"
      >
        <h1 className="text-xl font-bold">Inicia sessió</h1>

        <input
          className="w-full border p-2 rounded"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />

        <input
          className="w-full border p-2 rounded"
          placeholder="Contrasenya"
          type="password"
          value={contrasenya}
          onChange={(e) => setContrasenya(e.target.value)}
        />

        {error && <p className="text-red-500 text-sm">{error}</p>}

        <button
          disabled={loading}
          className="w-full bg-sky-600 text-white py-2 rounded"
        >
          {loading ? "Entrant..." : "Login"}
        </button>
      </form>
    </div>
  );
}