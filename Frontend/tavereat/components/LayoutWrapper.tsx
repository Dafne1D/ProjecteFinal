"use client";

import { useCart } from "@/context/CartContext";
import { ReactNode } from "react";

export default function LayoutWrapper({
  children,
}: {
  children: ReactNode;
}) {
  const { open } = useCart();

  return (
    <div
      className={`min-h-screen transition-all duration-300 ${
        open ? "pr-[360px]" : "pr-0"
      }`}
    >
      {children}
    </div>
  );
}