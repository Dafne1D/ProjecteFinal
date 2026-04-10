"use client";

import { useState, useEffect } from "react";
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
    <div className="min-h-screen bg-slate-50 flex flex-col font-sans text-slate-800 selection:bg-emerald-200">

      {/* Header */}
      <header className="sticky top-0 z-20 bg-white shadow-sm rounded-b-3xl pb-6 border-b border-slate-100">
        <div className="px-5 pt-6 pb-2 flex justify-between items-center">
          <button className="p-2 -ml-2 rounded-full hover:bg-slate-100 transition-colors">
            <Menu className="w-6 h-6 text-slate-700" />
          </button>

          <div className="flex flex-col items-center">
            <span className="text-xs font-semibold text-emerald-600 uppercase tracking-widest">
              Direcció d&apos;entrega
            </span>
            <div className="flex items-center space-x-1 cursor-pointer group">
              <span className="text-sm font-bold truncate max-w-[150px] group-hover:text-emerald-600 transition-colors">
                La meva ubicació
              </span>
              <MapPin className="w-4 h-4 text-emerald-500 group-hover:translate-y-0.5 transition-transform" />
            </div>
          </div>

          <button className="p-2 -mr-2 rounded-full bg-slate-100 hover:bg-slate-200 transition-colors">
            <User className="w-5 h-5 text-slate-700" />
          </button>
        </div>

        {/* Search Bar */}
        <div className="px-5 mt-2">
          <div className="flex items-center bg-slate-100 rounded-full px-4 h-12 shadow-inner border border-transparent focus-within:border-emerald-300 focus-within:bg-white focus-within:ring-4 focus-within:ring-emerald-50 transition-all">
            <Search className="w-5 h-5 mr-3 text-slate-400" />
            <input
              type="text"
              placeholder="Què vols menjar?"
              className="bg-transparent outline-none flex-1 w-full text-slate-800 placeholder-slate-400 font-medium"
            />
          </div>
        </div>
      </header>

      {/* Main Content */}
      <main className="flex-1 px-5 py-6 overflow-y-auto">

        {/* Loading */}
        {isLoading && (
          <div className="space-y-4">
            <h2 className="text-xl font-extrabold text-slate-800 mb-4 tracking-tight">
              Carregant categories...
            </h2>
            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4">
              {[1, 2, 3, 4, 5, 6].map((i) => (
                <div
                  key={i}
                  className="bg-white rounded-2xl shadow-sm border border-slate-100 p-3 flex items-center space-x-4 animate-pulse"
                >
                  <div className="w-20 h-20 bg-slate-200 rounded-xl shrink-0"></div>
                  <div className="flex-1 space-y-3 py-2">
                    <div className="h-4 bg-slate-200 rounded-full w-3/4"></div>
                    <div className="h-3 bg-slate-200 rounded-full w-1/2"></div>
                  </div>
                </div>
              ))}
            </div>
          </div>
        )}

        {/* Error */}
        {error && (
          <div className="flex flex-col items-center justify-center h-64 text-slate-500 bg-white rounded-3xl shadow-sm border border-slate-100 p-8 text-center">
            <div className="w-16 h-16 bg-red-50 text-red-500 rounded-full flex items-center justify-center mb-4">
              <AlertCircle className="w-8 h-8" />
            </div>
            <h3 className="text-lg font-bold text-slate-800 mb-2">
              ¡Ups! Hi ha un error
            </h3>
            <p className="text-sm">{error}</p>
            <button
              onClick={() => window.location.reload()}
              className="mt-6 px-6 py-2.5 bg-slate-800 hover:bg-slate-900 text-white rounded-full font-medium transition-colors active:scale-95"
            >
              Reintentar
            </button>
          </div>
        )}

        {/* Empty */}
        {!isLoading && !error && categories.length === 0 && (
          <div className="flex flex-col items-center justify-center h-64 text-slate-500 bg-white rounded-3xl shadow-sm border border-slate-100 p-8 text-center">
            <div className="w-16 h-16 bg-emerald-50 text-emerald-500 rounded-full flex items-center justify-center mb-4">
              <Search className="w-8 h-8" />
            </div>
            <h3 className="text-lg font-bold text-slate-800 mb-2">
              Sense resultats
            </h3>
            <p className="text-sm">Sense categories disponibles</p>
          </div>
        )}

        {/* Data */}
        {!isLoading && !error && categories.length > 0 && (
          <div className="animate-in fade-in slide-in-from-bottom-4 duration-500">
            <h2 className="text-xl font-extrabold text-slate-800 mb-4 tracking-tight">
              Explorar Categorías
            </h2>
            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 gap-4 pb-8">
              {categories.map((category, index) => (
                <CategoryCard key={index} title={category.nom} />
              ))}
            </div>
          </div>
        )}
      </main>
    </div>
  );
}