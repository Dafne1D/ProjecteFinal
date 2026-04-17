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
  const url = `${API_URL}/products/category/${encodeURIComponent(categoryName)}/full`;
  const res = await fetch(url);
  
  if (!res.ok) {
    let errText = "";
    try { errText = await res.text(); } catch (e) {}
    throw new Error(`API Error ${res.status} for URL ${url} | Details: ${errText || 'Sense detalls'}`);
  }

  return res.json();
};
