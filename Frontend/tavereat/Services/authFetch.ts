import { getToken, logout } from "@/Services/authAPI";

export const authFetch = async (url: string, options: RequestInit = {}) => {
  const token = getToken();

  if (!token) {
    throw new Error("No token");
  }

  const res = await fetch(url, {
    ...options,
    headers: {
      ...(options.headers || {}),
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  });
  
  if (res.status === 401) {
    window.location.href = "/login";
    throw new Error("Unauthorized");
  }

  return res;
};