import { authFetch } from "./authFetch";

const API_URL = "http://localhost:5000";

export type CartLinea = {
  producteId: string;
  nom: string;
  preu: number;
  quantitat: number;
};

export type Cart = {
  comandaId: string;
  productes: CartLinea[];
  total: number;
};

// ADD
export const addToCart = async (producteId: string) => {
  const res = await authFetch(`${API_URL}/cart/add`, {
    method: "POST",
    body: JSON.stringify({ producteId }),
  });

  if (!res.ok) throw new Error("Error afegint producte");
  return res.json();
};

// GET
export const getCart = async (): Promise<Cart> => {
  const res = await authFetch(`${API_URL}/cart`);

  if (!res.ok) throw new Error("Error carregant carrito");

  return res.json();
};

// PUT item
export const updateCartItem = async (producteId: string, quantitat: number) => {
  const res = await authFetch(
    `${API_URL}/cart/item/update?producteId=${producteId}&quantitat=${quantitat}`,
    {
      method: "PUT",
    }
  );

  if (!res.ok) throw new Error("Error actualitzant item");

  return res.json();
};

// REMOVE
export const deleteFromCart = async (producteId: string) => {
  const res = await authFetch(`${API_URL}/cart/item/${producteId}`, {
    method: "DELETE",
  });

  if (!res.ok) throw new Error("Error eliminant item");

  return;
};