"use client";

import { useState } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { ArrowLeft } from "lucide-react";

export default function LoginPage() {
  const router = useRouter();

  const [email, setEmail] = useState("");
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    setLoading(true);

    await new Promise((resolve) => setTimeout(resolve, 1000));

    console.log(email);

    setLoading(false);
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

        <Link
          href="/"
          className="text-2xl font-black italic text-sky-600"
        >
          TaverEat
        </Link>
      </header>

      {/* MAIN */}
      <main className="flex-1 flex items-center justify-center px-4 py-10">
        <section className="w-full max-w-md bg-white rounded-3xl border border-slate-200 shadow-sm p-8">
          <h1 className="text-3xl font-black text-slate-800 leading-tight mb-7">
            Inicia sessió o crea un compte
          </h1>

          {/* GOOGLE */}
          <button
            type="button"
            className="w-full h-12 rounded-full border border-slate-300 bg-white hover:bg-slate-50 transition text-[15px] font-bold text-slate-700"
          >
            Continuar amb Google
          </button>

          {/* DIVIDER */}
          <div className="flex items-center gap-4 my-6">
            <div className="flex-1 h-px bg-slate-200" />
            <span className="text-sm text-slate-400">o</span>
            <div className="flex-1 h-px bg-slate-200" />
          </div>

          {/* FORM */}
          <form onSubmit={handleSubmit}>
            <label className="block text-sm font-semibold text-slate-700 mb-2">
              Continuar amb el correu electrònic
            </label>

            <input
              type="email"
              required
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="Introdueix el teu email"
              className="w-full h-12 rounded-xl border border-slate-300 px-4 text-[15px] text-slate-800 outline-none focus:border-sky-500 focus:ring-4 focus:ring-sky-100 transition"
            />

            <button
              type="submit"
              disabled={loading}
              className="w-full h-12 rounded-full bg-sky-600 hover:bg-sky-700 transition text-white font-bold mt-5 disabled:opacity-50"
            >
              {loading ? "Carregant..." : "Continuar"}
            </button>
          </form>

          {/* FOOTER */}
          <p className="text-center text-[12px] text-slate-500 leading-5 mt-6">
            Al continuar, acceptes els nostres{" "}
            <Link
              href="#"
              className="font-semibold text-slate-700 hover:underline"
            >
              Termes i condicions
            </Link>
            , la{" "}
            <Link
              href="#"
              className="font-semibold text-slate-700 hover:underline"
            >
              Política de privacitat
            </Link>{" "}
            i la{" "}
            <Link
              href="#"
              className="font-semibold text-slate-700 hover:underline"
            >
              Política de cookies
            </Link>
            .
          </p>
        </section>
      </main>
    </div>
  );
}