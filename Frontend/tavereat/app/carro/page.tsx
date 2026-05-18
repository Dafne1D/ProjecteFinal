"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";

import { ShoppingCart, Trash2, ArrowLeft } from "lucide-react";
import type { CartData } from "@/types/cart";

import { getCart, deleteFromCart } from "@/Services/comandaVendaAPI";
import { getToken } from "@/Services/authAPI";

export default function CartPage() {
  const router = useRouter();

  const [cart, setCart] = useState<CartData | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!getToken()) {
      router.replace("/login");
      return;
    }

    loadCart();
  }, [router]);

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
        <div className="animate-spin h-10 w-10 border-b-2 border-sky-500 rounded-full" />
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-slate-50">
      {/* HEADER */}
      <header className="h-20 bg-white border-b flex items-center px-6">
        <button
          onClick={() => router.back()}
          className="mr-4 text-sky-600"
        >
          <ArrowLeft size={22} />
        </button>

        <h1 className="text-2xl font-black">El teu carrito</h1>
      </header>

      <div className="max-w-2xl mx-auto px-4 py-6">
        {!cart || cart.productes.length === 0 ? (
          <div className="bg-white p-10 rounded-3xl text-center">
            <ShoppingCart className="mx-auto w-12 h-12 text-slate-300 mb-4" />
            <p className="font-bold">El carrito està buit</p>
          </div>
        ) : (
          <>
            <div className="space-y-4">
              {cart.productes.map((item) => (
                <div
                  key={item.producteId}
                  className="bg-white p-4 rounded-2xl flex justify-between"
                >
                  <div>
                    <p className="font-bold">{item.nom}</p>
                    <p className="text-sm text-slate-500">
                      Quantitat: {item.quantitat}
                    </p>
                    <p className="text-sky-600 font-bold">
                      {(item.preu * item.quantitat).toFixed(2)} €
                    </p>
                  </div>

                  <button
                    onClick={() => handleRemove(item.producteId)}
                    className="text-red-500"
                  >
                    <Trash2 />
                  </button>
                </div>
              ))}
            </div>

            <div className="mt-6 bg-white p-6 rounded-3xl">
              <p className="text-lg font-bold">
                Total: {cart.total.toFixed(2)} €
              </p>
            </div>
          </>
        )}
      </div>
    </div>
  );
}