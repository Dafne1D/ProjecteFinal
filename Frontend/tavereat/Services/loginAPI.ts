const API_URL = "http://localhost:5000"; 
export interface LoginRequest {
  email: string;
  contrasenya: string;
}

export interface AuthResponse {
  token: string;
  email: string;
}

export const login = async (data: LoginRequest): Promise<AuthResponse> => {
  const res = await fetch(`${API_URL}/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  });

  if (!res.ok) {
    const text = await res.text();
    throw new Error(text || "Login error");
  }

  return res.json();
};