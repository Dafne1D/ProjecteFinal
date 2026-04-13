const API_URL = "http://localhost:5000";

export const getProductImage = async (productId: string): Promise<string | null> => {
  if (!productId) return null;

  try {
    const res = await fetch(`${API_URL}/img_url/${productId}/image`);

    if (!res.ok) return null;

    const data = await res.json();
    return data.url;
  } catch (err) {
    console.error("Error en obtenir l'imatge:", err);
    return null;
  }
};