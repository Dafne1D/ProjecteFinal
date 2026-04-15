"use client";

import { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import Image from "next/image";
import { ArrowLeft } from "lucide-react";

import { getProductsByCategory, Product } from "../../../Services/productAPI";

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
    <div className="min-h-screen bg-slate-50 flex flex-col font-sans">

      {/* HEADER */}
      <header className="sticky top-0 z-20 bg-white border-b border-slate-100 shadow-sm">
        <div className="px-5 py-4 flex items-center gap-3">
          <button
            onClick={() => router.back()}
            className="p-2 rounded-full hover:bg-sky-50 transition"
          >
            <ArrowLeft className="w-5 h-5 text-sky-500" />
          </button>

          <div>
            <p className="text-xs text-sky-400 uppercase tracking-widest">
              Categoria
            </p>
            <h1 className="text-lg font-bold text-slate-800">
              {decodeURIComponent(name)}
            </h1>
          </div>
        </div>
      </header>

      {/* CONTENT */}
      <main className="flex-1 px-5 py-6">

        {loading && (
          <div className="space-y-4">
            {[1, 2, 3].map((i) => (
              <div key={i} className="h-24 bg-slate-200 animate-pulse rounded-xl" />
            ))}
          </div>
        )}

        {!loading && products.length === 0 && (
          <div className="text-center mt-20">
            <h2 className="text-lg font-bold text-slate-800">
              No hi ha productes
            </h2>
          </div>
        )}

        {!loading && products.length > 0 && (
          <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">

            {products.map((p) => {
              const imageUrl = p.imgUrl || "";

              return (
                <div
                  key={p.id ?? `${p.nom}`}
                  className="group bg-white rounded-2xl border border-slate-100 shadow-sm p-3 hover:shadow-md transition flex items-center space-x-4"
                >

                  {/* IMAGE */}
                  <div className="relative w-24 h-24 bg-slate-100 rounded-xl overflow-hidden shrink-0">

                    {imageUrl ? (
                      <Image
                        src={imageUrl}
                        alt={p.nom}
                        fill
                        unoptimized
                        className="object-cover group-hover:scale-110 transition-transform duration-300"
                      />
                    ) : (
                      <div className="w-full h-full flex items-center justify-center text-slate-400 text-sm">
                        {imageUrl} {p.id}
                      </div>
                    )}

                  </div>

                  {/* CONTENT */}
                  <div className="flex-1 min-w-0 py-1">
                    <h2 className="font-bold text-slate-800 text-lg truncate">
                      {p.nom}
                    </h2>

                    <p className="text-sm text-slate-500 mt-0.5 line-clamp-2">
                      {p.descripcio}
                    </p>

                    <div className="mt-2 flex items-center justify-between">
                      <span className="text-sky-500 font-bold text-lg">
                        {p.preu} €
                      </span>

                      <button className="px-4 py-1.5 bg-sky-500 hover:bg-sky-600 text-white text-sm rounded-full transition">
                        Afegir
                      </button>
                    </div>
                  </div>

                </div>
              );
            })}

          </div>
        )}
      </main>
    </div>
  );
}