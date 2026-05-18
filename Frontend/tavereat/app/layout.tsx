import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";

import { CartProvider } from "@/context/CartContext";
import CartDrawer from "@/components/CartCard";
import LayoutWrapper from "@/components/LayoutWrapper";
import { ReactNode } from "react";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export default function RootLayout({
  children,
}: {
  children: ReactNode;
}) {
  return (
    <html lang="en" className={`${geistSans.variable} ${geistMono.variable}`}>
      <body className="min-h-full antialiased">
        <CartProvider>
          <LayoutWrapper>
            {children}
          </LayoutWrapper>

          <CartDrawer />
        </CartProvider>
      </body>
    </html>
  );
}

export const metadata: Metadata = {
  title: "TaverEat",
  description: "",
};