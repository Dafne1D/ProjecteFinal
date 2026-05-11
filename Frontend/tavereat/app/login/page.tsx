"use client";

import React, { useState } from "react";
import { Mail, Lock, Eye, EyeOff, ArrowRight } from "lucide-react";
import Link from "next/link";

export default function LoginPage() {
  const [showPassword, setShowPassword] = useState(false);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    // Logic for frontend demo
    console.log("Login attempt:", { email, password });
  };

  return (
    <div className="min-h-screen w-full flex items-center justify-center relative overflow-hidden bg-[#0f172a]">
      {/* Background Orbs for Mesh Gradient Effect */}
      <div className="absolute top-[-10%] left-[-10%] w-[40%] h-[40%] bg-orange-600/30 rounded-full blur-[120px] animate-pulse"></div>
      <div className="absolute bottom-[-10%] right-[-10%] w-[40%] h-[40%] bg-purple-600/20 rounded-full blur-[120px] animate-pulse" style={{ animationDelay: "2s" }}></div>
      <div className="absolute top-[20%] right-[10%] w-[30%] h-[30%] bg-blue-600/20 rounded-full blur-[100px] animate-pulse" style={{ animationDelay: "4s" }}></div>

      {/* Main Container */}
      <div className="z-10 w-full max-w-md px-6">
        <div className="backdrop-blur-2xl bg-white/5 border border-white/10 rounded-3xl p-8 shadow-2xl shadow-black/50">
          {/* Logo / Header */}
          <div className="text-center mb-8">
            <h1 className="text-4xl font-bold bg-gradient-to-r from-orange-400 to-rose-400 bg-clip-text text-transparent">
              TaverEat
            </h1>
            <p className="text-gray-400 mt-2 text-sm">Bienvenido de nuevo, entra y disfruta.</p>
          </div>

          <form onSubmit={handleSubmit} className="space-y-5">
            {/* Email Field */}
            <div className="space-y-2">
              <label className="text-xs font-medium text-gray-400 ml-1 uppercase tracking-wider">Email</label>
              <div className="relative group">
                <div className="absolute inset-y-0 left-0 pl-4 flex items-center pointer-events-none text-gray-500 group-focus-within:text-orange-400 transition-colors">
                  <Mail size={18} />
                </div>
                <input
                  type="email"
                  required
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  className="w-full bg-white/5 border border-white/10 rounded-xl py-3 pl-11 pr-4 text-white placeholder:text-gray-600 focus:outline-none focus:ring-2 focus:ring-orange-500/50 focus:border-orange-500/50 transition-all"
                  placeholder="tu@ejemplo.com"
                />
              </div>
            </div>

            {/* Password Field */}
            <div className="space-y-2">
              <div className="flex justify-between items-center px-1">
                <label className="text-xs font-medium text-gray-400 uppercase tracking-wider">Contraseña</label>
                <Link href="#" className="text-xs text-orange-400 hover:text-orange-300 transition-colors">
                  ¿Olvidaste tu contraseña?
                </Link>
              </div>
              <div className="relative group">
                <div className="absolute inset-y-0 left-0 pl-4 flex items-center pointer-events-none text-gray-500 group-focus-within:text-orange-400 transition-colors">
                  <Lock size={18} />
                </div>
                <input
                  type={showPassword ? "text" : "password"}
                  required
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  className="w-full bg-white/5 border border-white/10 rounded-xl py-3 pl-11 pr-12 text-white placeholder:text-gray-600 focus:outline-none focus:ring-2 focus:ring-orange-500/50 focus:border-orange-500/50 transition-all"
                  placeholder="••••••••"
                />
                <button
                  type="button"
                  onClick={() => setShowPassword(!showPassword)}
                  className="absolute inset-y-0 right-0 pr-4 flex items-center text-gray-500 hover:text-gray-300 transition-colors"
                >
                  {showPassword ? <EyeOff size={18} /> : <Eye size={18} />}
                </button>
              </div>
            </div>

            {/* Login Button */}
            <button
              type="submit"
              className="w-full bg-gradient-to-r from-orange-500 to-rose-500 hover:from-orange-600 hover:to-rose-600 text-white font-bold py-3.5 rounded-xl shadow-lg shadow-orange-500/20 transform active:scale-[0.98] transition-all flex items-center justify-center gap-2 group mt-2"
            >
              Iniciar Sesión
              <ArrowRight size={18} className="group-hover:translate-x-1 transition-transform" />
            </button>
          </form>

          {/* Divider */}
          <div className="relative my-8">
            <div className="absolute inset-0 flex items-center">
              <div className="w-full border-t border-white/10"></div>
            </div>
            <div className="relative flex justify-center text-xs uppercase">
              <span className="bg-[#1a1a2e] px-4 text-gray-500">O continúa con</span>
            </div>
          </div>

          {/* Social Logins */}
          <div className="grid grid-cols-1 gap-4">
            <button className="flex items-center justify-center gap-2 bg-white/5 border border-white/10 hover:bg-white/10 text-white py-2.5 rounded-xl transition-colors">
              <span className="text-sm">Google</span>
            </button>
          </div>

          {/* Sign Up Link */}
          <p className="text-center mt-8 text-sm text-gray-400">
            ¿No tienes cuenta?{" "}
            <Link href="#" className="text-orange-400 font-semibold hover:text-orange-300 transition-colors">
              Regístrate ahora
            </Link>
          </p>
        </div>

        {/* Footer info */}
        <p className="text-center mt-8 text-gray-600 text-xs tracking-widest uppercase">
          &copy; 2026 TaverEat &bull; Premium Experience
        </p>
      </div>
    </div>
  );
}
