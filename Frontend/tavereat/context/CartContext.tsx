"use client";

import {
  createContext,
  useContext,
  useCallback,
  useEffect,
  useState,
} from "react";
import { getCart } from "@/Services/comandaVendaAPI";
import type { Cart } from "@/Services/comandaVendaAPI";

type CartContextType = {
  open: boolean;
  setOpen: (v: boolean) => void;
  cart: Cart | null;
  refreshCart: () => Promise<void>;
  loading: boolean;
};

const CartContext = createContext<CartContextType | null>(null);

export function CartProvider({ children }: { children: React.ReactNode }) {
  const [open, setOpen] = useState(false);
  const [cart, setCart] = useState<Cart | null>(null);
  const [loading, setLoading] = useState(false);

  const refreshCart = useCallback(async () => {
    try {
      setLoading(true);
      const data = await getCart();
      setCart(data);
    } catch (err) {
      console.error("Cart error:", err);
      setCart(null);
    } finally {
      setLoading(false);
    }
  }, []);

  // SOLO cargar si hay token
  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) refreshCart();
  }, [refreshCart]);

  return (
    <CartContext.Provider value={{ open, setOpen, cart, refreshCart, loading }}>
      {children}
    </CartContext.Provider>
  );
}

export const useCart = () => {
  const context = useContext(CartContext);
  if (!context) throw new Error("CartProvider missing");
  return context;
};