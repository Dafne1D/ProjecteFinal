import { authFetch } from "./authFetch";

const API_URL = "http://localhost:5000";

export type Comanda = {
  id: string;
  estat: string;
  data: string;

  productes?: {
    nomProducte: string;
    quantitat: number;
  }[];

  direccio?: string;
};

export const getComandes = async () => {
  const res = await authFetch(`${API_URL}/admin/comandes`);

  if (res.status === 403) {
    throw new Error("FORBIDDEN");
  }

  if (!res.ok) {
    throw new Error("ERROR_GET_COMANDES");
  }

  return res.json();
};

export const updateEstat = async (comandaId: string, estat: string) => {
  const res = await authFetch(
    `${API_URL}/admin/comandes/${comandaId}/estat`,
    {
      method: "PUT",
      body: JSON.stringify({ estat }),
    }
  );

  if (res.status === 403) {
    throw new Error("FORBIDDEN");
  }

  if (!res.ok) {
    throw new Error("ERROR_UPDATE_ESTAT");
  }

  return res.json();
};