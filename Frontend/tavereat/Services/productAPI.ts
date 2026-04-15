export interface Product {
  id: string;
  nom: string;
  descripcio: string;
  preu: number;
  categoria_nom?: string;
  imgUrl?: string;
}

const API_URL = "http://localhost:5000";

export const getProductsByCategory = async (categoryName: string): Promise<Product[]> => {
  const res = await fetch(`${API_URL}/products/category/${encodeURIComponent(categoryName)}/full`);
  
  if (!res.ok) {
    throw new Error("Error en obtenir els llistat de productes");
  }

  return res.json();
};
