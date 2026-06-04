"use client";

import { useCart } from "@/context/CartContext";
import { useState } from "react";
import { X, Plus, Minus, Trash } from "lucide-react";
import {
  updateCartItem,
  deleteFromCart,
  assignarDireccio,
  confirmarComanda,
  CartLinea
} from "@/Services/comandaVendaAPI";

export default function CartDrawer() {
  const { open, setOpen, cart, refreshCart, loading } = useCart();
  const [selectedDireccio, setSelectedDireccio] = useState("");

  // INCREMENTAR
  const inc = async (item: CartLinea) => {
    try {
      const updatedCart = await updateCartItem(
        item.producteId,
        item.quantitat + 1
      );

      if (updatedCart) {
        await refreshCart();
      }
    } catch (err) {
      console.error(err);
    }
  };

  //  DECREMENTAR
  const dec = async (item: CartLinea) => {
    try {
      const newQty = item.quantitat - 1;

      await updateCartItem(item.producteId, newQty);
      await refreshCart();
    } catch (err) {
      console.error("Error reduint producte:", err);
    }
  };

  //  eliminar
  const remove = async (producteId: string) => {
    try {
      await deleteFromCart(producteId);
      await refreshCart();
    } catch (err) {
      console.error("Error eliminant:", err);
    }
  };

  const handleCheckout = async () => {
    try {
      if (!selectedDireccio) {
        alert("Selecciona una direcció");
        return;
      }

      await assignarDireccio(selectedDireccio);
      await confirmarComanda();
      await refreshCart();

      alert("Comanda confirmada!");
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <div
      className={`
        fixed top-0 right-0 h-full w-[360px] bg-white shadow-2xl border-l z-50
        transform transition-transform duration-300
        ${open ? "translate-x-0" : "translate-x-full"}
      `}
    >
      {/* HEADER */}
      <div className="p-4 border-b flex justify-between items-center">
        <h2 className="font-bold text-lg">El teu carrito</h2>

        <button
          onClick={() => setOpen(false)}
          className="p-2 rounded-lg hover:bg-slate-100"
        >
          <X size={20} />
        </button>
      </div>

      {/* BODY */}
      <div className="p-4 space-y-4 overflow-y-auto h-[calc(100%-140px)]">
        {loading && (
          <p className="text-sm text-slate-400">Carregant carrito...</p>
        )}

        {!loading && (!cart || cart.productes.length === 0) && (
          <div className="text-center text-slate-400 mt-10">
            El carrito està buit
          </div>
        )}

        {cart?.productes?.map((item) => (
          <div
            key={item.producteId}
            className="border rounded-xl p-3 space-y-2"
          >
            {/* HEADER ITEM */}
            <div className="flex justify-between">
              <span className="font-semibold">{item.nom}</span>

              <button
                onClick={() => remove(item.producteId)}
                className="text-red-500"
              >
                <Trash size={16} />
              </button>
            </div>

            {/* PRICE + QUANTITY */}
            <div className="flex justify-between items-center">
              <span className="text-sky-600 font-bold">
                {item.preu} €
              </span>

              <div className="flex items-center gap-2">
                <button
                  onClick={() => dec(item)}
                  className="p-1 rounded hover:bg-slate-100"
                >
                  <Minus size={16} />
                </button>

                <span className="min-w-[20px] text-center">
                  {item.quantitat}
                </span>

                <button
                  onClick={() => inc(item)}
                  className="p-1 rounded hover:bg-slate-100"
                >
                  <Plus size={16} />
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>

      {/* FOOTER */}
      <div className="absolute bottom-0 left-0 right-0 p-4 border-t bg-white">
        <div className="flex justify-between font-bold mb-3">
          <span>Total</span>
          <span className="text-sky-600">
            {cart?.total?.toFixed(2) ?? "0.00"} €
          </span>
        </div>

        <button
          onClick={handleCheckout}
          disabled={!cart || cart.productes.length === 0}
          className={`w-full py-2 rounded-xl font-semibold transition
            ${
              !cart || cart.productes.length === 0
                ? "bg-slate-200 text-slate-400"
                : "bg-sky-500 text-white hover:bg-sky-600"
            }`}
        >
          Finalitzar comanda
        </button>
      </div>
    </div>
  );
}