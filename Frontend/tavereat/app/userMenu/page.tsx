"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { User2, Mail, MapPin, Lock, Save, ArrowLeft } from "lucide-react";

import { getToken, logout } from "@/Services/authAPI";
import { getMe, updateMe, User } from "@/Services/userAPI";

export default function UserMenuPage() {
  const router = useRouter();

  const [form, setForm] = useState<User>({
    nom: "",
    email: "",
    direccio: "",
    contrasenya: "",
  });

  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);

  // AUTH GUARD
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
    return (
      <div className="min-h-screen flex items-center justify-center bg-slate-50">
        <div className="animate-spin h-10 w-10 rounded-full border-b-2 border-sky-500" />
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-slate-50">

      {/* HEADER */}
      <header className="h-20 bg-white border-b border-slate-200 flex items-center px-6 shadow-sm">

        <button
          onClick={() => router.back()}
          className="mr-4 text-sky-600 hover:text-sky-700 transition"
        >
          <ArrowLeft size={22} />
        </button>

        <h1 className="text-2xl font-black text-slate-800 truncate">
          El teu perfil
        </h1>
      </header>

      {/* PAGE */}
      <div className="flex justify-center px-4 py-10">

        <div className="w-full max-w-md">

          {/* CARD */}
          <div className="bg-white rounded-3xl shadow-sm border border-slate-100 overflow-hidden">

            {/* TOP */}
            <div className="bg-gradient-to-r from-sky-500 to-cyan-400 px-6 py-8 text-center">

              <div className="mx-auto h-24 w-24 rounded-full bg-white flex items-center justify-center shadow-lg">
                <User2 className="w-12 h-12 text-sky-500" />
              </div>

              <h1 className="text-2xl font-black text-white mt-4">
                Benvingut
              </h1>

              <p className="text-sky-100 font-semibold mt-1">
                {form.nom || "Usuari"}
              </p>
            </div>

            {/* CONTENT */}
            <div className="p-6 space-y-5">

              {/* NOM */}
              <div>
                <label className="text-sm font-semibold text-slate-500 mb-1 block">
                  Nom
                </label>

                <div className="flex items-center border rounded-xl px-3 h-12 bg-slate-50">
                  <User2 className="w-4 h-4 text-slate-400 mr-2" />

                  <input
                    className="bg-transparent outline-none flex-1"
                    value={form.nom}
                    onChange={(e) =>
                      setForm({ ...form, nom: e.target.value })
                    }
                  />
                </div>
              </div>

              {/* EMAIL */}
              <div>
                <label className="text-sm font-semibold text-slate-500 mb-1 block">
                  Email
                </label>

                <div className="flex items-center border rounded-xl px-3 h-12 bg-slate-50">
                  <Mail className="w-4 h-4 text-slate-400 mr-2" />

                  <input
                    className="bg-transparent outline-none flex-1"
                    value={form.email}
                    onChange={(e) =>
                      setForm({ ...form, email: e.target.value })
                    }
                  />
                </div>
              </div>

              {/* DIRECCIO */}
              <div>
                <label className="text-sm font-semibold text-slate-500 mb-1 block">
                  Direcció
                </label>

                <div className="flex items-center border rounded-xl px-3 h-12 bg-slate-50">
                  <MapPin className="w-4 h-4 text-slate-400 mr-2" />

                  <input
                    className="bg-transparent outline-none flex-1"
                    value={form.direccio}
                    onChange={(e) =>
                      setForm({ ...form, direccio: e.target.value })
                    }
                  />
                </div>
              </div>

              {/* PASSWORD */}
              <div>
                <label className="text-sm font-semibold text-slate-500 mb-1 block">
                  Nova contrasenya
                </label>

                <div className="flex items-center border rounded-xl px-3 h-12 bg-slate-50">
                  <Lock className="w-4 h-4 text-slate-400 mr-2" />

                  <input
                    type="password"
                    className="bg-transparent outline-none flex-1"
                    value={form.contrasenya}
                    onChange={(e) =>
                      setForm({ ...form, contrasenya: e.target.value })
                    }
                  />
                </div>
              </div>

              {/* SAVE */}
              <button
                onClick={handleSave}
                disabled={saving}
                className="w-full h-12 bg-sky-500 hover:bg-sky-600 transition rounded-xl text-white font-bold flex items-center justify-center gap-2"
              >
                <Save className="w-4 h-4" />

                {saving ? "Guardant..." : "Guardar canvis"}
              </button>

              {/* LOGOUT */}
              <button
                onClick={handleLogout}
                className="w-full h-12 bg-red-500 hover:bg-red-600 transition rounded-xl text-white font-bold"
              >
                Tancar sessió
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}