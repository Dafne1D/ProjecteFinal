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
      <header className="sticky top-0 z-20 bg-white border-b shadow-sm">
        <div className="px-5 py-4 flex items-center gap-3">
          <button
            onClick={() => router.back()}
            className="p-2 rounded-full hover:bg-sky-50 transition"
          >
            <ArrowLeft className="w-5 h-5 text-sky-500" />
          </button>

          <h1 className="text-lg font-bold text-slate-800">
            {decodeURIComponent(name)}
          </h1>
        </div>
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