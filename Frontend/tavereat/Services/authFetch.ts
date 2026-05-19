export const authFetch = async (
  url: string,
  options: RequestInit = {}
) => {
  const token = localStorage.getItem("token");

  // NO LOGIN
  if (!token) {
    window.location.href = "/login";
  }

  const res = await fetch(url, {
    ...options,
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
      ...(options.headers || {}),
    },
  });

  // TOKEN INVÁLIDO / EXPIRADO
  if (res.status === 401) {
    localStorage.removeItem("token");

    window.location.href = "/login";

    throw new Error("Unauthorized");
  }

  return res;
};