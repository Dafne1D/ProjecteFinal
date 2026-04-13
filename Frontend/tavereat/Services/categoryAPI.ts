export interface Category {
  nom: string;
}

const API_URL = "http://localhost:5000";

export const getCategoryImageUrl = (categoryName: string): string => {
  const name = (categoryName || "").toLowerCase();
  
  if (name.includes('pizza')) return 'https://images.unsplash.com/photo-1513104890138-7c749659a591?w=500&q=80';
  if (name.includes('begud') || name.includes('bebid') || name.includes('drink')) return 'https://images.unsplash.com/photo-1543878696-9f4a56a64bb5?w=500&q=80';
  if (name.includes('postre') || name.includes('dessert') || name.includes('dolç')) return 'https://images.unsplash.com/photo-1551024601-bec78aea704b?w=500&q=80';
  if (name.includes('burger') || name.includes('hamburgues')) return 'https://images.unsplash.com/photo-1568901346375-23c9450c58cd?w=500&q=80';
  if (name.includes('tapa') || name.includes('entrant')) return 'https://images.unsplash.com/photo-1541529086526-db283c563270?w=500&q=80';
  if (name.includes('pasta')) return 'https://images.unsplash.com/photo-1473093295043-cdd812d0e601?w=500&q=80';
  if (name.includes('gofre')) return 'https://images.unsplash.com/photo-1590080875515-8a886a07921a?w=500&q=80';
  if (name.includes('crep')) return 'https://images.unsplash.com/photo-1519676860045-d601662fb165?w=500&q=80';
  if (name.includes('amanida') || name.includes('ensalada') || name.includes('salad')) return 'https://images.unsplash.com/photo-1512621776951-a57141f2eefd?w=500&q=80';
  
  return 'https://images.unsplash.com/photo-1504674900247-0877df9cc836?w=500&q=80';
};

export const getCategories = async (): Promise<Category[]> => {
  const res = await fetch(`${API_URL}/categories`);

  if (!res.ok) {
    throw new Error("Error en obtenir les categories");
  }

  return res.json();
};