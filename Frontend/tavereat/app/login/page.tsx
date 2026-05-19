"use client";

import { useState, useEffect } from "react";
import { useRouter } from "next/navigation";
import { ArrowLeft } from "lucide-react";

import { login } from "@/Services/loginAPI";
import { setToken, isLoggedIn } from "@/Services/authAPI";
import Link from "next/link";

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
      if (err instanceof Error) {
        setError(err.message);
      } else {
        setError("Error desconocido");
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-slate-50 flex flex-col">

      {/* HEADER */}
      <header className="h-20 bg-white border-b border-slate-200 flex items-center px-6 shadow-sm">

        <button
          onClick={() => router.back()}
          className="mr-4 text-sky-600 hover:text-sky-700 transition"
        >
          <ArrowLeft size={22} />
        </button>
    
        <h1 className="text-2xl font-black text-slate-800 truncate">
          Login
        </h1>
      </header>

      {/* CONTENT */}
      <div className="flex-1 flex items-center justify-center px-4">

        <form
          onSubmit={handleSubmit}
          className="bg-white p-6 rounded-2xl shadow-lg w-full max-w-[340px] space-y-4"
        >
          
          <div>
            <h2 className="text-2xl font-black text-slate-800">
              Inicia sessió
            </h2>

            <p className="text-sm text-slate-500 mt-1">
              Accedeix al teu compte de TaverEat
            </p>
          </div>

          <input
            className="w-full border border-slate-200 p-3 rounded-xl outline-none focus:ring-4 focus:ring-sky-100 focus:border-sky-400 transition"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />

          <input
            className="w-full border border-slate-200 p-3 rounded-xl outline-none focus:ring-4 focus:ring-sky-100 focus:border-sky-400 transition"
            placeholder="Contrasenya"
            type="password"
            value={contrasenya}
            onChange={(e) => setContrasenya(e.target.value)}
          />

          {error && (
            <p className="text-red-500 text-sm">
              {error}
            </p>
          )}

          <button
            disabled={loading}
            className="w-full bg-sky-600 hover:bg-sky-700 disabled:bg-sky-300 text-white py-3 rounded-xl font-semibold transition"
          >
            {loading ? "Entrant..." : "Login"}
          </button>

          <Link href="/register">
          Registrar-se
          </Link>
        </form>
      </div>
    </div>
  );
}