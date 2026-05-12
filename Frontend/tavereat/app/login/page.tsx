"use client";

import { useState } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { ArrowLeft } from "lucide-react";
import { login } from "@/Services/authAPI";

export default function LoginPage() {
  const router = useRouter();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState(""); // 👈 nuevo
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      setLoading(true);
      setError("");

      const res = await login({
        email,
        contrasenya: password,
      });

      // guardar token
      localStorage.setItem("token", res.token);

      console.log("LOGIN OK:", res);

      } catch (err: unknown) {
        if (err instanceof Error) {
          setError(err.message);
        } else {
          setError("Hi ha hagut un error");
        }
      } finally {
        setLoading(false);
      }
  };

  return (
    <div className="min-h-screen bg-slate-50 flex flex-col">
      {/* HEADER */}
      <header className="h-20 bg-white border-b border-slate-200 flex items-center px-6">
        <button
          onClick={() => router.back()}
          className="mr-4 text-sky-600 hover:text-sky-700 transition"
        >
          <ArrowLeft size={22} />
        </button>

        <Link href="/" className="text-2xl font-black italic text-sky-600">
          TaverEat
        </Link>
      </header>

      {/* MAIN */}
      <main className="flex-1 flex items-center justify-center px-4 py-10">
        <section className="w-full max-w-md bg-white rounded-3xl border border-slate-200 shadow-sm p-8">
          <h1 className="text-3xl font-black text-slate-800 mb-7">
            Inicia sessió
          </h1>

          <form onSubmit={handleSubmit}>
            <label className="block text-sm font-semibold mb-2">
              Email
            </label>

            <input
              type="email"
              required
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              className="w-full h-12 border rounded-xl px-4 mb-4"
            />

            <label className="block text-sm font-semibold mb-2">
              Contrasenya
            </label>

            <input
              type="password"
              required
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="w-full h-12 border rounded-xl px-4"
            />

            {error && (
              <p className="text-red-500 text-sm mt-3">{error}</p>
            )}

            <button
              type="submit"
              disabled={loading}
              className="w-full h-12 bg-sky-600 text-white rounded-full mt-5"
            >
              {loading ? "Carregant..." : "Entrar"}
            </button>
          </form>
        </section>
      </main>
    </div>
  );
}