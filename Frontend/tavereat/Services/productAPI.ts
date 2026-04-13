export interface Product {
  id?: string;
  nom: string;
  descripcio: string;
  preu: number;
  categoria_nom?: string;
}

const API_URL = "http://localhost:5000";

export const getProductsByCategory = async (categoryName: string): Promise<Product[]> => {
  const res = await fetch(`${API_URL}/products/category/${encodeURIComponent(categoryName)}`);
  
  if (!res.ok) {
    throw new Error("Error en obtenir els llistat de productes");
  }

  return res.json();
};
