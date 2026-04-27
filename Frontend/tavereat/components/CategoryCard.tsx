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
      className="group bg-white rounded-2xl shadow-sm border border-gray-100 p-3 flex items-center space-x-4 transition-all hover:shadow-md hover:-translate-y-1 active:scale-95"
    >
      {/* IMAGE */}
      <div className="relative w-20 h-20 bg-sky-50 rounded-xl overflow-hidden shrink-0 border border-sky-100">

        {imageUrl ? (
          <Image
            src={imageUrl}
            alt={category.nom}
            fill
            unoptimized
            className="object-cover"
          />
        ) : (
          <div className="w-full h-full flex items-center justify-center text-slate-400 text-xs">
            Sense imatge
          </div>
        )}

      </div>

      {/* CONTENT */}
      <div className="flex-1 min-w-0">
        <h3 className="text-lg font-bold text-gray-800 truncate group-hover:text-sky-600 transition-colors">
          {category.nom}
        </h3>
      </div>

      {/* ARROW */}
      <div className="text-gray-300 group-hover:text-sky-600 transition-colors">
        <ChevronRight className="w-5 h-5" />
      </div>

    </Link>
  );
}