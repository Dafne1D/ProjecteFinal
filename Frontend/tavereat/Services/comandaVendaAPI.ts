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

export const addToCart = async (producteId: string) => {
  const res = await authFetch(`${API_URL}/cart/add`, {
    method: "POST",
    body: JSON.stringify({ producteId }),
  });

  if (!res.ok) throw new Error("Error afegint producte");

  const text = await res.text();
  return text ? JSON.parse(text) : null;
};

export const getCart = async (): Promise<Cart> => {
  const res = await authFetch(`${API_URL}/cart`);

  if (!res.ok) throw new Error("Error carregant carrito");

  return res.json();
};

export const updateCartItem = async (producteId: string, quantitat: number) => {
  const res = await authFetch(
    `${API_URL}/cart/item/update?producteId=${producteId}&quantitat=${quantitat}`,
    { method: "PUT" }
  );

  if (!res.ok) throw new Error("Error actualitzant item");

  const text = await res.text();
  return text ? JSON.parse(text) : null;
};

export const deleteFromCart = async (producteId: string) => {
  const res = await authFetch(`${API_URL}/cart/item/${producteId}`, {
    method: "DELETE",
  });

  if (!res.ok) throw new Error("Error eliminant item");
};

export const assignarDireccio = async (direccioId: string) => {
  const res = await authFetch(`${API_URL}/cart/direccio`, {
    method: "PUT",
    body: JSON.stringify({ direccioId }),
  });

  if (!res.ok) throw new Error("ERROR_ASSIGNAR_DIRECCIO");

  const text = await res.text();
  return text ? JSON.parse(text) : null;
};

export const confirmarComanda = async () => {
  const res = await authFetch(`${API_URL}/cart/confirmar`, {
    method: "POST",
  });

  if (!res.ok) throw new Error("ERROR_CONFIRMAR_COMANDA");

  const text = await res.text();
  return text ? JSON.parse(text) : null;
};