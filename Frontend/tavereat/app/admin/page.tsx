"use client";

import { useEffect, useState } from "react";
import { getComandes, updateEstat, Comanda } from "@/Services/adminAPI";
import { useRouter } from "next/navigation";

const ESTATS = ["pendent", "preparant", "repartiment", "entregada"];

export default function AdminComandesPage() {
  const [comandes, setComandes] = useState<Comanda[]>([]);
  const [loading, setLoading] = useState(true);
  const [forbidden, setForbidden] = useState(false);
  const router = useRouter();

  const load = async () => {
    try {
      const data = await getComandes();
      setComandes(data);
      setForbidden(false);
    } catch (err) {
      if (err instanceof Error) {
        if (err.message === "FORBIDDEN") {
          setForbidden(true);
          return;
        }

        if (err.message === "UNAUTHORIZED") {
          router.push("/login");
          return;
        }
      }

      setForbidden(true);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    load();
  }, []);

  const move = async (id: string, nextEstat: string) => {
    try {
      await updateEstat(id, nextEstat);
      await load();
    } catch (err) {
      console.error("Error actualitzant estat:", err);
    }
  };

  if (forbidden) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-slate-100">
        <div className="text-center bg-white p-10 rounded-2xl shadow">
          <h1 className="text-2xl font-black text-red-500">
            No tens permisos d’administrador
          </h1>
          <p className="text-slate-500 mt-2">
            Contacta amb un administrador
          </p>
        </div>
      </div>
    );
  }

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        Carregant...
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-slate-100 p-6">
      <h1 className="text-4xl font-black mb-8">Panell administratiu</h1>

      <div className="grid grid-cols-4 gap-6">
        {ESTATS.map((estat) => (
          <div key={estat} className="bg-white rounded-3xl p-4 shadow-sm">
            <h2 className="text-xl font-black capitalize mb-4">
              {estat}
            </h2>

            <div className="space-y-4">
              {comandes
                .filter((c) => c.estat === estat)
                .map((comanda) => (
                  <div
                    key={comanda.id}
                    className="bg-slate-50 rounded-2xl p-4"
                  >
                    <p className="font-bold text-sm">
                      #{comanda.id.slice(0, 8)}
                    </p>

                    <p className="text-xs text-slate-500 mt-1">
                      {new Date(comanda.data).toLocaleString()}
                    </p>

                    {/* PRODUCTES en PREPARANT */}
                    {estat === "preparant" && comanda.productes && (
                      <div className="mt-2 text-xs text-slate-600">
                        {comanda.productes.map((p, i) => (
                          <div key={i}>
                            {p.nomProducte} x{p.quantitat}
                          </div>
                        ))}
                      </div>
                    )}

                    {/* DIRECCIÓN en REPARTIMENT */}
                    {estat === "repartiment" && comanda.direccio && (
                      <div className="mt-2 text-xs text-slate-600">
                        {comanda.direccio}
                      </div>
                    )}

                    {/* BOTONES */}
                    <div className="mt-4 flex flex-col gap-2">
                      {estat === "pendent" && (
                        <button
                          onClick={() => move(comanda.id, "preparant")}
                          className="bg-orange-500 text-white rounded-xl py-2"
                        >
                          Començar
                        </button>
                      )}

                      {estat === "preparant" && (
                        <button
                          onClick={() => move(comanda.id, "repartiment")}
                          className="bg-sky-500 text-white rounded-xl py-2"
                        >
                          Enviar repartiment
                        </button>
                      )}

                      {estat === "repartiment" && (
                        <button
                          onClick={() => move(comanda.id, "entregada")}
                          className="bg-green-500 text-white rounded-xl py-2"
                        >
                          Entregada
                        </button>
                      )}
                    </div>
                  </div>
                ))}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}