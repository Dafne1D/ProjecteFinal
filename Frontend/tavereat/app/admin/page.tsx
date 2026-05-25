"use client";

import { useEffect, useState } from "react";

import {
  getComandes,
  updateEstat,
  Comanda,
} from "@/Services/adminAPI";

const ESTATS = [
  "pendent",
  "cuinant",
  "repartiment",
  "entregada",
];

export default function AdminComandesPage() {
    const [comandes, setComandes] = useState<Comanda[]>([]);

    const load = async () => {
        const data = await getComandes();
        setComandes(data);
    };

    useEffect(() => {
    const fetchData = async () => {
        await load();
    };

    fetchData();
    }, []);

    const move = async (
        id: string,
        nextEstat: string
    ) => {
        await updateEstat(id, nextEstat);

        load();
    };

    return (
        <div className="min-h-screen bg-slate-100 p-6">
        <h1 className="text-4xl font-black mb-8">
            Panell administratiu
        </h1>

        <div className="grid grid-cols-4 gap-6">
            {ESTATS.map((estat) => (
            <div
                key={estat}
                className="bg-white rounded-3xl p-4 shadow-sm"
            >
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
                        {new Date(comanda.data)
                            .toLocaleString()}
                        </p>

                        <div className="mt-4 flex flex-col gap-2">
                        {estat === "pendent" && (
                            <button
                            onClick={() =>
                                move(comanda.id, "cuinant")
                            }
                            className="bg-orange-500 text-white rounded-xl py-2"
                            >
                            Començar
                            </button>
                        )}

                        {estat === "cuinant" && (
                            <button
                            onClick={() =>
                                move(comanda.id, "repartiment")
                            }
                            className="bg-sky-500 text-white rounded-xl py-2"
                            >
                            Enviar repartiment
                            </button>
                        )}

                        {estat === "repartiment" && (
                            <button
                            onClick={() =>
                                move(comanda.id, "entregada")
                            }
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