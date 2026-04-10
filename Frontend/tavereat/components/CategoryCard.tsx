import { ChevronRight } from "lucide-react";

interface CategoryCardProps {
  title: string;
}

export default function CategoryCard({ title }: CategoryCardProps) {
  return (
    <div className="group bg-white rounded-2xl shadow-sm border border-gray-100 p-3 flex items-center space-x-4 transition-all hover:shadow-md hover:-translate-y-1 cursor-pointer active:scale-95">
      {/* Placeholder Image */}
      <div className="relative w-20 h-20 bg-emerald-50 rounded-xl overflow-hidden shrink-0 flex items-center justify-center border border-emerald-100">
        <span className="text-2xl font-bold text-emerald-600 opacity-60">
          {title.charAt(0).toUpperCase()}
        </span>
      </div>

      {/* Content */}
      <div className="flex-1 min-w-0">
        <h3 className="text-lg font-bold text-gray-800 truncate group-hover:text-emerald-600 transition-colors">
          {title}
        </h3>
        {/* Línea secundaria decorativa */}
        <p className="text-sm text-gray-500 truncate mt-1 flex items-center">
          <span className="w-2 h-2 rounded-full bg-emerald-400 mr-2"></span>
          Popular cerca de ti
        </p>
      </div>

      {/* Arrow */}
      <div className="text-gray-300 group-hover:text-emerald-600 transition-colors">
        <ChevronRight className="w-5 h-5" />
      </div>
    </div>
  );
}
