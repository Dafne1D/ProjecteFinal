import Image from "next/image";
import { Product } from "../Services/productAPI";

export default function ProductCard({ p }: { p: Product }) {
  const imageUrl = p.imgUrl || "";

  return (
    <div className="group bg-white rounded-2xl border border-slate-100 shadow-sm p-3 hover:shadow-md transition flex items-center space-x-4">
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
}
