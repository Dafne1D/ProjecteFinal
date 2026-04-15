import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  images: {
    remotePatterns: [
      {
        protocol: "http",
        hostname: "localhost",
        port: "", 
        pathname: "/**", 
      },
    ],
  },
};

module.exports = nextConfig;