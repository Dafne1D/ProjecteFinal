"use client";

import { useState, useEffect } from "react";
import Link from "next/link";
import CategoryCard from "../components/CategoryCard";
import { Search, MapPin, AlertCircle, Menu, User } from "lucide-react";
import { getCategories, Category } from "../Services/categoryAPI";

export default function Home() {
  const [categories, setCategories] = useState<Category[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const data = await getCategories();
        setCategories(data);
      } catch (err: unknown) {
        setError(
          err instanceof Error
            ? err.message
            : "Error al conectar con el servidor."
        );
      } finally {
        setIsLoading(false);
      }
    };

    fetchCategories();
  }, []);

  return (
    <div className="min-h-screen bg-slate-50 flex flex-col font-sans text-slate-800">

      {/* HEADER */}
      <header className="sticky top-0 z-20 bg-white shadow-sm rounded-b-3xl pb-6 border-b border-slate-100">

        <div className="px-5 pt-6 pb-2 flex justify-between items-center">
          <button className="p-2 -ml-2 rounded-full hover:bg-slate-100">
            <Menu className="w-6 h-6" />
          </button>

          <div className="flex flex-col items-center">
            <span className="text-xs font-semibold text-sky-500 uppercase tracking-widest">
              Direcció d&apos;entrega
            </span>

            <div className="flex items-center space-x-1 cursor-pointer group">
              <span className="text-sm font-bold truncate max-w-[150px] group-hover:text-sky-500">
                La meva ubicació
              </span>
              <MapPin className="w-4 h-4 text-sky-400" />
            </div>
          </div>

          <button className="p-2 -mr-2 rounded-full bg-slate-100 hover:bg-slate-200">
            <User className="w-5 h-5" />
          </button>
        </div>

        {/* SEARCH */}
        <div className="px-5 mt-2">
          <div className="flex items-center bg-slate-100 rounded-full px-4 h-12 shadow-inner focus-within:ring-4 focus-within:ring-sky-50 focus-within:border-sky-300 transition-all">
            <Search className="w-5 h-5 mr-3 text-slate-400" />
            <input
              type="text"
              placeholder="Què vols menjar?"
              className="bg-transparent outline-none flex-1"
            />
          </div>
        </div>
      </header>

      {/* MAIN */}
      <main className="flex-1 px-5 py-6">

        {/* LOADING */}
        {isLoading && (
          <div className="space-y-4">
            <h2 className="text-xl font-extrabold">Carregant categories...</h2>

            <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
              {[1, 2, 3, 4, 5, 6].map(i => (
                <div key={i} className="h-24 bg-slate-200 animate-pulse rounded-xl" />
              ))}
            </div>
          </div>
        )}

        {/* ERROR */}
        {error && (
          <div className="flex flex-col items-center justify-center h-64 text-slate-500">
            <AlertCircle className="w-10 h-10 text-red-400 mb-2" />
            <p>{error}</p>

            <button
              onClick={() => window.location.reload()}
              className="mt-4 px-5 py-2 bg-slate-800 text-white rounded-full"
            >
              Reintentar
            </button>
          </div>
        )}

        {/* CATEGORIES */}
        {!isLoading && !error && (
          <>
            <h2 className="text-xl font-extrabold mb-4">
              Explorar Categorías
            </h2>

            <div className="grid grid-cols-2 md:grid-cols-4 gap-4">

              {categories.map((category) => (
                <Link
                  key={category.nom}
                  href={`/categories/${encodeURIComponent(category.nom)}`}
                  className="focus-visible:ring-2 focus-visible:ring-sky-500 rounded-2xl outline-none active:scale-95 transition-transform"
                >
                  <CategoryCard title={category.nom} />
                </Link>
              ))}
            </div>
          </>
        )}
      </main>
    </div>
  );
}