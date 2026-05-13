import { authFetch } from "@/Services/authFetch";

const API_URL = "http://localhost:5000";

export type UserUpdateRequest = {
  nom: string;
  email: string;
  direccio: string;
};

export type User = {
  nom: string;
  email: string;
  direccio: string;
};

// get user
export const getMe = async (): Promise<User> => {
  const res = await authFetch(`${API_URL}/auth/me`);

  if (!res.ok) {
    throw new Error("Error loading user");
  }

  return res.json();
};

//  UPDATE client
export const updateMe = async (data: UserUpdateRequest) => {
  const res = await authFetch(`${API_URL}/auth/me`, {
    method: "PUT",
    body: JSON.stringify(data),
  });

  if (!res.ok) {
    throw new Error("Error updating user");
  }

  return res.json();
};