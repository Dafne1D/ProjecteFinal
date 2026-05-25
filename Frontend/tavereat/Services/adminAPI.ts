import { authFetch } from "./authFetch";

const API_URL = "http://localhost:5000";

export type Comanda = {
  id: string;
  clientId: string;
  data: string;
  estat: string;
};

export const getComandes = async (): Promise<Comanda[]> => {
  const res = await authFetch(`${API_URL}/admin/comandes`);

  if (!res.ok) {
    throw new Error("Error carregant comandes");
  }

  return res.json();
};

export const updateEstat = async (
  comandaId: string,
  estat: string
) => {
  const res = await authFetch(
    `${API_URL}/admin/comandes/${comandaId}/estat`,
    {
      method: "PUT",
      body: JSON.stringify({ estat }),
    }
  );

  if (!res.ok) {
    throw new Error("Error actualitzant estat");
  }

  return res.json();
};