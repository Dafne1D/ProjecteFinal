"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { getToken, logout } from "@/Services/authAPI";
import { getMe, updateMe, User } from "@/Services/userAPI";

export default function UserMenuPage() {
  const router = useRouter();

  const [form, setForm] = useState<User>({
    nom: "",
    email: "",
    direccio: "",
  });

  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);

  // auth guard
  useEffect(() => {
    if (!getToken()) {
      router.replace("/login");
      return;
    }

    loadUser();
  }, []);

  const loadUser = async () => {
    try {
      const data = await getMe();
      setForm(data);
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleSave = async () => {
    setSaving(true);

    try {
      await updateMe(form);

      alert("Perfil actualizado");
      router.push("/");
    } catch (err) {
      console.error(err);
      alert("Error guardando cambios");
    } finally {
      setSaving(false);
    }
  };

  const handleLogout = () => {
    logout();
    router.replace("/login");
  };

  if (loading) {
    return <div className="p-10">Cargando...</div>;
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-slate-50">
      <div className="bg-white p-6 rounded-xl shadow w-[350px] space-y-3">

        <h1 className="text-xl font-bold">Perfil d&apos;usuari</h1>

        <input
          className="w-full border p-2 rounded"
          value={form.nom}
          onChange={(e) => setForm({ ...form, nom: e.target.value })}
        />

        <input
          className="w-full border p-2 rounded"
          value={form.email}
          onChange={(e) => setForm({ ...form, email: e.target.value })}
        />

        <input
          className="w-full border p-2 rounded"
          value={form.direccio}
          onChange={(e) => setForm({ ...form, direccio: e.target.value })}
        />

        <button
          onClick={handleSave}
          disabled={saving}
          className="w-full bg-sky-600 text-white py-2 rounded"
        >
          {saving ? "Guardant..." : "Guardar canvis"}
        </button>

        <button
          onClick={handleLogout}
          className="w-full bg-red-500 text-white py-2 rounded"
        >
          Tancar sessió
        </button>
      </div>
    </div>
  );
}