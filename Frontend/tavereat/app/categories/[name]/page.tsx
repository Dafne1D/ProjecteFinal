"use client";

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import { ArrowLeft } from "lucide-react";
import ProductCard from "@/components/ProductCard";
import { getProductsByCategory, Product } from "@/Services/productAPI";

export default function CategoryPage() {
  const params = useParams();
  const router = useRouter();

  const name = params?.name as string;

  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!name) return;

    const fetchProducts = async () => {
      try {
        setLoading(true);
        const data = await getProductsByCategory(name);
        setProducts(data);
        console.log(data);
      } catch (err) {
        console.error("Error loading products", err);
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
  }, [name]);

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

        <h1 className="text-2xl font-black text-slate-800 truncate">
          {decodeURIComponent(name)}
        </h1>
      </header>

      {/* CONTENT */}
      <main className="flex-1 px-5 py-6">

        {/* LOADING */}
        {loading && (
          <div className="space-y-4">
            {[1, 2, 3].map((i) => (
              <div
                key={i}
                className="h-24 bg-slate-200 animate-pulse rounded-xl"
              />
            ))}
          </div>
        )}

        {/* EMPTY */}
        {!loading && products.length === 0 && (
          <div className="text-center mt-20">
            <h2 className="text-lg font-bold text-slate-800">
              No hi ha productes
            </h2>
          </div>
        )}

        {/* PRODUCTS */}
        {!loading && products.length > 0 && (
          <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
            {products.map((p) => (
              <ProductCard key={p.id ?? p.nom} p={p} />
            ))}
          </div>
        )}

      </main>
    </div>
  );
}