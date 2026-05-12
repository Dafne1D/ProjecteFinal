const API_URL = "http://localhost:5000";

export interface LoginRequest {
  email: string;
  contrasenya: string;
}

export interface LoginResponse {
  token: string;
  email: string;
}

export const login = async (
  data: LoginRequest
): Promise<LoginResponse> => {
  const res = await fetch(`${API_URL}/auth/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!res.ok) {
    let errText = "";

    try {
      errText = await res.text();
    } catch {
      errText = "Error al iniciar sessió";
    }

    throw new Error(errText || "Error al iniciar sessió");
  }

  return res.json();
};

export const saveToken = (token: string) => {
  localStorage.setItem("token", token);
};

export const getToken = () => {
  return localStorage.getItem("token");
};

export const logout = () => {
  localStorage.removeItem("token");
};

export const isLoggedIn = () => {
  return !!localStorage.getItem("token");
};