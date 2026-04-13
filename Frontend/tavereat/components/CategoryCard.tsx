import { ChevronRight } from "lucide-react";
import { getCategoryImageUrl } from "../Services/categoryAPI";

interface CategoryCardProps {
  title: string;
}

export default function CategoryCard({ title }: CategoryCardProps) {
  return (
    <div className="group bg-white rounded-2xl shadow-sm border border-gray-100 p-3 flex items-center space-x-4 transition-all hover:shadow-md hover:-translate-y-1 cursor-pointer active:scale-95">
      {/* Category Image */}
      <div className="relative w-20 h-20 bg-sky-50 rounded-xl overflow-hidden shrink-0 flex items-center justify-center border border-sky-100">
        <img 
          src={getCategoryImageUrl(title)} 
          alt={title}
          className="w-full h-full object-cover group-hover:scale-110 transition-transform duration-300"
        />
      </div>

      {/* Content */}
      <div className="flex-1 min-w-0">
        <h3 className="text-lg font-bold text-gray-800 truncate group-hover:text-sky-600 transition-colors">
          {title}
        </h3>
      </div>

      {/* Arrow */}
      <div className="text-gray-300 group-hover:text-sky-600 transition-colors">
        <ChevronRight className="w-5 h-5" />
      </div>
    </div>
  );
}
