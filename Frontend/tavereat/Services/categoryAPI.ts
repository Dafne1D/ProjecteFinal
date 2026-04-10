export interface Category {
  nom: string;
}

const API_URL = "http://localhost:5000";

export const getCategories = async (): Promise<Category[]> => {
  const res = await fetch(`${API_URL}/categories`);

  if (!res.ok) {
    throw new Error("Error en obtenr les categories");
  }

  return res.json();
};