const API_URL = "http://localhost:5000";

type RegisterData = {
  nom: string;
  email: string;
  password: string;
};

export const registerUser = async (data: RegisterData) => {
  const res = await fetch(`${API_URL}/auth/register`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!res.ok) {
    throw new Error("Error al registrar-se");
  }

  return res.json();
};