"use client";

import { ShoppingCart } from "lucide-react";
import { Product } from "../Services/productAPI";
import { addToCart } from "@/Services/comandaVendaAPI";
import { useCart } from "@/context/CartContext";

export default function ProductCard({ p }: { p: Product }) {
  const imageUrl = p.imgUrl?.trim() || "";

  const { refreshCart, setOpen } = useCart();

  const handleAddToCart = async () => {
    try {
      await addToCart(p.id);

      await refreshCart();
      setOpen(true);

    } catch (err) {
      console.error(err);
    }
  };

  return (
    <div className="group bg-white rounded-3xl border border-slate-100 shadow-sm hover:shadow-lg transition overflow-hidden">

      {/* IMAGE */}
      <div className="relative h-44 bg-slate-100 overflow-hidden">
        {imageUrl ? (
          <img
            src={imageUrl}
            alt={p.nom}
            className="w-full h-full object-cover group-hover:scale-105 transition duration-300"
          />
        ) : (
          <div className="w-full h-full flex items-center justify-center text-slate-400 font-semibold">
            Sense imatge
          </div>
        )}

        {/* PRICE */}
        <div className="absolute top-3 right-3 bg-white/90 backdrop-blur px-3 py-1 rounded-full shadow text-sky-600 font-black text-sm">
          {p.preu} €
        </div>
      </div>

      {/* CONTENT */}
      <div className="p-4">

        <h2 className="font-black text-lg text-slate-800 truncate">
          {p.nom}
        </h2>

        <p className="text-sm text-slate-500 mt-1 line-clamp-2 min-h-[40px]">
          {p.descripcio}
        </p>

        {/* BUTTON */}
        <button
          onClick={handleAddToCart}
          className="mt-4 h-11 w-11 rounded-2xl bg-sky-500 hover:bg-sky-600 transition flex items-center justify-center text-white ml-auto"
        >
          <ShoppingCart className="w-5 h-5" />
        </button>
      </div>
    </div>
  );
}