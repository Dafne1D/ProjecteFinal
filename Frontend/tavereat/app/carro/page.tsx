"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";

import {
  ShoppingCart,
  Trash2,
  ArrowLeft,
} from "lucide-react";

import {
  getCart,
  deleteFromCart,
  Cart,
} from "@/Services/comandaVendaAPI";

import { getToken } from "@/Services/authAPI";

export default function CartPage() {
  const router = useRouter();

  const [cart, setCart] = useState<Cart | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!getToken()) {
      router.replace("/login");
      return;
    }

    loadCart();
  }, []);

  const loadCart = async () => {
    try {
      const data = await getCart();
      setCart(data);
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleRemove = async (producteId: string) => {
    try {
      await deleteFromCart(producteId);
      await loadCart();
    } catch (err) {
      console.error(err);
      alert("Error eliminant producte");
    }
  };


  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-slate-50">
        <div className="animate-spin h-10 w-10 rounded-full border-b-2 border-sky-500" />
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-slate-50">

      {/* HEADER */}
      <header className="h-20 bg-white border-b border-slate-200 flex items-center px-6">
        <button
          onClick={() => router.back()}
          className="mr-4 text-sky-600 hover:text-sky-700 transition"
        >
          <ArrowLeft size={22} />
        </button>

        <h1 className="text-2xl font-black text-slate-800">
          El teu carrito
        </h1>
      </header>

      {/* CONTENT */}
      <div className="max-w-2xl mx-auto px-4 py-6">

        {!cart || cart.productes.length === 0 ? (
          <div className="bg-white rounded-3xl p-10 shadow-sm text-center">
            <ShoppingCart className="mx-auto w-12 h-12 text-slate-300 mb-4" />

            <h2 className="text-xl font-bold text-slate-700">
              El carrito està buit
            </h2>

            <p className="text-slate-500 mt-2">
              Afegeix productes per començar una comanda
            </p>
          </div>
        ) : (
          <>
            <div className="space-y-4">

              {cart.productes.map((item) => (
                <div
                  key={item.producteId}
                  className="bg-white rounded-2xl shadow-sm border border-slate-100 p-4 flex items-center justify-between"
                >
                  <div>
                    <h2 className="font-bold text-slate-800">
                      {item.nom}
                    </h2>

                    <p className="text-sm text-slate-500">
                      Quantitat: {item.quantitat}
                    </p>

                    <p className="text-sky-600 font-bold mt-1">
                      {(item.preu * item.quantitat).toFixed(2)} €
                    </p>
                  </div>

                  <button
                    onClick={() => handleRemove(item.producteId)}
                    className="p-3 rounded-xl bg-red-50 hover:bg-red-100 transition"
                  >
                    <Trash2 className="w-5 h-5 text-red-500" />
                  </button>
                </div>
              ))}
            </div>

            {/* TOTAL */}
            <div className="bg-white rounded-3xl shadow-sm border border-slate-100 p-6 mt-6">

              <div className="flex items-center justify-between mb-5">
                <span className="text-lg font-semibold text-slate-600">
                  Total
                </span>

                <span className="text-3xl font-black text-sky-600">
                  {cart.total.toFixed(2)} €
                </span>
              </div>
            </div>
          </>
        )}
      </div>
    </div>
  );
}