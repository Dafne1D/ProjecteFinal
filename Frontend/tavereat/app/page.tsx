"use client";

import { useState, useEffect } from "react";
import Link from "next/link";
import { Menu, MapPin, Search, User, AlertCircle } from "lucide-react";

import CategoryCard from "../components/CategoryCard";
import ProductCard from "../components/ProductCard";

import { getCategories, Category } from "../Services/categoryAPI";
import { searchProducts, Product } from "../Services/productAPI";
import { isLoggedIn } from "@/Services/authAPI";

import { getMe, updateMe } from "@/Services/userAPI";

export default function Home() {
  const [categories, setCategories] = useState<Category[]>([]);
  const [searchQuery, setSearchQuery] = useState("");
  const [searchResults, setSearchResults] = useState<Product[]>([]);
  const [isSearching, setIsSearching] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const [logged, setLogged] = useState(false);

  const [direccio, setDireccio] = useState("La meva ubicació");
  const [editingLocation, setEditingLocation] = useState(false);
  const [newLocation, setNewLocation] = useState("");

  // AUTH
  useEffect(() => {
    const updateAuth = () => {
      setLogged(isLoggedIn());
    };

    updateAuth(); 

    window.addEventListener("auth-change", updateAuth);

    return () => {
      window.removeEventListener("auth-change", updateAuth);
    };
  }, []);

  // la meva ubicació update 
  useEffect(() => {
    if (!logged) return;

    const fetchUser = async () => {
      try {
        const user = await getMe();

        if (user.direccio) {
          setDireccio(user.direccio);
          setNewLocation(user.direccio);
        }
      } catch (err) {
        console.error(err);
      }
    };

    fetchUser();
  }, [logged]);

  // BUSCAR PRODUCTS
  useEffect(() => {
    if (!searchQuery.trim()) {
      setSearchResults([]);
      setIsSearching(false);
      return;
    }

    const delay = setTimeout(async () => {
      setIsSearching(true);
      try {
        const results = await searchProducts(searchQuery);
        setSearchResults(results);
      } catch (err) {
        console.error(err);
      } finally {
        setIsSearching(false);
      }
    }, 400);

    return () => clearTimeout(delay);
  }, [searchQuery]);

  // CATEGORIES
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

      const handleSaveLocation = async () => {
      try {
        const user = await getMe();

        await updateMe({
          nom: user.nom,
          email: user.email,
          direccio: newLocation,
          contrasenya: ""
        });

        setDireccio(newLocation);
        setEditingLocation(false);
      } catch (err) {
        console.error(err);
      }
    };

  return (
    <div className="min-h-screen bg-slate-50 flex flex-col font-sans text-slate-800">

      {/* ================= HEADER ================= */}
      <header className="sticky top-0 z-20 bg-white shadow-sm rounded-b-3xl pb-6 border-b border-slate-100">

        <div className="px-5 pt-6 pb-2 flex justify-between items-center">

          {/* MENU */}
          <button className="p-2 -ml-2 rounded-full hover:bg-slate-100">
            <Menu className="w-6 h-6" />
          </button>

          {/*DIRECCIÓ ENTREGA*/}
          <div className="flex flex-col items-center relative">
            <span className="text-xs font-semibold text-sky-500 uppercase tracking-widest">
              Direcció d&apos;entrega
            </span>

            <button
              onClick={() => logged && setEditingLocation(!editingLocation)}
              className="flex items-center space-x-1 cursor-pointer group"
            >
              <span className="text-sm font-bold truncate max-w-[170px] group-hover:text-sky-500">
                {direccio}
              </span>

              <MapPin className="w-4 h-4 text-sky-400" />
            </button>

            {/* CANVIAR LOCATION */}
            {editingLocation && (
              <div className="absolute top-14 bg-white shadow-xl border border-slate-200 rounded-2xl p-4 w-72 z-50">
                <input
                  type="text"
                  value={newLocation}
                  onChange={(e) => setNewLocation(e.target.value)}
                  placeholder="Nova direcció"
                  className="w-full border border-slate-200 rounded-xl px-3 py-2 outline-none focus:ring-2 focus:ring-sky-200"
                />

                <div className="flex gap-2 mt-3">
                  <button
                    onClick={handleSaveLocation}
                    className="flex-1 bg-sky-500 text-white py-2 rounded-xl hover:bg-sky-600 transition"
                  >
                    Guardar
                  </button>

                  <button
                    onClick={() => setEditingLocation(false)}
                    className="flex-1 bg-slate-100 py-2 rounded-xl hover:bg-slate-200 transition"
                  >
                    Cancelar
                  </button>
                </div>
              </div>
            )}
          </div>

          {/* USER BUTTON */}
          {logged ? (
            <Link
              href="/userMenu"
              className="p-2 -mr-2 rounded-full bg-orange-100 hover:bg-orange-200 transition-colors"
            >
              <User className="w-5 h-5 text-orange-500" />
            </Link>
          ) : (
            <Link
              href="/login"
              className="p-2 -mr-2 rounded-full bg-orange-100 hover:bg-orange-200 transition-colors"
            >
              <User className="w-5 h-5 text-orange-500" />
            </Link>
          )}
        </div>

        {/* SEARCH */}
        <div className="px-5 mt-2">
          <div className="flex items-center bg-slate-100 rounded-full px-4 h-12 shadow-inner focus-within:ring-4 focus-within:ring-sky-50">
            <Search className="w-5 h-5 mr-3 text-slate-400" />
            <input
              type="text"
              placeholder="Què vols menjar?"
              className="bg-transparent outline-none flex-1"
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
            />
          </div>
        </div>
      </header>

      {/* ================= MAIN ================= */}
      <main className="flex-1 px-5 py-6">

        {/* LOADING */}
        {isLoading && (
          <div className="space-y-4">
            <h2 className="text-xl font-extrabold">
              Carregant categories...
            </h2>
          </div>
        )}

        {/* ERROR */}
        {error && (
          <div className="flex flex-col items-center justify-center h-64 text-slate-500">
            <AlertCircle className="w-10 h-10 text-red-400 mb-2" />
            <p>{error}</p>
          </div>
        )}

        {/* SEARCH RESULTS */}
        {!isLoading && !error && searchQuery.trim() !== "" && (
          <>
            <h2 className="text-xl font-extrabold mb-4">
              Resultats de Búsqueda
            </h2>

            {isSearching ? (
              <div className="flex justify-center mt-10">
                <div className="animate-spin h-8 w-8 border-b-2 border-sky-500 rounded-full" />
              </div>
            ) : searchResults.length > 0 ? (
              <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
                {searchResults.map((product) => (
                  <ProductCard key={product.id || product.nom} p={product} />
                ))}
              </div>
            ) : (
              <div className="text-center mt-10 text-slate-500">
                No s&apos;han trobat resultats.
              </div>
            )}
          </>
        )}

        {/* CATEGORIES */}
        {!isLoading && !error && searchQuery.trim() === "" && (
          <>
            <h2 className="text-xl font-extrabold mb-4">
              Explorar Categorías
            </h2>

            <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
              {categories.map((category) => (
                <CategoryCard key={category.nom} category={category} />
              ))}
            </div>
          </>
        )}
      </main>
    </div>
  );
}