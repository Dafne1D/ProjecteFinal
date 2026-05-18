"use client";

import { useEffect, useState } from "react";
import Image from "next/image";
import Link from "next/link";
import { ChevronRight } from "lucide-react";
import { getProductsByCategory, Product } from "../Services/productAPI";

export interface Category {
  nom: string;
}

interface CategoryCardProps {
  category: Category;
}

export default function CategoryCard({ category }: CategoryCardProps) {
  const [imageUrl, setImageUrl] = useState<string>("");

  useEffect(() => {
    const loadImage = async () => {
      try {
        const products: Product[] = await getProductsByCategory(category.nom);

        if (!products.length) return;

        const firstProduct = products[0];

        if (firstProduct?.imgUrl) {
          setImageUrl(firstProduct.imgUrl);
        }
      } catch (err) {
        console.error("Error loading category image", err);
      }
    };

    loadImage();
  }, [category.nom]);

  return (
    <Link
      href={`/categories/${encodeURIComponent(category.nom)}`}
      className="group relative bg-white rounded-3xl border border-slate-100 shadow-sm hover:shadow-lg transition overflow-hidden"
    >
      {/* IMAGE */}
      <div className="relative h-40 bg-slate-100 overflow-hidden">
        {imageUrl ? (
          <Image
            src={imageUrl}
            alt={category.nom}
            fill
            unoptimized
            className="object-cover group-hover:scale-105 transition duration-300"
          />
        ) : (
          <div className="w-full h-full flex items-center justify-center text-slate-400 font-semibold">
            Sense imatge
          </div>
        )}

        <div className="absolute inset-0 bg-gradient-to-t from-black/40 via-black/10 to-transparent" />

        {/* TITUL */}
        <div className="absolute bottom-3 left-3 right-3 flex justify-between items-center">
          <h3 className="text-white font-black text-lg truncate">
            {category.nom}
          </h3>

          <div className="bg-white/90 backdrop-blur rounded-full p-1 text-sky-600">
            <ChevronRight className="w-4 h-4" />
          </div>
        </div>
      </div>
    </Link>
  );
}